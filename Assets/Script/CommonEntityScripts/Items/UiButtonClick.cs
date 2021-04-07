using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiButtonClick : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
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
    
    public ItemType _type;
    public Item _item;
    public Image icon;
    [SerializeField] private bool isEquipmentSlot;


    


    private void Awake()
    {
        playerHoverUi = GameObject.Find("Canvas").GetComponentInChildren<PlayerHoverUi>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<Inventory>();
    }
    private void Start()
    {
        if (!isEquipmentSlot) playerInventory.inventorySlots.Add(this);
        else playerInventory.equipmentSlots.Add(this);
        if(playerHoverUi != null) //Debug.Log("found");
        if (_item != null) icon.sprite = _item.item.iconSprite;

        if ((int)_type == 6) SetInvisible();
    }

    public void PlaceItem(Item newItem, UiButtonClick previousSlot = null)
    {
        if ((int)_type != 0)
        {
            Debug.Log("!=0");
            playerInventory.Equip(newItem, this);
            if (_item != null) playerInventory.Unequip(_item, this);
        }
        if(this == playerInventory.rightHand || this == playerInventory.leftHand)
        {
            if(newItem.item.isTwoHander)
            {
                if(_item != null)
                {
                    Item temp;
                    if (_item.item.isTwoHander) {
                        temp = playerInventory.leftHand._item;
                        playerInventory.rightHand._item = null;
                        playerInventory.leftHand._item = null;
                    }
                    else temp = _item;
                    playerInventory.NewItem(temp);
                    if (playerInventory.leftHand == this)
                    {
                        if (playerInventory.rightHand._item != null) playerInventory.NewItem(playerInventory.rightHand._item);
                    }
                    else if (playerInventory.rightHand == this)
                    {
                        if (playerInventory.leftHand._item != null) playerInventory.NewItem(playerInventory.leftHand._item);
                    }
                }
                else
                {
                    if (this == playerInventory.rightHand)
                    {
                        if(playerInventory.leftHand._item != null)
                        {
                            Item temp = playerInventory.leftHand._item;
                            playerInventory.Unequip(temp, this);
                            playerInventory.NewItem(temp);
                        }
                    }
                    else if (this == playerInventory.leftHand)
                    {
                        if (playerInventory.rightHand._item != null)
                        {
                            Item temp = playerInventory.rightHand._item;
                            playerInventory.Unequip(temp, this);
                            playerInventory.NewItem(temp);
                        }
                    }
                }
                playerInventory.rightHand._item = newItem;
                playerInventory.leftHand._item = newItem;
                playerInventory.rightHand.icon.sprite = newItem.item.iconSprite;
                playerInventory.leftHand.icon.sprite = newItem.item.iconSprite;
            }
            else
            {
                if(_item != null)
                {
                    if(_item.item.isTwoHander)
                    {
                        Item temp = playerInventory.leftHand._item;
                        playerInventory.rightHand._item = null;
                        playerInventory.leftHand._item = null;
                        playerInventory.rightHand.icon.sprite = null;
                        playerInventory.leftHand.icon.sprite = null;
                        playerInventory.NewItem(temp);
                    }
                    else
                    {
                        Item temp = _item;
                        playerInventory.Unequip(temp, this);
                        _item = null;
                        icon.sprite = null;
                        playerInventory.NewItem(temp);
                    }
                    playerInventory.Equip(newItem, this);
                    _item = newItem;
                    icon.sprite = newItem.item.iconSprite;
                }
                else
                {
                    playerInventory.Equip(newItem, this);
                    _item = newItem;
                    icon.sprite = newItem.item.iconSprite;
                }
            }
        }
        else
        {
            if(_item != null)
            {
                if((int)previousSlot._type != 0)
                {
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
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if((int)_type != 0 && _item != null)
            {
                playerInventory.Unequip(_item, this);
                if(_item.item.isTwoHander)
                {
                    if(this == playerInventory.rightHand)
                    {
                        Item temp = playerInventory.leftHand._item;
                        playerInventory.leftHand._item = null;
                        playerInventory.leftHand.icon.sprite = null;
                        playerInventory.rightHand._item = null;
                        playerInventory.rightHand.icon.sprite = null;
                        playerInventory.NewItem(temp);
                    }
                    else if (this == playerInventory.leftHand)
                    {
                        Item temp = playerInventory.rightHand._item;
                        playerInventory.rightHand._item = null;
                        playerInventory.rightHand.icon.sprite = null;
                        playerInventory.leftHand._item = null;
                        playerInventory.leftHand.icon.sprite = null;
                        playerInventory.NewItem(temp);
                    }
                }
                else if(playerInventory.InventoryHasRoom())
                {
                    Item temp = _item;
                    _item = null;
                    icon.sprite = null;
                    playerInventory.NewItem(temp);
                }
            }
            else if (_item != null)
            {
                Item temp = _item;
                icon.sprite = null;
                _item = null;
                playerInventory.UseItem(temp);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (_item != null)
            {
                if(this == playerInventory.rightHand && _item.item.isTwoHander)
                {
                    playerInventory.leftHand._item = null;
                    playerInventory.leftHand.icon.sprite = null;
                }
                else if (this == playerInventory.leftHand && _item.item.isTwoHander)
                {
                    playerInventory.rightHand._item = null;
                    playerInventory.rightHand.icon.sprite = null;
                }
                if ((int)_type != 0) playerInventory.Unequip(_item, this);
                playerHoverUi.SetGrabbedItem(_item, this);
                icon.sprite = null;
                _item = null;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        playerHoverUi.UpdateHoveredSlot(this);
    }

    public void SetNewItemToslot(Item newItem)
    {
        _item = newItem;
        icon.sprite = newItem.item.iconSprite;
    }
    public void RemoveItemFromslot()
    {
        _item = null;
        icon.sprite = null;
    }

    public void SetVisible()
    {
        gameObject.SetActive(true);
    }
    public void SetInvisible()
    {
        gameObject.SetActive(false);
    }
}
