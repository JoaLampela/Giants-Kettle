using System.Collections;
using UnityEngine;

public class Player_Animations : MonoBehaviour
{
    public bool attacking { private set; get; }
    public Vector2 lookDirection { private set; get; }
    public GameObject rightArmContainer;
    public GameObject rightArm;
    public GameObject leftArm;
    public GameObject leftArmContainer;
    [SerializeField] private GameObject rightHandWeapon;
    [SerializeField] private GameObject leftHandWeapon;

    public float trueAttackSpeed = 1;

    private Rigidbody2D playerRB;
    private Animator animator;
    private bool usingSingleHandedSword;
    private bool usingTwoHandedSword;
    private bool usingStaff;
    private bool usingShield;
    private bool usingBow;
    private bool offhandUsingSingleHandedSword;
    private bool offhandUsingShield;

    private bool attackOnCooldown;


    // Start is called before the first frame update
    void Start()
    {

        playerRB = GetComponent<Rigidbody2D>();
        attackOnCooldown = false;
        animator = GetComponent<Animator>();
        animator.SetFloat("TrueAttackSpeed", trueAttackSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (usingSingleHandedSword)
        {
            if (Input.GetMouseButtonDown(0) && !attacking && !attackOnCooldown)
            {
                animator.SetTrigger("Attack");
                attacking = true;
                StartCoroutine(GetComponent<MovementScript>().AttackSlow(0.04f * 2f / trueAttackSpeed));
                StartCoroutine(SetAttacking(0.04f));
                StartCoroutine(GetComponent<MovementScript>().AttackStep(0 / trueAttackSpeed, 500));
                StartCoroutine(SetAttackOnCooldown(0.4f / trueAttackSpeed));
            }
            if (Input.GetMouseButtonDown(1) && !attacking && !attackOnCooldown)
            {
                animator.SetTrigger("Special");
                attacking = true;

                StartCoroutine(GetComponent<MovementScript>().AttackStep(0.15f / trueAttackSpeed, 1000));
                StartCoroutine(GetComponent<MovementScript>().AttackSlow(0.30f * 2f / trueAttackSpeed));
                StartCoroutine(SetAttacking(0.30f / trueAttackSpeed));
                StartCoroutine(SetAttackOnCooldown(0.6f / trueAttackSpeed));
            }
        }
        if (usingTwoHandedSword)
        {
            if (Input.GetMouseButtonDown(0) && !attacking && !attackOnCooldown)
            {
                animator.SetTrigger("Attack");
                attacking = true;
                StartCoroutine(GetComponent<MovementScript>().AttackStep(0.4f / trueAttackSpeed, 1000));
                StartCoroutine(GetComponent<MovementScript>().AttackSlow(0.6f / trueAttackSpeed));
                StartCoroutine(SetAttacking(0.5f / trueAttackSpeed));
                StartCoroutine(SetAttackOnCooldown(0.6f / trueAttackSpeed));
            }
            if (Input.GetMouseButtonDown(1) && !attacking && !attackOnCooldown)
            {
                animator.SetTrigger("Special");
                attacking = true;
                StartCoroutine(GetComponent<MovementScript>().AttackStep(0.6f / trueAttackSpeed, 1500));
                StartCoroutine(GetComponent<MovementScript>().AttackSlow(0.80f / trueAttackSpeed));
                StartCoroutine(SetAttacking(0.8f / trueAttackSpeed));
                StartCoroutine(SetAttackOnCooldown(0.8f / trueAttackSpeed));
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && !attacking && !attackOnCooldown)
            {
                animator.SetTrigger("LeftAttack");
                attacking = true;
                StartCoroutine(GetComponent<MovementScript>().AttackSlow(1.5f / trueAttackSpeed));
                StartCoroutine(SetAttacking(1.5f / trueAttackSpeed));
                StartCoroutine(SetAttackOnCooldown(2f / trueAttackSpeed));
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && !attacking)
        {
            if (GetComponent<MovementScript>().Dash())
                animator.SetTrigger("Dash");
        }
        
        if (!attacking)
            LookToMouse();
        else
        {
            LookDirectionUpdate();
        }
    }



    private void LookDirectionUpdate()
    {
        Vector2 difference = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z)) - rightArmContainer.transform.position;
        difference.Normalize();
        lookDirection = difference;
    }

    private void LookToMouse()
    {

        Vector2 difference = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z)) - rightArmContainer.transform.position;
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
        rightArmContainer.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }
    public void SwitchToSingleHandedSword(GameObject  inGameObject)
    {
        leftArm.transform.parent = leftArmContainer.transform;
        LeftHandResetPosition();
        UnequipRightHandBools();
        rightHandWeapon = Instantiate(inGameObject, rightArm.transform.position, new Quaternion(0, 0, 0, 0), rightArm.transform);
        usingSingleHandedSword = true;
        animator.SetBool("ShortSwordEquiped", true);
    }
    public void SwitchToEmptyRightHand()
    {
        UnequipRightHandBools();
        leftArm.transform.parent = leftArmContainer.transform;
    }
    public void SwitchToTwoHandedSword()
    {
        UnequipRightHandBools();
        UnequipLefHandBools();
        leftArm.transform.parent = rightArm.transform;
        usingTwoHandedSword = true;
        animator.SetBool("TwoHandedSwordEquiped", true);
        leftArm.transform.position = rightArm.transform.position + new Vector3(0, -0.1f, 0);
    }
    private void UnequipRightHandBools()
    {
        Destroy(rightHandWeapon);
        usingSingleHandedSword = false;
        usingTwoHandedSword = false;
        usingStaff = false;
        usingShield = false;
        usingBow = false;
        animator.SetBool("ShortSwordEquiped", false);
        animator.SetBool("TwoHandedSwordEquiped", false);
        animator.SetBool("BowEquiped", false);
        animator.SetBool("StaffEquiped", false);
        animator.SetBool("ShieldEquiped", false);
    }
    private void UnequipLefHandBools()
    {
        offhandUsingShield = false;
        offhandUsingSingleHandedSword = false;
        usingTwoHandedSword = false;
        usingStaff = false;
        usingBow = false;
        animator.SetBool("TwoHandedSwordEquiped", false);
        animator.SetBool("BowEquiped", false);
        animator.SetBool("StaffEquiped", false);
        animator.SetBool("ShortSwordOffHandEquiped", false);
        animator.SetBool("ShieldOffHandEquiped", false);
    }
    private void LeftHandResetPosition()
    {
        leftArm.transform.position = new Vector3(-0.4f, 0, 0) * transform.localScale.x + leftArmContainer.transform.position;
    }
    private void RightHandResetPosition()
    {
        leftArm.transform.position = new Vector3(0.4f, 0, 0) * transform.localScale.x + rightArmContainer.transform.position;
    }
    IEnumerator SetAttacking(float CoolDown)
    {
        attacking = true;
        yield return new WaitForSeconds(CoolDown);
        attacking = false;
    }
    IEnumerator SetAttackOnCooldown(float CoolDown)
    {
        attackOnCooldown = true;
        yield return new WaitForSeconds(CoolDown);
        attackOnCooldown = false;
    }
}
