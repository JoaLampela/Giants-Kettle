using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float speed = 10;
    private Vector2 movement;
    Rigidbody playerRB;
    //Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();

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

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

       
        if (moveX == 1 || moveX == -1 || moveY == 1 || moveY == -1)
        {
            //animator.SetBool("Walking", true);
            if (moveX == 1 || moveX == -1)
                gameObject.transform.localScale = new Vector3(moveX, 1, 1);
        }
        else
        {
            //animator.SetBool("Walking", false);
        }

        //.normalized caps the vector length to 1, so that diagonal movement works properly
        movement = new Vector2(moveX, moveY).normalized;
    }
}
