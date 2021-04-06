using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnGround : MonoBehaviour
{
    public Item _item;
    private Inventory playerInventory;

    private void Awake()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log("Picked up " + _item.item.name);
            playerInventory.NewItem(_item);
            Destroy(gameObject);
        }
    }

    public void SetItem(Item newItem)
    {
        GetComponent<SpriteRenderer>().sprite = newItem.item.iconSprite;
        _item = newItem;
    }
}
