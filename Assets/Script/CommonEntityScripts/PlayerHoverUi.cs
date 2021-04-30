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
    public event Action<ItemObject, int> OnDropItem;
    public GameObject flyingIcon;
    private Inventory playerInventory;
    [SerializeField] private Sprite empty;
    GameEventManager gameManager;
    [SerializeField] private InspectorPanel inspector;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<Inventory>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameEventManager>();
    }


    public void UpdateHoveredSlot(UiButtonClick slot)
    {
        hoveredSlot = slot;
    }

    // Update is called once per frame
    [Obsolete]
    void Update()
    {
        if(hoveredSlot == null)
        {
            if(gameManager.castingLocked) gameManager.castingLocked = false;

            inspector.DisapleInspector();
        }
        else if(hoveredSlot != null)
        {
            if(!gameManager.castingLocked) gameManager.castingLocked = true;
            
            if (hoveredSlot._item != null)
            {
                inspector.SetPreviewImage(hoveredSlot._item.item.iconSprite);
                inspector.EnableInspector();
                inspector.InspectorSetRuneSlots(hoveredSlot._item);
                inspector.InspectorSetRuneEffects(hoveredSlot._item);
            }
            else inspector.DisapleInspector();
        }
        if(Input.GetMouseButtonUp(0) && grabbedItem != null)
        {
            if (hoveredSlot != null)
            {
                if(hoveredSlot._type != 0)
                {
                    if ((int)hoveredSlot._type == (int)grabbedItem.item.type) PlaceItem();
                    else
                    {
                        playerInventory.NewItem(grabbedItem);
                        grabbedItem = null;
                        grabbedItemSlot = null;
                        flyingIcon.SetActive(false);
                    }
                }
                else if(hoveredSlot._item != null)
                {
                    Debug.Log("Item slot was not empty");
                    if (grabbedItem.item.type == ItemType.Rune)
                    {
                        bool slotFound = false;
                        for (int i = 0; i < hoveredSlot._item._runeList.Length; i++)
                        {
                            if (hoveredSlot._item._runeList[i] == null)
                            {
                                slotFound = true;
                                hoveredSlot._item._runeList[i] = (RuneObject)grabbedItem.item;
                                break;
                            }
                        }
                        if (!slotFound)
                        {
                            playerInventory.NewItem(grabbedItem);
                        }
                        else
                        {
                            if (hoveredSlot.runeTooltipController != null)
                            {
                                hoveredSlot.runeTooltipController.HideToolTip();
                                hoveredSlot.runeTooltipController.DisplayToolTip();
                            }
                            
                        }
                        grabbedItem = null;
                        grabbedItemSlot = null;
                        flyingIcon.SetActive(false);
                    }
                    else PlaceItem();
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
        flyingIcon.GetComponent<Image>().sprite = empty;
        flyingIcon.SetActive(true);
        flyingIcon.GetComponent<Image>().sprite = grabbedItem.item.iconSprite;
        
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
        if (hoveredSlot.runeTooltipController != null)
        {
            if (hoveredSlot._item != null) hoveredSlot.runeTooltipController.DisplayToolTip();
        }
        grabbedItem = null;
        grabbedItemSlot = null;
        flyingIcon.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoveredSlot = null;
    }
}
