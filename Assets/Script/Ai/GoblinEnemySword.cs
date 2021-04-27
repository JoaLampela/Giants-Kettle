using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinEnemySword : MonoBehaviour
{
    public GameObject rightArmContainer;
    public GameObject eye;
    public float attackRange;
    private bool attacking;
    private bool canAttack;
    private Animator animator;
    private Vector2 lookDirection;
    private EntityTargetingSystem targetingSystem;
    private Rigidbody2D goblinRB;
    private CircleCollider2D goblinCollider;
    private EntityAbilityManager abilityManager;

    float currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        animator = GetComponent<Animator>();
        goblinRB = GetComponent<Rigidbody2D>();
        goblinCollider = GetComponent<CircleCollider2D>();
        targetingSystem = GetComponent<EntityTargetingSystem>();
        abilityManager = GetComponent<EntityAbilityManager>();
        currentRotation = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (targetingSystem.target != null)
        {
            if (goblinRB.velocity != Vector2.zero)
                animator.SetBool("Walking", true);
            else
                animator.SetBool("Walking", false);
            if (!attacking)
            {
                LookToTarget();
                if (Vector2.Distance(targetingSystem.target.transform.position, transform.position) < attackRange && canAttack)
                {
                    abilityManager.CastAbility(4);
                    StartCoroutine(SetAttackOnCoolDown(2f));
                }
            }


        }

    }


    private void LookDirectionUpdate()
    {
        Vector2 difference = targetingSystem.target.transform.position - rightArmContainer.transform.position;
        difference.Normalize();
        lookDirection = difference;
    }

    private void LookToTarget()
    {

        Vector2 difference = targetingSystem.target.transform.position - rightArmContainer.transform.position;
        difference.Normalize();
        lookDirection = difference;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Vector2 goblinTransFormVector = (Vector2)targetingSystem.target.transform.position - (Vector2)transform.position;
        if (goblinTransFormVector.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            rotationZ += 180f;
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        rightArmContainer.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }
    IEnumerator SetAttackOnCoolDown(float coolDown)
    {
        canAttack = false;
        attacking = true;
        yield return new WaitForSeconds(1.2f);
        attacking = false;
        yield return new WaitForSeconds(coolDown);
        canAttack = true;

    }
}
