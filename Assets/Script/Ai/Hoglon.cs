using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoglon : MonoBehaviour, IEntityAnimations
{



    public float attackRange;
    public float chargeRange;
    private Rigidbody2D rb;
    private bool charging;
    private bool attacking;
    private bool canAttack;
    private bool canCharge;
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


        targetPositionScript = GetComponent<IAbilityTargetPosition>();
        enemyMovementController = GetComponent<EnemyMovementController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        targetingSystem = GetComponent<EntityTargetingSystem>();
        canAttack = true;
        canCharge = true;
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
            if (charging)
                GetComponent<Rigidbody2D>().velocity = targetPosAtStart.normalized * 15;
            if (!charging)
                LookToTarget();
            if (canCharge && canAttack && !attacking && !charging && Vector2.Distance(targetingSystem.target.transform.position, gameObject.transform.position) > chargeRange)
            {
                GetComponent<EntityAbilityManager>().CastAbility(2);
                Debug.Log("charging");
            }
            else if (canAttack && !attacking && !charging && Vector2.Distance(targetingSystem.target.transform.position, gameObject.transform.position) < attackRange)
            {
                GetComponent<EntityAbilityManager>().CastAbility(1);
                StartCoroutine(SetAttackOnCoolDown(10));
                Debug.Log("casting");
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
        StartCoroutine(SetAttackOnCoolDown(0.5f));
    }

    IEnumerator SetAttackOnCoolDown(float coolDown)
    {
        canAttack = false;
        attacking = true;
        canCharge = false;
        enemyMovementController.SetMoveSlow(true);
        yield return new WaitForSeconds(1f);
        canCharge = true;
        attacking = false;
        enemyMovementController.SetMoveSlow(false);
        StartCoroutine(SetAttackOffCoolDown(coolDown));
    }
    public IEnumerator SetChargeOnCoolDown(float coolDown)
    {
        enemyMovementController.Halt(true);
        targetPosAtStart = targetPositionScript.GetTargetPosition() - (Vector2)transform.position;
        charging = true;
        canAttack = false;
        canCharge = false;
        attacking = true;
        yield return new WaitForSeconds(2f);
        animator.SetBool("Charging", false);


        enemyMovementController.Halt(false);
        charging = false;
        canAttack = true;
        attacking = false;
        StartCoroutine(SetChargeOffCoolDown(coolDown));
    }
    IEnumerator SetAttackOffCoolDown(float coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        canAttack = true;
    }
    IEnumerator SetChargeOffCoolDown(float coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        canCharge = true;
    }
}
