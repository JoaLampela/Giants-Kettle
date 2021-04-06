using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public enum ItemType
    {
        Default,
        Weapon,
        Armor,
        Consumable,
        Rune
    }

    private EntityEvents events;

    public List<UiButtonClick> inventorySlots = new List<UiButtonClick>();
    public List<UiButtonClick> equipmentSlots = new List<UiButtonClick>();
    public UiButtonClick rightHand;
    public UiButtonClick leftHand;
    public int testEquipmentCount = 0;
    public GameObject itemOnGround;
    public EntityAbilityManager abilityManager;

    private void Awake()
    {
        events = GetComponent<EntityEvents>();
        abilityManager = GetComponent<EntityAbilityManager>();
    }


    public bool InventoryHasRoom()
    {
        foreach (UiButtonClick inventorySlot in inventorySlots)
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
                inventorySlot.icon.sprite = item.item.iconSprite;
                slotFound = true;
                break;
            }
        }
        if (!slotFound) DropItem(item);
    }

    public void UseItem(Item usedItem)
    {
        if ((int)usedItem.item.type == 3)
        {
            UseConsumable(usedItem);
        }
        else
        {
            Debug.Log("Used item");
            foreach (UiButtonClick inventorySlot in equipmentSlots)
            {
                if ((int)inventorySlot._type == (int)usedItem.item.type)
                {
                    if (usedItem.item.isTwoHander)
                    {
                        if (rightHand._item != null)
                        {
                            if (rightHand._item.item.isTwoHander)
                            {
                                leftHand._item = null;
                                leftHand.icon.sprite = null;
                            }
                            Unequip(rightHand._item, rightHand);
                            NewItem(rightHand._item);
                            rightHand._item = null;
                            rightHand.icon.sprite = null;
                        }
                        if (leftHand._item != null)
                        {
                            if (leftHand._item.item.isTwoHander)
                            {
                                rightHand._item = null;
                                rightHand.icon.sprite = null;
                            }
                            Unequip(leftHand._item, leftHand);
                            NewItem(leftHand._item);
                            leftHand._item = null;
                            leftHand.icon.sprite = null;
                        }
                        Equip(usedItem, inventorySlot);
                        rightHand._item = usedItem;
                        leftHand._item = usedItem;
                        rightHand.icon.sprite = usedItem.item.iconSprite;
                        leftHand.icon.sprite = usedItem.item.iconSprite;
                    }
                    else if (inventorySlot._item != null)
                    {
                        if (inventorySlot._item.item.isTwoHander)
                        {
                            Equip(usedItem, inventorySlot);
                            rightHand._item = usedItem;
                            rightHand.icon.sprite = usedItem.item.iconSprite;

                            Item temp = leftHand._item;
                            leftHand._item = null;
                            leftHand.icon.sprite = null;
                            Unequip(temp, inventorySlot);
                            NewItem(temp);
                        }


                        else if (inventorySlot == rightHand)
                        {
                            Item temp = rightHand._item;
                            Unequip(temp, rightHand);
                            Equip(usedItem, inventorySlot);
                            rightHand._item = usedItem;
                            rightHand.icon.sprite = usedItem.item.iconSprite;
                            usedItem = temp;
                            if (leftHand._item != null)
                            {
                                temp = leftHand._item;
                                leftHand._item = usedItem;
                                Unequip(temp, inventorySlot);
                                Equip(usedItem, leftHand);
                                leftHand.icon.sprite = usedItem.item.iconSprite;
                                NewItem(temp);
                            }
                            else
                            {
                                leftHand._item = usedItem;
                                Equip(usedItem, leftHand);
                                leftHand.icon.sprite = usedItem.item.iconSprite;
                            }
                        }
                        else
                        {
                            Item temp = inventorySlot._item;
                            inventorySlot._item = usedItem;
                            inventorySlot.icon.sprite = usedItem.item.iconSprite;
                            NewItem(temp);
                        }

                    }
                    else
                    {
                        Equip(usedItem, inventorySlot);
                        inventorySlot._item = usedItem;
                        inventorySlot.icon.sprite = usedItem.item.iconSprite;
                    }
                    break;
                }
            }
        }
    }

    public void Unequip(Item unequippedItem, UiButtonClick slot)
    {
        if(slot == rightHand || slot == leftHand)
        {
            WeaponObject weapon = (WeaponObject)unequippedItem.item;

            switch ((int)weapon.type)
            {
                case (1):
                    if (slot == rightHand)
                    {
                        Debug.Log("Unequipped Right");
                        abilityManager.RemoveAbility(2);
                        Destroy(GetComponent<StingRight>());
                    }
                    if (slot == leftHand)
                    {
                        Debug.Log("Unequipped Left");
                        abilityManager.RemoveAbility(1);
                        Destroy(GetComponent<StingLeft>());
                    }
                    break;
                case (2):
                    Debug.Log("Unequipped 2 hander");
                    abilityManager.RemoveAbility(2);
                    abilityManager.RemoveAbility(1);
                    Destroy(GetComponent<Sting2Handed>());
                    Destroy(GetComponent<SpinAttack>());
                    break;
                case (3):
                    if (slot == rightHand)
                    {
                        Debug.Log("Unequipped Right");
                        abilityManager.RemoveAbility(2);
                        Destroy(GetComponent<ShieldToss>());
                    }
                    if (slot == leftHand)
                    {
                        Debug.Log("Unequipped Left");
                        abilityManager.RemoveAbility(1);
                        Destroy(GetComponent<Block>());
                    }
                    break;
                case (4):
                    Debug.Log("Unequipped bow");
                    abilityManager.RemoveAbility(2);
                    abilityManager.RemoveAbility(1);
                    Destroy(GetComponent<PowerShot>());
                    Destroy(GetComponent<TripleShot>());
                    break;
                case (5):
                    Debug.Log("Unequipped Staff");
                    abilityManager.RemoveAbility(2);
                    abilityManager.RemoveAbility(1);
                    Destroy(GetComponent<BigProjectile>());
                    Destroy(GetComponent<NonProjectile>());
                    break;
            }
        }

        Debug.Log("Unequipped " + unequippedItem.item.name);
        testEquipmentCount--;
    }

    public void Equip(Item equippedItem, UiButtonClick slot)
    {
        if(slot == rightHand || slot == leftHand)
        {
            WeaponObject weapon = (WeaponObject)equippedItem.item;

            switch ((int)weapon.weaponType)
            {
                case (0):
                    if (slot == rightHand)
                    {
                        Debug.Log("Equipped Right");
                        StingRight stingRight = gameObject.AddComponent<StingRight>();
                        abilityManager.SetAbility(2, stingRight);
                    }
                    if (slot == leftHand)
                    {
                        Debug.Log("Equipped Left");
                        StingLeft stingLeft = gameObject.AddComponent<StingLeft>();
                        abilityManager.SetAbility(1, stingLeft);
                    }
                    break;
                case (1):
                    Debug.Log("Equip 2 hander");
                    Sting2Handed sting2Handed = gameObject.AddComponent<Sting2Handed>();
                    abilityManager.SetAbility(2, sting2Handed);
                    SpinAttack spinAttack = gameObject.AddComponent<SpinAttack>();
                    abilityManager.SetAbility(1, spinAttack);
                    break;
                case (2):
                    if (slot == rightHand)
                    {
                        Debug.Log("Equipped Right");
                        ShieldToss shieldToss = gameObject.AddComponent<ShieldToss>();
                        abilityManager.SetAbility(2, shieldToss);
                    }
                    if (slot == leftHand)
                    {
                        Debug.Log("Equipped Left");
                        Block block = gameObject.AddComponent<Block>();
                        abilityManager.SetAbility(1, block);
                    }
                    break;
                case (3):
                    Debug.Log("Equip bow");
                    PowerShot powerShot = gameObject.AddComponent<PowerShot>();
                    abilityManager.SetAbility(2, powerShot);
                    TripleShot tripleShot = gameObject.AddComponent<TripleShot>();
                    abilityManager.SetAbility(1, tripleShot);
                    break;
                case (4):
                    Debug.Log("Equip staff");
                    BigProjectile bigProjectile = gameObject.AddComponent<BigProjectile>();
                    abilityManager.SetAbility(2, bigProjectile);
                    NonProjectile nonProjectile = gameObject.AddComponent<NonProjectile>();
                    abilityManager.SetAbility(1, nonProjectile);
                    break;
            }
        }

        Debug.Log("Equipped " + equippedItem.item.name);
        testEquipmentCount++;
    }

    public void UseConsumable(Item usedItem)
    {
        Debug.Log("Used " + usedItem.item.name);
        if ((int)usedItem.item.type == 2)
        {
            ConsumableObject consumable = (ConsumableObject)usedItem.item;
            foreach (ItemBuff buff in consumable.buffs)
            {
                events.NewBuff(buff.sourceID, buff.effectID, buff.effectiveness, buff.duration);
            }
            //if (item.scriptName != "") gameObject.AddComponent(Type.GetType(item.scriptName));
        }

    }

    public void DropItem(Item droppedItem)
    {
        Debug.Log("Dropped item " + droppedItem.item.name);
        GameObject groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
        groundItem.GetComponent<ItemOnGround>()._item = droppedItem;
        groundItem.GetComponent<SpriteRenderer>().sprite = droppedItem.item.iconSprite;
    }
}
