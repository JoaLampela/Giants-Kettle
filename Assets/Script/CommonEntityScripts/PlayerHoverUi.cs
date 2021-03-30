using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerHoverUi : MonoBehaviour, IPointerExitHandler
{
    public UiButtonClick hoveredSlot = null;
    private Item grabbedItem = null;
    private UiButtonClick grabbedItemSlot = null;
    public event Action<Item, int> OnDropItem;
    public GameObject flyingIcon;
    private inventory playerInventory;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<inventory>();
    }


    public void UpdateHoveredSlot(UiButtonClick slot)
    {
        hoveredSlot = slot;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && grabbedItem != null)
        {
            if (hoveredSlot != null)
            {
                if(hoveredSlot._slot != 0)
                {
                    if (hoveredSlot._slot == grabbedItem.inventorySlot) PlaceItem();
                    else
                    {
                        playerInventory.NewItem(grabbedItem);
                        grabbedItem = null;
                        grabbedItemSlot = null;
                        flyingIcon.SetActive(false);
                    }
                }
                else PlaceItem();
            }
            else DropItem();
        }
    }
    public void SetGrabbedItem(Item item, UiButtonClick slot)
    {
        grabbedItem = item;
        grabbedItemSlot = slot;
        flyingIcon.GetComponent<Image>().sprite = grabbedItem.sprite;
        flyingIcon.SetActive(true);
    }
    private void DropItem()
    {
        playerInventory.DropItem(grabbedItem);
        grabbedItem = null;
        grabbedItemSlot = null;
        flyingIcon.SetActive(false);
    }
    private void PlaceItem()
    {
        hoveredSlot.PlaceItem(grabbedItem, grabbedItemSlot);
        grabbedItem = null;
        grabbedItemSlot = null;
        flyingIcon.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoveredSlot = null;
    }
}
