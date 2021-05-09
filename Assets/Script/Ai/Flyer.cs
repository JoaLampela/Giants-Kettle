using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour, IEntityAnimations
{



    public float attackRange;
    private bool attacking;
    private bool canAttack;
    private Animator animator;
    private Vector2 lookDirection;
    private EntityTargetingSystem targetingSystem;


    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        animator = GetComponent<Animator>();
        targetingSystem = GetComponent<EntityTargetingSystem>();
        GetComponent<EntityAbilityManager>().ability4 = GetComponent<StaffBasicAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetingSystem.target != null)
        {
            LookToTarget();
            if (canAttack)
            {
                Debug.Log("Flyer cast ability");
                GetComponent<EntityAbilityManager>().CastAbility(4);
            }
        }

    }


    private void LookToTarget()
    {

        Vector2 difference = targetingSystem.target.transform.position - transform.position;
        difference.Normalize();
        lookDirection = difference;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Vector2 targetTransFormVector = (Vector2)targetingSystem.target.transform.position - (Vector2)transform.position;
        if (targetTransFormVector.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            rotationZ += 180f;
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
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

    public bool GetAttacking()
    {
        return attacking;
    }
}
