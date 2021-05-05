using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoglon : MonoBehaviour, IEntityAnimations
{



    public float attackRange;
    public float chargeRange;
    private Rigidbody2D rb;
    private bool preCharging;
    private bool charging;
    private bool attacking;
    private bool canAttack;
    private bool canSummon;
    private bool canCharge;
    private EntityHealth entityHealth;
    private IAbilityTargetPosition targetPositionScript;
    private Animator animator;
    private Vector2 lookDirection;
    private EntityTargetingSystem targetingSystem;
    private EnemyMovementController enemyMovementController;
    private Vector2 targetPosAtStart;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<EntityAbilityManager>().ability1 = GetComponent<HoglonBasicAttack>();
        GetComponent<EntityAbilityManager>().ability2 = GetComponent<HoglonCharge>();
        GetComponent<EntityAbilityManager>().ability3 = GetComponent<HoglonSummon>();


        entityHealth = GetComponent<EntityHealth>();
        targetPositionScript = GetComponent<IAbilityTargetPosition>();
        enemyMovementController = GetComponent<EnemyMovementController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        targetingSystem = GetComponent<EntityTargetingSystem>();
        canAttack = true;
        canCharge = true;
        canSummon = true;
        preCharging = false;
        charging = false;
        attacking = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (targetingSystem.target != null)
        {
            if (rb.velocity.magnitude != 0)
            {
                animator.SetBool("Walking", true);
            }
            if (preCharging)
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if (charging)
                GetComponent<Rigidbody2D>().velocity = targetPosAtStart.normalized * 15;
            if (!charging)
                LookToTarget();
            if (entityHealth.maxHealth / entityHealth.health > 2 && canSummon && !attacking && !charging)
            {
                attacking = true;
                Debug.Log("hoglon using ability 3");
                GetComponent<EntityAbilityManager>().CastAbility(3);
                StartCoroutine(SetSummonOnCoolDown(10));
            }
            else if (canCharge && !attacking && !charging && Vector2.Distance(targetingSystem.target.transform.position, gameObject.transform.position) > chargeRange)
            {
                attacking = true;
                GetComponent<EntityAbilityManager>().CastAbility(2);
                StartCoroutine(SetChargeOnCoolDown(6));
            }
            else if (canAttack && !attacking && !charging && Vector2.Distance(targetingSystem.target.transform.position, gameObject.transform.position) < attackRange)
            {
                attacking = true;
                GetComponent<EntityAbilityManager>().CastAbility(1);
                StartCoroutine(SetAttackOnCoolDown(2));
            }

        }
    }

    private void LookToTarget()
    {

        Vector2 difference = targetingSystem.target.transform.position - transform.position;
        difference.Normalize();
        lookDirection = difference;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        rotationZ = rotationZ / 6;
        Vector2 targetTransFormVector = (Vector2)targetingSystem.target.transform.position - (Vector2)transform.position;
        if (targetTransFormVector.x < 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            if (targetTransFormVector.y < 0)
                rotationZ += 30;
            else
                rotationZ -= 30;

        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        gameObject.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
    public void SetAttacking(bool trueOrFalse)
    {

    }

    public IEnumerator SetAttackOnCoolDown(float coolDown)
    {
        canAttack = false;
        attacking = true;
        enemyMovementController.SetMoveSlow(true);
        yield return new WaitForSeconds(1f);
        attacking = false;
        enemyMovementController.SetMoveSlow(false);
        StartCoroutine(SetAttackOffCoolDown(coolDown));
    }
    public IEnumerator SetSummonOnCoolDown(float coolDown)
    {

        enemyMovementController.SetMoveSlow(true);
        canSummon = false;
        attacking = true;
        yield return new WaitForSeconds(3f);
        enemyMovementController.SetMoveSlow(false);
        attacking = false;
        StartCoroutine(SetSummonOffCoolDown(coolDown));
    }
    public IEnumerator SetChargeOnCoolDown(float coolDown)
    {
        enemyMovementController.Halt(true);
        canCharge = false;
        preCharging = true;
        attacking = true;
        yield return new WaitForSeconds(1);
        targetPosAtStart = targetPositionScript.GetTargetPosition() - (Vector2)transform.position;
        preCharging = false;
        charging = true;
        yield return new WaitForSeconds(2f);
        animator.SetBool("Charging", false);
        enemyMovementController.Halt(false);
        charging = false;
        attacking = false;
        StartCoroutine(SetChargeOffCoolDown(coolDown));
    }
    IEnumerator SetAttackOffCoolDown(float coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        canAttack = true;
    }
    IEnumerator SetSummonOffCoolDown(float coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        canSummon = true;
    }
    IEnumerator SetChargeOffCoolDown(float coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        canCharge = true;
    }
}
