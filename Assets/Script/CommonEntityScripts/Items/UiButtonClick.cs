using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiButtonClick : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    private Inventory playerInventory;
    private PlayerHoverUi playerHoverUi;
    
    public int _slot;
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
        if(playerHoverUi != null) Debug.Log("found");
        if (_item) icon.sprite = _item.sprite; 
    }

    public void PlaceItem(Item item, UiButtonClick previousSlot = null)
    {
        if (_slot != 0)
        {
            Debug.Log("!=0");
            playerInventory.Equip(item);
            if (_item != null) playerInventory.Unequip(_item);
        }
        if(this == playerInventory.rightHand || this == playerInventory.leftHand)
        {
            if(item.isTwoHander)
            {
                if(_item != null)
                {
                    Item temp;
                    if (_item.isTwoHander) {
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
                            playerInventory.Unequip(temp);
                            playerInventory.NewItem(temp);
                        }
                    }
                    else if (this == playerInventory.leftHand)
                    {
                        if (playerInventory.rightHand._item != null)
                        {
                            Item temp = playerInventory.rightHand._item;
                            playerInventory.Unequip(temp);
                            playerInventory.NewItem(temp);
                        }
                    }
                }
                playerInventory.rightHand._item = item;
                playerInventory.leftHand._item = item;
                playerInventory.rightHand.icon.sprite = item.sprite;
                playerInventory.leftHand.icon.sprite = item.sprite;
            }
            else
            {
                if(_item != null)
                {
                    if(_item.isTwoHander)
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
                        _item = null;
                        icon.sprite = null;
                        playerInventory.NewItem(temp);
                    }
                    _item = item;
                    icon.sprite = item.sprite;
                }
                else
                {
                    _item = item;
                    icon.sprite = item.sprite;
                }
            }
        }
        else
        {
            if(_item != null)
            {
                if(previousSlot._slot != 0)
                {
                    if (previousSlot._slot == _item.inventorySlot) previousSlot.PlaceItem(_item);
                    else playerInventory.NewItem(_item);
                }
                else
                {
                    previousSlot.PlaceItem(_item);
                }
                _item = null;
            }
            _item = item;
            icon.sprite = _item.sprite;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(_slot != 0 && _item != null)
            {
                playerInventory.Unequip(_item);
                if(_item.isTwoHander)
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
                if(this == playerInventory.rightHand && _item.isTwoHander)
                {
                    playerInventory.leftHand._item = null;
                    playerInventory.leftHand.icon.sprite = null;
                }
                else if (this == playerInventory.leftHand && _item.isTwoHander)
                {
                    playerInventory.rightHand._item = null;
                    playerInventory.rightHand.icon.sprite = null;
                }
                if (_slot != 0) playerInventory.Unequip(_item);
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
}
