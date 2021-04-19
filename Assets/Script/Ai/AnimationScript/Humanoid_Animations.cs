    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid_Animations : MonoBehaviour
{
    public bool attacking { private set; get; }
    public Vector2 lookDirection { private set; get; }
    public GameObject leftHand;
    public GameObject rightHand;
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

    public void SwitchToSingleHandedSword(GameObject inGameObject)
    {
        leftHand.transform.parent = leftHandContainer.transform;
        LeftHandResetPosition();
        UnequipRightHandBools();
        rightHandWeapon = Instantiate(inGameObject, rightHand.transform.position, new Quaternion(0, 0, 0, 0), rightHand.transform);
        usingSingleHandedSword = true;
        animator.SetBool("ShortSwordEquiped", true);
    }
    public void SwitchToOffHandSingleHandedSword(GameObject inGameObject)
    {
        leftHand.transform.parent = leftHandContainer.transform;
        LeftHandResetPosition();
        UnequipLeftHandBools();
        leftHandWeapon = Instantiate(inGameObject, leftHand.transform.position, new Quaternion(0, 0, 0, 0), rightHand.transform);
        offHandUsingSingleHandedSword = true;
        animator.SetBool("ShortSwordOffHandEquiped", true);
    }
    public void SwitchToOffHandShield(GameObject inGameObject)
    {
        leftHand.transform.parent = leftHandContainer.transform;
        LeftHandResetPosition();
        UnequipLeftHandBools();
        leftHandWeapon = Instantiate(inGameObject, leftHand.transform.position, new Quaternion(0, 0, 0, 0), rightHand.transform);
        offHandUsingShield = true;
        animator.SetBool("ShortSwordOffHandEquiped", true);
    }
    public void SwitchToBow(GameObject inGameObject)
    {
        UnequipRightHandBools();
        UnequipLeftHandBools();
        leftHand.transform.parent = rightHand.transform;
        rightHandWeapon = Instantiate(inGameObject, rightHand.transform.position, new Quaternion(0, 0, 0, 0), rightHand.transform);
        usingBow = true;
        animator.SetBool("BowEquiped", true);
        leftHand.transform.position = rightHand.transform.position + new Vector3(0, -0.1f, 0);
    }
    public void SwitchToTwoHandedSword(GameObject inGameObject)
    {
        UnequipRightHandBools();
        UnequipLeftHandBools();
        leftHand.transform.parent = rightHand.transform;
        rightHandWeapon = Instantiate(inGameObject, rightHand.transform.position, new Quaternion(0, 0, 0, 0), rightHand.transform);
        usingTwoHandedSword = true;
        animator.SetBool("TwoHandedSwordEquiped", true);
        leftHand.transform.position = rightHand.transform.position + new Vector3(0, -0.1f, 0);
    }
    public void SwitchToStaff(GameObject inGameObject)
    {
        UnequipRightHandBools();
        UnequipLeftHandBools();
        leftHand.transform.parent = rightHand.transform;
        rightHandWeapon = Instantiate(inGameObject, rightHand.transform.position, new Quaternion(0, 0, 0, 0), rightHand.transform);
        usingStaff = true;
        animator.SetBool("StaffEquiped", true);
        leftHand.transform.position = rightHand.transform.position + new Vector3(0, -0.1f, 0);
    }

    public void SwitchToEmptyRightHand()
    {
        UnequipRightHandBools();
        leftHand.transform.parent = leftHandContainer.transform;
    }
    public void SwitchToEmptyLeftHand()
    {
        UnequipLeftHandBools();
        LeftHandResetPosition();
        leftHand.transform.parent = leftHandContainer.transform;
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
    private void LeftHandResetPosition()
    {
        leftHand.transform.position = new Vector3(-0.4f, 0, 0) * transform.localScale.x + leftHandContainer.transform.position;
    }
    private void RightHandResetPosition()
    {
        leftHand.transform.position = new Vector3(0.4f, 0, 0) * transform.localScale.x + rightHandContainer.transform.position;
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
