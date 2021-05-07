using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnGround : MonoBehaviour
{
    public Item _item;
    private Inventory playerInventory;
    public bool pickedUp = false;
    private Rigidbody2D rb;
    private GameObject player;
    private float speed = 20;
    private float currentY = 0;
    private bool goingUp = true;
    private bool smoothGoingUp = true;
    private Vector2 startPos;
    private float smoorthValue = 0;
    private float pickUpDistance = 10;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<Inventory>();
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    private void Start()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        GameObject.Find("Game Manager").GetComponent<GameEventManager>().OnExitLevel -= DestroyGameObject;
    }

    private void Subscribe()
    {
        GameObject.Find("Game Manager").GetComponent<GameEventManager>().OnExitLevel += DestroyGameObject;
    }
    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1) && !pickedUp && Vector2.Distance(player.transform.position,transform.position) < pickUpDistance)
        {
            pickedUp = true;
        }
    }
    private void Update()
    {
        if(pickedUp)
        {
            rb.velocity = (player.transform.position - transform.position).normalized * speed;
        }
        else
        {
            ItemPopping();
        }
    }

    private void ItemPopping()
    {
        
        float moveValueY = 0.5f;
        float cycleTime = 2f;
        if(goingUp)
        {
            currentY += (moveValueY * smoorthValue / cycleTime) * Time.deltaTime;
            if (currentY >= moveValueY)
            {
                goingUp = false;
                smoorthValue = 0;
            }
        }
        else
        {
            currentY -= (moveValueY / cycleTime) * Time.deltaTime;
            if (currentY <= 0)
            {
                goingUp = true;
                smoorthValue = 0;
            }
        }
        transform.position = new Vector2(startPos.x, startPos.y + currentY);
        if (smoothGoingUp)
        {
            smoorthValue += 1f * Time.deltaTime;
        }
        else
        {
            smoorthValue -= 1f * Time.deltaTime;
        }
        if(smoorthValue > 2)
        {
            smoothGoingUp = false;
        }
        if(smoorthValue < 0)
        {
            smoothGoingUp = true;
        }

    }

    public void SetItem(Item newItem)
    {
        //GetComponent<SpriteRenderer>().sprite = newItem.item.iconSprite;
        _item = newItem;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject == player && pickedUp)
        {
            playerInventory.NewItem(_item);
            Destroy(gameObject);
        }
        if(collision.tag == "Walls")
        {
            pickedUp = true;
        }
    }
}
