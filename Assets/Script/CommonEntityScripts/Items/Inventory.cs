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

    public GameObject armorHeadR1;
    public GameObject armorHeadR2;
    public GameObject armorHeadR3;
    public GameObject armorHeadR4;
    public GameObject armorHeadR5;
    public GameObject armorHeadR6;

    public GameObject armorChestR1;
    public GameObject armorChestR2;
    public GameObject armorChestR3;
    public GameObject armorChestR4;
    public GameObject armorChestR5;
    public GameObject armorChestR6;

    public GameObject armorLegsR1;
    public GameObject armorLegsR2;
    public GameObject armorLegsR3;
    public GameObject armorLegsR4;
    public GameObject armorLegsR5;
    public GameObject armorLegsR6;

    public GameObject weaponRightHandR1;
    public GameObject weaponRightHandR2;
    public GameObject weaponRightHandR3;
    public GameObject weaponRightHandR4;
    public GameObject weaponRightHandR5;
    public GameObject weaponRightHandR6;

    public GameObject weaponLeftHandR1;
    public GameObject weaponLeftHandR2;
    public GameObject weaponLeftHandR3;
    public GameObject weaponLeftHandR4;
    public GameObject weaponLeftHandR5;
    public GameObject weaponLeftHandR6;

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

                        else if (inventorySlot == leftHand)
                        {
                            Item temp = leftHand._item;
                            Unequip(temp, leftHand);
                            Equip(usedItem, inventorySlot);
                            leftHand._item = usedItem;
                            leftHand.icon.sprite = usedItem.item.iconSprite;
                            usedItem = temp;
                            if (rightHand._item != null)
                            {
                                temp = rightHand._item;
                                rightHand._item = usedItem;
                                Unequip(temp, inventorySlot);
                                Equip(usedItem, rightHand);
                                rightHand.icon.sprite = usedItem.item.iconSprite;
                                NewItem(temp);
                            }
                            else
                            {
                                rightHand._item = usedItem;
                                Equip(usedItem, rightHand);
                                rightHand.icon.sprite = usedItem.item.iconSprite;

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
        EquipmentObject equipmentObject = (EquipmentObject)unequippedItem.item;
        if ((int)slot._type == 1)
        {
            if (slot == rightHand)
            {
                Debug.Log("rune slots " + equipmentObject.runeSlots);
                switch (equipmentObject.runeSlots)
                {
                    case 0:
                        break;
                    case 1:
                        weaponRightHandR1.SetActive(false);
                        break;
                    case 2:
                        weaponRightHandR1.SetActive(false);
                        weaponRightHandR2.SetActive(false);
                        break;
                    case 3:
                        weaponRightHandR1.SetActive(false);
                        weaponRightHandR2.SetActive(false);
                        weaponRightHandR3.SetActive(false);
                        break;
                    case 4:
                        weaponRightHandR1.SetActive(false);
                        weaponRightHandR2.SetActive(false);
                        weaponRightHandR3.SetActive(false);
                        weaponRightHandR4.SetActive(false);
                        break;
                    case 5:
                        weaponRightHandR1.SetActive(false);
                        weaponRightHandR2.SetActive(false);
                        weaponRightHandR3.SetActive(false);
                        weaponRightHandR4.SetActive(false);
                        weaponRightHandR5.SetActive(false);
                        break;
                    case 6:
                        weaponRightHandR1.SetActive(false);
                        weaponRightHandR2.SetActive(false);
                        weaponRightHandR3.SetActive(false);
                        weaponRightHandR4.SetActive(false);
                        weaponRightHandR5.SetActive(false);
                        weaponRightHandR6.SetActive(false);
                        break;
                }
            }
            else if (slot == leftHand)
            {
                switch (equipmentObject.runeSlots)
                {
                    case 0:
                        break;
                    case 1:
                        weaponLeftHandR1.SetActive(false);
                        break;
                    case 2:
                        weaponLeftHandR1.SetActive(false);
                        weaponLeftHandR2.SetActive(false);
                        break;
                    case 3:
                        weaponLeftHandR1.SetActive(false);
                        weaponLeftHandR2.SetActive(false);
                        weaponLeftHandR3.SetActive(false);
                        break;
                    case 4:
                        weaponLeftHandR1.SetActive(false);
                        weaponLeftHandR2.SetActive(false);
                        weaponLeftHandR3.SetActive(false);
                        weaponLeftHandR4.SetActive(false);
                        break;
                    case 5:
                        weaponLeftHandR1.SetActive(false);
                        weaponLeftHandR2.SetActive(false);
                        weaponLeftHandR3.SetActive(false);
                        weaponLeftHandR4.SetActive(false);
                        weaponLeftHandR5.SetActive(false);
                        break;
                    case 6:
                        weaponLeftHandR1.SetActive(false);
                        weaponLeftHandR2.SetActive(false);
                        weaponLeftHandR3.SetActive(false);
                        weaponLeftHandR4.SetActive(false);
                        weaponLeftHandR5.SetActive(false);
                        weaponLeftHandR6.SetActive(false);
                        break;
                }
            }
        }
        else if ((int)slot._type == 2)
        {

        }
        else if ((int)slot._type == 3)
        {

        }
        else if ((int)slot._type == 4)
        {

        }


        if (slot == rightHand || slot == leftHand)
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
        EquipmentObject equipmentObject = (EquipmentObject)equippedItem.item;
        Debug.Log("rune slots " + equipmentObject.runeSlots + "type " + (int)slot._type + " slot = " + slot);
        if ((int)slot._type == 1)
        {
            if(slot == rightHand)
            {
                Debug.Log("rune slots " + equipmentObject.runeSlots);
                switch(equipmentObject.runeSlots)
                {
                    case 0:
                        break;
                    case 1:
                        weaponRightHandR1.SetActive(true);
                        break;
                    case 2:
                        weaponRightHandR1.SetActive(true);
                        weaponRightHandR2.SetActive(true);
                        break;
                    case 3:
                        weaponRightHandR1.SetActive(true);
                        weaponRightHandR2.SetActive(true);
                        weaponRightHandR3.SetActive(true);
                        break;
                    case 4:
                        weaponRightHandR1.SetActive(true);
                        weaponRightHandR2.SetActive(true);
                        weaponRightHandR3.SetActive(true);
                        weaponRightHandR4.SetActive(true);
                        break;
                    case 5:
                        weaponRightHandR1.SetActive(true);
                        weaponRightHandR2.SetActive(true);
                        weaponRightHandR3.SetActive(true);
                        weaponRightHandR4.SetActive(true);
                        weaponRightHandR5.SetActive(true);
                        break;
                    case 6:
                        weaponRightHandR1.SetActive(true);
                        weaponRightHandR2.SetActive(true);
                        weaponRightHandR3.SetActive(true);
                        weaponRightHandR4.SetActive(true);
                        weaponRightHandR5.SetActive(true);
                        weaponRightHandR6.SetActive(true);
                        break;
                }
            }
            else if(slot == leftHand)
            {
                switch (equipmentObject.runeSlots)
                {
                    case 0:
                        break;
                    case 1:
                        weaponLeftHandR1.SetActive(true);
                        break;
                    case 2:
                        weaponLeftHandR1.SetActive(true);
                        weaponLeftHandR2.SetActive(true);
                        break;
                    case 3:
                        weaponLeftHandR1.SetActive(true);
                        weaponLeftHandR2.SetActive(true);
                        weaponLeftHandR3.SetActive(true);
                        break;
                    case 4:
                        weaponLeftHandR1.SetActive(true);
                        weaponLeftHandR2.SetActive(true);
                        weaponLeftHandR3.SetActive(true);
                        weaponLeftHandR4.SetActive(true);
                        break;
                    case 5:
                        weaponLeftHandR1.SetActive(true);
                        weaponLeftHandR2.SetActive(true);
                        weaponLeftHandR3.SetActive(true);
                        weaponLeftHandR4.SetActive(true);
                        weaponLeftHandR5.SetActive(true);
                        break;
                    case 6:
                        weaponLeftHandR1.SetActive(true);
                        weaponLeftHandR2.SetActive(true);
                        weaponLeftHandR3.SetActive(true);
                        weaponLeftHandR4.SetActive(true);
                        weaponLeftHandR5.SetActive(true);
                        weaponLeftHandR6.SetActive(true);
                        break;
                }
            }
        }
        else if((int)slot._type == 2)
        {

        }
        else if ((int)slot._type == 3)
        {

        }
        else if ((int)slot._type == 4)
        {

        }



        if (slot == rightHand || slot == leftHand)
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
