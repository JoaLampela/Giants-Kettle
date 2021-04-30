using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoglon : MonoBehaviour, IEntityAnimations
{



    public float attackRange;
    private Rigidbody2D rb;
    private bool attacking;
    private bool canAttack;
    private Animator animator;
    private Vector2 lookDirection;
    private EntityTargetingSystem targetingSystem;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<EntityAbilityManager>().ability1 = GetComponent<TwoHandedBasicAttack>();
        rb = GetComponent<Rigidbody2D>();
        canAttack = true;
        animator = GetComponent<Animator>();
        targetingSystem = GetComponent<EntityTargetingSystem>();
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
            LookToTarget();
            if (canAttack && Vector2.Distance(targetingSystem.target.transform.position, gameObject.transform.position) < attackRange)
            {
                GetComponent<EntityAbilityManager>().CastAbility(1);
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
        yield return new WaitForSeconds(1f);
        attacking = false;
        yield return new WaitForSeconds(coolDown);
        canAttack = true;
    }
}
