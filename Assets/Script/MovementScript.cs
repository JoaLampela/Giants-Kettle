using System.Collections;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float speed = 8;
    public Camera playerCamera;
    public BoxCollider2D swordCollider;


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
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        baseMovementSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        lookDirection = GetComponent<Player_Weapon>().lookDirection;
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
    public IEnumerator AttackSlow(float slowTime)
    {
        attacking = true;
        speed = speed * 0.25f;
        yield return new WaitForSeconds(slowTime);
        speed = baseMovementSpeed;
        attacking = false;
    }

}
