using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class inventory : MonoBehaviour
{
    private EntityEvents events;

    public List<UiButtonClick> inventorySlots = new List<UiButtonClick>();
    public List<UiButtonClick> equipmentSlots = new List<UiButtonClick>();
    public UiButtonClick rightHand;
    public UiButtonClick leftHand;
    public int testEquipmentCount = 0;
    public GameObject itemOnGround;

    private void Awake()
    {
        events = GetComponent<EntityEvents>();
    }


    public bool InventoryHasRoom()
    {
        foreach(UiButtonClick inventorySlot in inventorySlots)
        {
            if (inventorySlot._item == null)
            {
                return true;
            }
        }
        return false;
    }

    public void NewItem(Item item)
    {
        bool slotFound = false;
        foreach (UiButtonClick inventorySlot in inventorySlots)
        {
            if (inventorySlot._item == null)
            {
                inventorySlot._item = item;
                inventorySlot.icon.sprite = item.sprite;
                slotFound = true;
                break;
            }
        }
        if (!slotFound) DropItem(item);
    }

    public void UseItem(Item item)
    {
        if(item.inventorySlot == -1)
        {
            UseConsumable(item);
        }
        else
        {
            Debug.Log("Used item");
            foreach (UiButtonClick inventorySlot in equipmentSlots)
            {
                if (inventorySlot._slot == item.inventorySlot)
                {
                    if (item.isTwoHander)
                    {
                        if (rightHand._item != null)
                        {
                            if (rightHand._item.isTwoHander)
                            {
                                leftHand._item = null;
                                leftHand.icon.sprite = null;
                            }
                            Unequip(rightHand._item);
                            NewItem(rightHand._item);
                            rightHand._item = null;
                            rightHand.icon.sprite = null;
                        }
                        if (leftHand._item != null)
                        {
                            if (leftHand._item.isTwoHander)
                            {
                                rightHand._item = null;
                                rightHand.icon.sprite = null;
                            }
                            Unequip(leftHand._item);
                            NewItem(leftHand._item);
                            leftHand._item = null;
                            leftHand.icon.sprite = null;
                        }
                        Equip(item);
                        rightHand._item = item;
                        leftHand._item = item;
                        rightHand.icon.sprite = item.sprite;
                        leftHand.icon.sprite = item.sprite;
                    }
                    else if (inventorySlot._item != null)
                    {
                        if (inventorySlot._item.isTwoHander)
                        {
                            Equip(item);
                            rightHand._item = item;
                            rightHand.icon.sprite = item.sprite;

                            Item temp = leftHand._item;
                            leftHand._item = null;
                            leftHand.icon.sprite = null;
                            Unequip(temp);
                            NewItem(temp);
                        }


                        else if (inventorySlot == rightHand)
                        {
                            Item temp = rightHand._item;
                            Equip(item);
                            rightHand._item = item;
                            rightHand.icon.sprite = item.sprite;
                            item = temp;
                            if (leftHand._item != null)
                            {
                                temp = leftHand._item;
                                leftHand._item = item;
                                leftHand.icon.sprite = item.sprite;
                                Unequip(temp);
                                NewItem(temp);
                            }
                            else
                            {
                                leftHand._item = item;
                                leftHand.icon.sprite = item.sprite;
                            }
                        }
                        else
                        {
                            Item temp = inventorySlot._item;
                            inventorySlot._item = item;
                            inventorySlot.icon.sprite = item.sprite;
                            NewItem(temp);
                        }

                    }
                    else
                    {
                        Equip(item);
                        inventorySlot._item = item;
                        inventorySlot.icon.sprite = item.sprite;
                    }
                    break;
                }
            }
        }
    }

    public void Unequip(Item item)
    {
        Debug.Log("Unequipped " + item.name);
        testEquipmentCount--;


        foreach (ItemBuff buff in item.buffs)
        {
            events.RemoveBuff(buff.sourceID);
        }
        if(item.scriptName != "") Destroy(GetComponent(Type.GetType(item.scriptName)));
    }
    public void Equip(Item item)
    {
        Debug.Log("Equipped " + item.name);
        testEquipmentCount++;

        foreach (ItemBuff buff in item.buffs)
        {
            events.NewBuff(buff.sourceID, buff.effectID, buff.effectiveness);
        }
        if (item.scriptName != "") gameObject.AddComponent(Type.GetType(item.scriptName));
    }

    public void UseConsumable(Item item)
    {
        Debug.Log("Used " + item.name);
        foreach (ItemBuff buff in item.buffs)
        {
            events.NewBuff(buff.sourceID, buff.effectID, buff.effectiveness, buff.duration);
        }
        if(item.scriptName != "") gameObject.AddComponent(Type.GetType(item.scriptName));
    }

    public void DropItem(Item item)
    {
        Debug.Log("Dropped item " + item.name);
        GameObject groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
        groundItem.GetComponent<ItemOnGround>().item = item;
        groundItem.GetComponent<SpriteRenderer>().sprite = item.sprite;
    }
}
