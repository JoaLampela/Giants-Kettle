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

    private Rigidbody2D humanoidRB;
    private Animator animator;
    private bool usingSingleHandedSword;
    private bool usingTwoHandedSword;
    private bool usingStaff;
    private bool usingShield;
    private bool usingBow;
    private bool offhandUsingSingleHandedSword;
    private bool offhandUsingShield;
    private bool attackOnCooldown;


    void Start()
    {

        humanoidRB = GetComponent<Rigidbody2D>();
        attackOnCooldown = false;
        animator = GetComponent<Animator>();


        SwitchToSingleHandedSword();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (usingSingleHandedSword)
            {
                SwitchToTwoHandedSword();
            }
            else
            {
                SwitchToSingleHandedSword();
            }
        }
    }

    public void SwitchToSingleHandedSword()
    {
        leftHand.transform.parent = leftHandContainer.transform;
        LeftHandResetPosition();
        UnequipRightHandBools();
        usingSingleHandedSword = true;
        animator.SetBool("ShortSwordEquiped", true);
    }
    public void SwitchToTwoHandedSword()
    {
        UnequipRightHandBools();
        UnequipLefHandBools();
        leftHand.transform.parent = rightHand.transform;
        usingTwoHandedSword = true;
        animator.SetBool("TwoHandedSwordEquiped", true);
        leftHand.transform.position = rightHand.transform.position + new Vector3(0, -0.1f, 0);
    }
    private void UnequipRightHandBools()
    {
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
