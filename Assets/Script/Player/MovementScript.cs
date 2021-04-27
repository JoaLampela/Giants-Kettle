using System.Collections;
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
    private EntityEvents events;


    private void Awake()
    {
        SoundManager.Initialize();
        events = GetComponent<EntityEvents>();
    }

    //Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        canDash = true;
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        baseMovementSpeed = speed;
        Subscribe();
    }
    private void OnDisable()
    {
        Unsubscribe();
    }
    private void Subscribe()
    {
        events.OnTakeStep += TakeStep;
    }
    private void Unsubscribe()
    {
        events.OnTakeStep -= TakeStep;
    }
    private void TakeStep(int distance)
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Debug.Log(dir);
        Vector2 distanceVector = dir.normalized * distance;
        playerRB.AddForce(distanceVector);
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
        speed = GetComponent<EntityStats>().currentSpeed/100f;

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

        bool isIdle = moveX == 0 && moveY == 0;
        if (!isIdle)
        {
            SoundManager.PlaySound(SoundManager.Sound.PlayerMove, transform.position);
        }
    }
    
    public void AttackStep(float force)
    {
        playerRB.AddForce(force * lookDirection);
    }

    public void StartAttackSlow()
    {
        attacking = true;
        speed = speed * 0.25f;
    }
    public void StopAttackSlow()
    {
        attacking = false;
        speed = baseMovementSpeed;
    }

}
