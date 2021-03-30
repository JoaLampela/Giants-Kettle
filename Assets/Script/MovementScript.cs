using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float speed = 8;
    float originalMovmentSpeed;
    public GameObject rightArm;
    public Camera playerCamera;
    private Vector2 movement;
    Rigidbody2D playerRB;
    private bool walkingBackWards;
    private bool attacking = false;
    private Animator animator;
    private Vector2 lookDirection;
    public BoxCollider2D swordCollider;
    public int lastHorizontalAxisRaw;



    //Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lastHorizontalAxisRaw = 0;
        originalMovmentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
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

    //get input for the player movement
    void GetMovementInput()
    {

        if (Input.GetMouseButtonDown(0) && !attacking)
        {
            animator.SetTrigger("Player_Swing");
            //Debug.Log("Swing sword pog");
            attacking = true;
            speed = speed / 1.5f;
            StartCoroutine(swingAnimationCoolDown(0.5f));
            swordCollider.enabled = true;
        }
        else if (!attacking)
        {

            Vector2 difference = playerCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, playerCamera.transform.position.z)) - rightArm.transform.position;
            difference.Normalize();
            lookDirection = difference;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + 90f;
            //Debug.Log(rotationZ);
            if (rotationZ >= 180 || rotationZ <= 0)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            rightArm.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveY != lastHorizontalAxisRaw)
        {
            if (moveY == -1)
            {

            }
            else if (moveY == 1)
            {

            }
        }

        if (moveX == 1 || moveX == -1 || moveY == 1 || moveY == -1)
        {
            animator.SetBool("Walking", true);
            if (moveY == 1)
                walkingBackWards = false;
            else if (moveY == -1)
                walkingBackWards = true;
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        //.normalized caps the vector length to 1, so that diagonal movement works properly
        movement = new Vector2(moveX, moveY).normalized;
    }

    IEnumerator swingAnimationCoolDown(float CoolDown)
    {
        yield return new WaitForSeconds(CoolDown);
        speed = originalMovmentSpeed;
        attacking = false;
        swordCollider.enabled = false;
    }
}
