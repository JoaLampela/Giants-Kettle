using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiButtonClick : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool inventoryLocked;

    public enum ItemType
    {
        Default,
        Weapon,
        ArmorHead,
        ArmorChest,
        ArmorLegs,
        Consumable,
        Rune
    }


    private Inventory playerInventory;
    private PlayerHoverUi playerHoverUi;

    [SerializeField] private Sprite defaultIcon;
    public ItemType _type;
    public Item _item;
    public Image icon;
    [SerializeField] private bool isEquipmentSlot;
    [SerializeField] private int slotNuber;
    [SerializeField] public RuneTooltipController runeTooltipController;
    private bool frameLocked = false;


    


    private void Awake()
    {
        playerHoverUi = GameObject.Find("Canvas").GetComponentInChildren<PlayerHoverUi>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<Inventory>();
        if(gameObject.transform.childCount >= 2)
        {
            if (gameObject.transform.GetChild(1).GetComponent<RuneTooltipController>())
            {
                runeTooltipController = gameObject.transform.GetChild(1).GetComponent<RuneTooltipController>();
            }
        }
        
    }
    private void Start()
    {
        if (!isEquipmentSlot) playerInventory.inventorySlots[slotNuber] = this;
        else playerInventory.equipmentSlots[slotNuber] = this;
        if(playerHoverUi != null) //Debug.Log("found");
        if (_item != null) icon.sprite = _item.item.iconSprite;

        if ((int)_type == 6) SetInvisible();
        Subscribe();
    }
    private void OnDisable()
    {
        //Unsubscribe();
    }

    private void Subscribe()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<EntityEvents>().OnLockInventory += LockInventory;
        player.GetComponent<EntityEvents>().OnUnlockInventory += UnlockInventory;
    }
    private void Unsubscribe()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<EntityEvents>().OnLockInventory -= LockInventory;
        player.GetComponent<EntityEvents>().OnUnlockInventory -= UnlockInventory;
    }

    private void LockInventory()
    {
        inventoryLocked = true;
    }
    private void UnlockInventory()
    {
        inventoryLocked = false;
    }

    public void PlaceItem(Item newItem, UiButtonClick previousSlot = null)
    {
        if(!inventoryLocked && !frameLocked)
        {
            if ((int)_type != 0 && _type != ItemType.Rune)
            {
                Debug.Log("!=0");
                if (_item != null) playerInventory.Unequip(_item, this);
            }
            if (this == playerInventory.rightHand || this == playerInventory.leftHand)
            {
                if (newItem.item.isTwoHander)
                {
                    if (_item != null)
                    {
                        Item temp;
                        if (_item.item.isTwoHander)
                        {
                            temp = playerInventory.leftHand._item;
                            playerInventory.rightHand.RemoveItemFromslot();
                            playerInventory.leftHand.RemoveItemFromslot();
                        }
                        else temp = _item;
                        playerInventory.NewItem(temp);
                        if (playerInventory.leftHand == this)
                        {
                            if (playerInventory.rightHand._item != null)
                            {
                                playerInventory.NewItem(playerInventory.rightHand._item);
                                playerInventory.Unequip(playerInventory.rightHand._item, playerInventory.rightHand);
                            }
                        }
                        else if (playerInventory.rightHand == this)
                        {
                            if (playerInventory.leftHand._item != null)
                            {
                                playerInventory.NewItem(playerInventory.leftHand._item);
                                playerInventory.Unequip(playerInventory.leftHand._item, playerInventory.leftHand);
                            }
                        }
                    }
                    else
                    {
                        if (this == playerInventory.rightHand)
                        {
                            if (playerInventory.leftHand._item != null)
                            {
                                Item temp = playerInventory.leftHand._item;
                                playerInventory.Unequip(temp, playerInventory.leftHand);
                                playerInventory.NewItem(temp);
                            }
                        }
                        else if (this == playerInventory.leftHand)
                        {
                            if (playerInventory.rightHand._item != null)
                            {
                                Item temp = playerInventory.rightHand._item;
                                playerInventory.Unequip(temp, playerInventory.rightHand);
                                playerInventory.NewItem(temp);
                            }
                        }
                    }

                    playerInventory.rightHand._item = newItem;
                    playerInventory.leftHand._item = newItem;
                    playerInventory.rightHand.icon.sprite = newItem.item.iconSprite;
                    playerInventory.leftHand.icon.sprite = newItem.item.iconSprite;
                    playerInventory.Equip(newItem, this);
                }
                else
                {
                    if (_item != null)
                    {
                        if (_item.item.isTwoHander)
                        {
                            Item temp = playerInventory.leftHand._item;
                            playerInventory.leftHand.RemoveItemFromslot();
                            playerInventory.rightHand.RemoveItemFromslot();
                            playerInventory.NewItem(temp);
                        }
                        else
                        {
                            Item temp = _item;
                            playerInventory.Unequip(temp, this);
                            RemoveItemFromslot();
                            playerInventory.NewItem(temp);
                        }
                        //playerInventory.Equip(newItem, this);
                        _item = newItem;
                        icon.sprite = newItem.item.iconSprite;
                        playerInventory.Equip(newItem, this);
                    }
                    else
                    {
                        //playerInventory.Equip(newItem, this);
                        _item = newItem;
                        icon.sprite = newItem.item.iconSprite;
                        playerInventory.Equip(newItem, this);
                    }
                }
            }
            else
            {
                if (_item != null)
                {
                    playerInventory.RemoveRuneFromItem(gameObject);
                    if ((int)previousSlot._type != 0)
                    {
                        Debug.Log("REMOVING RUNE");

                        if ((int)previousSlot._type == (int)_item.item.type) previousSlot.PlaceItem(_item);
                        else playerInventory.NewItem(_item);
                    }
                    else
                    {
                        previousSlot.PlaceItem(_item);
                    }
                    _item = null;
                }
                _item = newItem;
                icon.sprite = _item.item.iconSprite;
                if ((int)newItem.item.type != (int)ItemType.Rune)
                {
                    if (this._type != 0) playerInventory.Equip(newItem, this);
                }
            }
            if (_type == ItemType.Rune)
            {
                playerInventory.AddNewRuneToItem(newItem, gameObject);
            }
            SetRuneToolTipOn();
        }
        else
        {
            playerInventory.NewItem(newItem);
        }
        

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!inventoryLocked) 
        {
            if (runeTooltipController != null) runeTooltipController.HideToolTip();
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                Debug.Log("Right Click");
                if ((int)_type != 0 && _item != null && _type != ItemType.Rune)
                {
                    playerInventory.Unequip(_item, this);
                    if (_item.item.isTwoHander)
                    {
                        if (this == playerInventory.rightHand)
                        {
                            Item temp = playerInventory.leftHand._item;
                            playerInventory.leftHand.RemoveItemFromslot();
                            playerInventory.rightHand.RemoveItemFromslot();
                            if (Input.GetKey(KeyCode.LeftShift)) playerInventory.DropItem(temp, playerInventory.gameObject.transform.position);
                            else playerInventory.NewItem(temp);
                        }
                        else if (this == playerInventory.leftHand)
                        {
                            Item temp = playerInventory.rightHand._item;
                            playerInventory.rightHand.RemoveItemFromslot();
                            playerInventory.leftHand.RemoveItemFromslot();
                            if (Input.GetKey(KeyCode.LeftShift)) playerInventory.DropItem(temp, playerInventory.gameObject.transform.position);
                            else playerInventory.NewItem(temp);
                        }
                    }
                    else if (playerInventory.InventoryHasRoom())
                    {
                        Item temp = _item;
                        RemoveItemFromslot();
                        Debug.Log("type " + _type);
                        if (Input.GetKey(KeyCode.LeftShift) && !frameLocked && _type != ItemType.Rune) playerInventory.DropItem(temp, playerInventory.gameObject.transform.position);
                        else playerInventory.NewItem(temp);
                    }
                }
                else if (_item != null && (int)_item.item.type != (int)ItemType.Rune)
                {
                    Item temp = _item;
                    RemoveItemFromslot();
                    if (Input.GetKey(KeyCode.LeftShift)) playerInventory.DropItem(temp, playerInventory.gameObject.transform.position);
                    else playerInventory.UseItem(temp);
                }
                else
                {
                    Item temp = _item;

                    if (Input.GetKey(KeyCode.LeftShift) && !frameLocked && _type != ItemType.Rune)
                    {
                        RemoveItemFromslot();
                        playerInventory.DropItem(temp, playerInventory.gameObject.transform.position);
                    }
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Left)
            {
                Debug.Log("Left click inv");
                if (_item != null)
                {
                    Debug.Log("item not null " + _type + " " + (this == playerInventory.rightHand));

                    if (this == playerInventory.rightHand && _item.item.isTwoHander)
                    {
                        playerInventory.leftHand.RemoveItemFromslot();
                        playerInventory.Unequip(_item, this);
                        playerHoverUi.SetGrabbedItem(_item, this);
                        RemoveItemFromslot();
                    }
                    else if (this == playerInventory.leftHand && _item.item.isTwoHander)
                    {
                        playerInventory.rightHand.RemoveItemFromslot();
                        playerInventory.Unequip(_item, this);
                        playerHoverUi.SetGrabbedItem(_item, this);
                        RemoveItemFromslot();
                    }
                    else if ((int)_type != 0 && _type != ItemType.Rune)
                    {
                        Debug.Log(_item);
                        playerInventory.Unequip(_item, this);
                        playerHoverUi.SetGrabbedItem(_item, this);
                        RemoveItemFromslot();
                    }
                    else if (_type == ItemType.Rune && !frameLocked)
                    {
                        playerInventory.RemoveRuneFromItem(gameObject);
                        playerHoverUi.SetGrabbedItem(_item, this);
                        RemoveItemFromslot();
                    }
                    else if(!frameLocked)
                    {
                        playerHoverUi.SetGrabbedItem(_item, this);
                        RemoveItemFromslot();
                    }
                    
                }
            }
        }
        
    }

    public void HotbarUseItem()
    {
        if(!inventoryLocked)
        {
            if (runeTooltipController != null) runeTooltipController.HideToolTip();
            if ((int)_type != 0 && _item != null && _type != ItemType.Rune)
            {
                playerInventory.Unequip(_item, this);
                if (_item.item.isTwoHander)
                {
                    if (this == playerInventory.rightHand)
                    {
                        Item temp = playerInventory.leftHand._item;
                        playerInventory.leftHand.RemoveItemFromslot();
                        playerInventory.rightHand.RemoveItemFromslot();
                        playerInventory.NewItem(temp);
                    }
                    else if (this == playerInventory.leftHand)
                    {
                        Item temp = playerInventory.rightHand._item;
                        playerInventory.rightHand.RemoveItemFromslot();
                        playerInventory.leftHand.RemoveItemFromslot();
                        playerInventory.NewItem(temp);
                    }
                }
                else if (playerInventory.InventoryHasRoom())
                {
                    Item temp = _item;
                    RemoveItemFromslot();
                    playerInventory.NewItem(temp);
                }
            }
            else if (_item != null && (int)_item.item.type != (int)ItemType.Rune)
            {
                Item temp = _item;
                RemoveItemFromslot();
                playerInventory.UseItem(temp);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        playerHoverUi.UpdateHoveredSlot(this);
        SetRuneToolTipOn();
    }

    public void SetRuneToolTipOn()
    {
        if (runeTooltipController != null) runeTooltipController.HideToolTip();
        if (playerHoverUi.hoveredSlot == this)
        {
            if (runeTooltipController != null)
            {
                if (_item != null) runeTooltipController.DisplayToolTip();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (runeTooltipController != null) runeTooltipController.HideToolTip();
    }   

    public void SetNewItemToslot(Item newItem)
    {
        _item = newItem;
        icon.sprite = newItem.item.iconSprite;
    }
    public void RemoveItemFromslot()
    {
        _item = null;
        icon.sprite = defaultIcon;
    }

    public void SetVisible()
    {
        gameObject.SetActive(true);
    }
    public void SetInvisible()
    {
        gameObject.SetActive(false);
    }

    [System.Obsolete]
    public void SetLockOn()
    {
        frameLocked = true;
        gameObject.transform.GetChild(1).gameObject.active = true;
    }

    [System.Obsolete]
    public void SetLockOff()
    {
        frameLocked = false;
        gameObject.transform.GetChild(1).gameObject.active = false;
    }
}
