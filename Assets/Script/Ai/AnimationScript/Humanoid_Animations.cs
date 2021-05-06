using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid_Animations : MonoBehaviour, IEntityAnimations
{
    public bool attacking { private set; get; }
    public Vector2 lookDirection { private set; get; }
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject hiddenLeftHand;
    public GameObject leftHandContainer;
    public GameObject rightHandContainer;
    public GameObject rightHandWeapon;
    public GameObject leftHandWeapon;

    [Header("Spawn these prefrabs for the goblin")]
    public GameObject weapon1;
    public GameObject weapon2;

    private Rigidbody2D humanoidRB;
    private Animator animator;
    private bool usingSingleHandedSword;
    private bool usingTwoHandedSword;
    private bool usingStaff;
    private bool usingShield;
    private bool usingBow;
    private bool offHandUsingSingleHandedSword;
    private bool offHandUsingShield;
    private bool attackOnCooldown;


    void Start()
    {

        humanoidRB = GetComponent<Rigidbody2D>();
        attackOnCooldown = false;
        animator = GetComponent<Animator>();

        if (weapon1 != null)
        {
            SwitchToSingleHandedSword(weapon1);
        }
        if (weapon2 != null)
        {
            SwitchToOffHandSingleHandedSword(weapon2);
        }
    }
    private void LookDirectionUpdate()
    {
        Vector2 difference = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z)) - rightHandContainer.transform.position;
        difference.Normalize();
        lookDirection = difference;
    }

    private void LookToMouse()
    {

        Vector2 difference = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z)) - rightHandContainer.transform.position;
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
        rightHandContainer.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    public void SwitchToSingleHandedSword(GameObject inGameObject)
    {
        UnequipRightHandBools();
        rightHandWeapon = Instantiate(inGameObject, rightHand.transform.position, new Quaternion(0, 0, 0, 0), rightHand.transform);
        usingSingleHandedSword = true;
        animator.SetBool("ShortSwordEquiped", true);
    }
    public void SwitchToOffHandSingleHandedSword(GameObject inGameObject)
    {
        UnequipLeftHandBools();
        leftHandWeapon = Instantiate(inGameObject, leftHand.transform.position, new Quaternion(0, 0, 0, 0), leftHand.transform);
        offHandUsingSingleHandedSword = true;
        animator.SetBool("ShortSwordOffHandEquiped", true);
    }
    public void SwitchToOffHandShield(GameObject inGameObject)
    {
        UnequipLeftHandBools();
        leftHandWeapon = Instantiate(inGameObject, leftHand.transform.position, new Quaternion(0, 0, 0, 0), rightHand.transform);
        offHandUsingShield = true;
        animator.SetBool("ShieldOffHandEquiped", true);
    }
    public void SwitchToShield(GameObject inGameObject)
    {
        UnequipRightHandBools();
        rightHandWeapon = Instantiate(inGameObject, rightHand.transform.position, new Quaternion(0, 0, 0, 0), rightHand.transform);
        usingShield = true;
        animator.SetBool("ShieldEquiped", true);
    }
    public void SwitchToBow(GameObject inGameObject)
    {
        UnequipRightHandBools();
        UnequipLeftHandBools();
        rightHandWeapon = Instantiate(inGameObject, rightHand.transform.position, new Quaternion(0, 0, 0, 0), rightHand.transform);
        usingBow = true;
        animator.SetBool("BowEquiped", true);
    }
    public void SwitchToTwoHandedSword(GameObject inGameObject)
    {
        UnequipRightHandBools();
        UnequipLeftHandBools();
        hiddenLeftHand.GetComponent<SpriteRenderer>().enabled = true;
        leftHand.GetComponent<SpriteRenderer>().enabled = false;
        rightHandWeapon = Instantiate(inGameObject, rightHand.transform.position, new Quaternion(0, 0, 0, 0), rightHand.transform);
        usingTwoHandedSword = true;
        animator.SetBool("TwoHandedSwordEquiped", true);
    }
    public void SwitchToStaff(GameObject inGameObject)
    {
        UnequipRightHandBools();
        UnequipLeftHandBools();
        rightHandWeapon = Instantiate(inGameObject, rightHand.transform.position, new Quaternion(0, 0, 0, 0), rightHand.transform);
        usingStaff = true;
        animator.SetBool("StaffEquiped", true);
    }

    public void SwitchToEmptyRightHand()
    {
        UnequipRightHandBools();
    }
    public void SwitchToEmptyLeftHand()
    {
        UnequipLeftHandBools();
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
    private void UnequipLeftHandBools()
    {
        Destroy(leftHandWeapon);
        hiddenLeftHand.GetComponent<SpriteRenderer>().enabled = false;
        leftHand.GetComponent<SpriteRenderer>().enabled = true;
        offHandUsingShield = false;
        offHandUsingSingleHandedSword = false;
        usingTwoHandedSword = false;
        usingStaff = false;
        usingBow = false;
        animator.SetBool("TwoHandedSwordEquiped", false);
        animator.SetBool("BowEquiped", false);
        animator.SetBool("StaffEquiped", false);
        animator.SetBool("ShortSwordOffHandEquiped", false);
        animator.SetBool("ShieldOffHandEquiped", false);
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

    public void SetAttacking(bool value)
    {
        attacking = value;
    }

    public bool GetAttacking()
    {
        return attacking;
    }
}
