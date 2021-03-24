using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float speed = 10;
    public GameObject rightArm;
    public Camera playerCamera;
    private Vector2 movement;
    Rigidbody2D playerRB;
    Animator animator;
    private bool walkingBackWards;
    private bool attacking;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
    }

    private void FixedUpdate()
    {
        //move the player

        playerRB.velocity = new Vector2(movement.x * speed, movement.y * speed);

    }

    //get input for the player movement
    void GetMovementInput()
    {

        if (Input.GetMouseButtonDown(0) && !attacking)
        {
            animator.SetTrigger("Player_Swing");
            Debug.Log("Swing sword pog");
            attacking = true;
            StartCoroutine(swingAnimatonCoolDown(0.4f));
        }
        else if (!attacking)
        {
            Vector2 difference = playerCamera.ScreenToWorldPoint(Input.mousePosition) - rightArm.transform.position;
            difference.Normalize();
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + 90f;
            if (rotationZ < 0 || rotationZ > 180)
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            else
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            rightArm.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        //.normalized caps the vector length to 1, so that diagonal movement works properly
        if (moveX == 1 || moveX == -1 || moveY == 1 || moveY == -1)
        {
            animator.SetBool("Walking", true);
            if (moveX == 1)
                walkingBackWards = false;
            else if (moveX == -1)
                walkingBackWards = true;


        }
        else
        {
            animator.SetBool("Walking", false);
        }

        movement = new Vector2(moveX, moveY).normalized;
    }

    IEnumerator swingAnimatonCoolDown(float CoolDown)
    {
        yield return new WaitForSeconds(CoolDown);
        attacking = false;
    }
}
