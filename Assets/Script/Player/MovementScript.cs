﻿using System.Collections;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float speed = 8;
    public Camera playerCamera;
    public BoxCollider2D swordCollider;
    public float dashCoolDown;

    private bool canDash;
    private Vector2 movement;
    private Rigidbody2D playerRB;
    private bool walkingBackWards;
    private bool attacking;
    private Animator animator;
    private Vector2 lookDirection;
    private float baseMovementSpeed;





    //Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        canDash = true;
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        baseMovementSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        lookDirection = GetComponent<Player_Animations>().lookDirection;
        GetMovementInput();
    }

    private void FixedUpdate()
    {
        //move the player
        //slower if looking the wrong way
        if (Vector2.Dot(lookDirection, playerRB.velocity) < 0)
        {
            playerRB.velocity = new Vector2(movement.x * speed * 0.7f, movement.y * speed * 0.7f);
        }
        else
        {
            playerRB.velocity = new Vector2(movement.x * speed, movement.y * speed);
        }


    }
    public bool Dash()
    {
        if (canDash)
        {
            canDash = false;
            StartCoroutine(DashEnumerator(0.10f));
            return true;
        }
        return false;
    }
    //get input for the player movement
    void GetMovementInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (!(moveX == 1 || moveX == -1 || moveY == 1 || moveY == -1))
        {
            animator.SetBool("Walking", false);
        }
        else
        {
            animator.SetBool("Walking", true);
        }

        //.normalized caps the vector length to 1, so that diagonal movement works properly
        movement = new Vector2(moveX, moveY).normalized;
    }
    public IEnumerator DashEnumerator(float DashTime)
    {
        yield return new WaitForSeconds(DashTime);
        playerRB.AddForce(playerRB.velocity.normalized * 10000f);
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;

    }
    public IEnumerator AttackStep(float waitTime, float force)
    {
        yield return new WaitForSeconds(waitTime);
        playerRB.AddForce(force * lookDirection);
    }

    public IEnumerator AttackSlow(float slowTime)
    {
        attacking = true;
        speed = speed * 0.25f;
        yield return new WaitForSeconds(slowTime);
        speed = baseMovementSpeed;
        attacking = false;
    }

}