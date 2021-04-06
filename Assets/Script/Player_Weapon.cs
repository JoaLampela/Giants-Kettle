using System.Collections;
using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    public bool attacking { private set; get; }
    public Vector2 lookDirection { private set; get; }
    public GameObject rightArm;

    private Animator animator;
    private bool UsingSingleHandedSword;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        SwitchToSingleHandedSword();
    }

    // Update is called once per frame
    void Update()
    {
        if (UsingSingleHandedSword)
        {
            if (Input.GetMouseButtonDown(0) && !attacking)
            {
                animator.SetTrigger("Attack");
                attacking = true;
                StartCoroutine(GetComponent<MovementScript>().AttackSlow(0.04f * 2f));
                StartCoroutine(SingleHandedSwordSwing(0.04f));
            }
        }
        if (!attacking)
            LookToMouse();
    }

    private void LookToMouse()
    {

        Vector2 difference = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z)) - rightArm.transform.position;
        difference.Normalize();
        lookDirection = difference;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Vector3 playerTransformVector = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z)) - transform.position;
        if (playerTransformVector.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            rotationZ += 180f;
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        rightArm.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }
    public void SwitchToSingleHandedSword()
    {
        UnequipRightHandBools();
        UsingSingleHandedSword = true;
        animator.SetBool("ShortSwordEquiped", true);
    }
    private void UnequipRightHandBools()
    {
        UsingSingleHandedSword = false;
        animator.SetBool("ShortSwordEquiped", false);
    }
    IEnumerator SingleHandedSwordSwing(float CoolDown)
    {
        attacking = true;
        yield return new WaitForSeconds(CoolDown);
        attacking = false;
    }
}
