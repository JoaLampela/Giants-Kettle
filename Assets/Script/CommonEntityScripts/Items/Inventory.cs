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
        ArmorHead,
        ArmorChest,
        ArmorLegs,
        Consumable,
        Rune
    }

    private EntityEvents events;

    public UiButtonClick[] inventorySlots;
    public UiButtonClick[] equipmentSlots;
    public UiButtonClick rightHand;
    public UiButtonClick leftHand;
    public UiButtonClick armorHead;
    public UiButtonClick armorChest;
    public UiButtonClick armorLegs;

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
    private void Start()
    {
        inventorySlots = new UiButtonClick[24];
        equipmentSlots = new UiButtonClick[36];
        Debug.Log(inventorySlots.Length + "lenght");
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
        if ((int)usedItem.item.type == (int)ItemType.Consumable)
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
                                leftHand.RemoveItemFromslot();
                            }
                            Unequip(rightHand._item, rightHand);
                            NewItem(rightHand._item);
                            rightHand.RemoveItemFromslot();
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
                            leftHand.RemoveItemFromslot();
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
                            Item temp = leftHand._item;
                            leftHand.RemoveItemFromslot();
                            Unequip(temp, inventorySlot);

                            Equip(usedItem, inventorySlot);
                            Debug.Log(usedItem + " to slot " + inventorySlot);
                            rightHand._item = usedItem;
                            rightHand.icon.sprite = usedItem.item.iconSprite;

                            
                            NewItem(temp);
                        }


                        else if (inventorySlot == rightHand)
                        {
                            Item temp = rightHand._item;
                            Unequip(temp, rightHand);
                            Equip(usedItem, rightHand);
                            rightHand.SetNewItemToslot(usedItem);
                            usedItem = temp;
                            if (leftHand._item != null)
                            {
                                temp = leftHand._item;
                                leftHand.SetNewItemToslot(usedItem);
                                Unequip(temp, leftHand);
                                Equip(usedItem, leftHand);
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
                            Equip(usedItem, leftHand);
                            leftHand.SetNewItemToslot(usedItem);
                            usedItem = temp;
                            if (rightHand._item != null)
                            {
                                temp = rightHand._item;
                                rightHand.SetNewItemToslot(usedItem);
                                Unequip(temp, rightHand);
                                Equip(usedItem, rightHand);
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
            if (slot == rightHand || unequippedItem.item.isTwoHander)
            {
                weaponRightHandR1.GetComponent<UiButtonClick>().RemoveItemFromslot();
                weaponRightHandR2.GetComponent<UiButtonClick>().RemoveItemFromslot();
                weaponRightHandR3.GetComponent<UiButtonClick>().RemoveItemFromslot();
                weaponRightHandR4.GetComponent<UiButtonClick>().RemoveItemFromslot();
                weaponRightHandR5.GetComponent<UiButtonClick>().RemoveItemFromslot();
                weaponRightHandR6.GetComponent<UiButtonClick>().RemoveItemFromslot();
                weaponRightHandR1.SetActive(false);
                weaponRightHandR2.SetActive(false);
                weaponRightHandR3.SetActive(false);
                weaponRightHandR4.SetActive(false);
                weaponRightHandR5.SetActive(false);
                weaponRightHandR6.SetActive(false);
            }
            if (slot == leftHand || unequippedItem.item.isTwoHander)
            {
                weaponLeftHandR1.GetComponent<UiButtonClick>().RemoveItemFromslot();
                weaponLeftHandR2.GetComponent<UiButtonClick>().RemoveItemFromslot();
                weaponLeftHandR3.GetComponent<UiButtonClick>().RemoveItemFromslot();
                weaponLeftHandR4.GetComponent<UiButtonClick>().RemoveItemFromslot();
                weaponLeftHandR5.GetComponent<UiButtonClick>().RemoveItemFromslot();
                weaponLeftHandR6.GetComponent<UiButtonClick>().RemoveItemFromslot();
                weaponLeftHandR1.SetActive(false);
                weaponLeftHandR2.SetActive(false);
                weaponLeftHandR3.SetActive(false);
                weaponLeftHandR4.SetActive(false);
                weaponLeftHandR5.SetActive(false);
                weaponLeftHandR6.SetActive(false);
            }
        }
        else if ((int)slot._type == 2)
        {
            armorHeadR1.SetActive(false);
            armorHeadR2.SetActive(false);
            armorHeadR3.SetActive(false);
            armorHeadR4.SetActive(false);
            armorHeadR5.SetActive(false);
            armorHeadR6.SetActive(false);
        }
        else if ((int)slot._type == 3)
        {
            armorChestR1.SetActive(false);
            armorChestR2.SetActive(false);
            armorChestR3.SetActive(false);
            armorChestR4.SetActive(false);
            armorChestR5.SetActive(false);
            armorChestR6.SetActive(false);
        }
        else if ((int)slot._type == 4)
        {
            armorLegsR1.SetActive(false);
            armorLegsR2.SetActive(false);
            armorLegsR3.SetActive(false);
            armorLegsR4.SetActive(false);
            armorLegsR5.SetActive(false);
            armorLegsR6.SetActive(false);
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
        Debug.Log("equippin " + equippedItem + " to " + slot + " isTwohander = " +equippedItem.item.isTwoHander);
        if ((int)slot._type == 1)
        {
            if(slot == rightHand || equippedItem.item.isTwoHander)
            {
                Debug.Log("In right");

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

                if (equippedItem._runeList.Length >= 1)
                    if (equippedItem._runeList[0] != null) weaponRightHandR1.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[0]));
                if (equippedItem._runeList.Length >= 2)
                    if (equippedItem._runeList[1] != null) weaponRightHandR2.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[1]));
                if (equippedItem._runeList.Length >= 3)
                    if (equippedItem._runeList[2] != null) weaponRightHandR3.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[2]));
                if (equippedItem._runeList.Length >= 4)
                    if (equippedItem._runeList[3] != null) weaponRightHandR4.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[3]));
                if (equippedItem._runeList.Length >= 5)
                    if (equippedItem._runeList[4] != null) weaponRightHandR5.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[4]));
                if (equippedItem._runeList.Length == 6)
                    if (equippedItem._runeList[5] != null) weaponRightHandR6.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[5]));
            }
            if(slot == leftHand || equippedItem.item.isTwoHander)
            {
                Debug.Log("In left");
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
                if(equippedItem._runeList.Length >= 1)
                    if(equippedItem._runeList[0] != null) weaponLeftHandR1.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[0]));
                if (equippedItem._runeList.Length >= 2)
                    if (equippedItem._runeList[1] != null) weaponLeftHandR2.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[1]));
                if (equippedItem._runeList.Length >= 3)
                    if (equippedItem._runeList[2] != null) weaponLeftHandR3.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[2]));
                if (equippedItem._runeList.Length >= 4)
                    if (equippedItem._runeList[3] != null) weaponLeftHandR4.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[3]));
                if (equippedItem._runeList.Length >= 5)
                    if (equippedItem._runeList[4] != null) weaponLeftHandR5.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[4]));
                if (equippedItem._runeList.Length == 6)
                    if (equippedItem._runeList[5] != null) weaponLeftHandR6.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[5]));
            }
        }
        else if((int)slot._type == 2)
        {
            switch (equipmentObject.runeSlots)
            {
                case 0:
                    break;
                case 1:
                    armorHeadR1.SetActive(true);
                    break;
                case 2:
                    armorHeadR1.SetActive(true);
                    armorHeadR2.SetActive(true);
                    break;
                case 3:
                    armorHeadR1.SetActive(true);
                    armorHeadR2.SetActive(true);
                    armorHeadR3.SetActive(true);
                    break;
                case 4:
                    armorHeadR1.SetActive(true);
                    armorHeadR2.SetActive(true);
                    armorHeadR3.SetActive(true);
                    armorHeadR4.SetActive(true);
                    break;
                case 5:
                    armorHeadR1.SetActive(true);
                    armorHeadR2.SetActive(true);
                    armorHeadR3.SetActive(true);
                    armorHeadR4.SetActive(true);
                    armorHeadR5.SetActive(true);
                    break;
                case 6:
                    armorHeadR1.SetActive(true);
                    armorHeadR2.SetActive(true);
                    armorHeadR3.SetActive(true);
                    armorHeadR4.SetActive(true);
                    armorHeadR5.SetActive(true);
                    armorHeadR6.SetActive(true);
                    break;
            }
        }
        else if ((int)slot._type == 3)
        {
            switch (equipmentObject.runeSlots)
            {
                case 0:
                    break;
                case 1:
                    armorChestR1.SetActive(true);
                    break;
                case 2:
                    armorChestR1.SetActive(true);
                    armorChestR2.SetActive(true);
                    break;
                case 3:
                    armorChestR1.SetActive(true);
                    armorChestR2.SetActive(true);
                    armorChestR3.SetActive(true);
                    break;
                case 4:
                    armorChestR1.SetActive(true);
                    armorChestR2.SetActive(true);
                    armorChestR3.SetActive(true);
                    armorChestR4.SetActive(true);
                    break;
                case 5:
                    armorChestR1.SetActive(true);
                    armorChestR2.SetActive(true);
                    armorChestR3.SetActive(true);
                    armorChestR4.SetActive(true);
                    armorChestR5.SetActive(true);
                    break;
                case 6:
                    armorChestR1.SetActive(true);
                    armorChestR2.SetActive(true);
                    armorChestR3.SetActive(true);
                    armorChestR4.SetActive(true);
                    armorChestR5.SetActive(true);
                    armorChestR6.SetActive(true);
                    break;
            }
        }
        else if ((int)slot._type == 4)
        {
            switch (equipmentObject.runeSlots)
            {
                case 0:
                    break;
                case 1:
                    armorLegsR1.SetActive(true);
                    break;
                case 2:
                    armorLegsR1.SetActive(true);
                    armorLegsR2.SetActive(true);
                    break;
                case 3:
                    armorLegsR1.SetActive(true);
                    armorLegsR2.SetActive(true);
                    armorLegsR3.SetActive(true);
                    break;
                case 4:
                    armorLegsR1.SetActive(true);
                    armorLegsR2.SetActive(true);
                    armorLegsR3.SetActive(true);
                    armorLegsR4.SetActive(true);
                    break;
                case 5:
                    armorLegsR1.SetActive(true);
                    armorLegsR2.SetActive(true);
                    armorLegsR3.SetActive(true);
                    armorLegsR4.SetActive(true);
                    armorLegsR5.SetActive(true);
                    break;
                case 6:
                    armorLegsR1.SetActive(true);
                    armorLegsR2.SetActive(true);
                    armorLegsR3.SetActive(true);
                    armorLegsR4.SetActive(true);
                    armorLegsR5.SetActive(true);
                    armorLegsR6.SetActive(true);
                    break;
            }
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
        if ((int)usedItem.item.type == (int)ItemType.Consumable)
        {
            ConsumableObject consumable = (ConsumableObject)usedItem.item;
            foreach (ItemBuff buff in consumable.buffs)
            {
                events.NewBuff(buff.sourceID, buff.effectID, buff.effectiveness, buff.duration);
            }
            //if (item.scriptName != "") gameObject.AddComponent(Type.GetType(item.scriptName));
        }

    }


    public void AddNewRuneToItem(Item newItem, GameObject slot)
    {
        Debug.Log("Addidng new rune to " + slot);
        RuneObject rune = (RuneObject)newItem.item;
        if (slot == weaponRightHandR1)
        {
            rightHand._item._runeList[0] = rune;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR1.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponRightHandR2)
        {
            rightHand._item._runeList[1] = rune;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR2.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponRightHandR3)
        {
            rightHand._item._runeList[2] = rune;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR3.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponRightHandR4)
        {
            rightHand._item._runeList[3] = rune;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR4.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponRightHandR5)
        {
            rightHand._item._runeList[4] = rune;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR5.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponRightHandR6)
        {
            rightHand._item._runeList[5] = rune;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR6.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }

        else if (slot == weaponLeftHandR1)
        {
            leftHand._item._runeList[0] = rune;
            if (leftHand._item.item.isTwoHander) weaponRightHandR1.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponLeftHandR2)
        {
            leftHand._item._runeList[1] = rune;
            if (leftHand._item.item.isTwoHander) weaponRightHandR2.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponLeftHandR3)
        {
            leftHand._item._runeList[2] = rune;
            if (leftHand._item.item.isTwoHander) weaponRightHandR3.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponLeftHandR4)
        {
            leftHand._item._runeList[3] = rune;
            if (leftHand._item.item.isTwoHander) weaponRightHandR4.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponLeftHandR5)
        {
            leftHand._item._runeList[4] = rune;
            if (leftHand._item.item.isTwoHander) weaponRightHandR5.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponLeftHandR6)
        {
            leftHand._item._runeList[5] = rune;
            if (leftHand._item.item.isTwoHander) weaponRightHandR6.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }

        else if (slot == armorHeadR1) armorHead._item._runeList[0] = rune;
        else if (slot == armorHeadR2) armorHead._item._runeList[1] = rune;
        else if (slot == armorHeadR3) armorHead._item._runeList[2] = rune;
        else if (slot == armorHeadR4) armorHead._item._runeList[3] = rune;
        else if (slot == armorHeadR5) armorHead._item._runeList[4] = rune;
        else if (slot == armorHeadR6) armorHead._item._runeList[5] = rune;

        else if (slot == armorChestR1) armorChest._item._runeList[0] = rune;
        else if (slot == armorChestR2) armorChest._item._runeList[1] = rune;
        else if (slot == armorChestR3) armorChest._item._runeList[2] = rune;
        else if (slot == armorChestR4) armorChest._item._runeList[3] = rune;
        else if (slot == armorChestR5) armorChest._item._runeList[4] = rune;
        else if (slot == armorChestR6) armorChest._item._runeList[5] = rune;

        else if (slot == armorLegsR1) armorLegs._item._runeList[0] = rune;
        else if (slot == armorLegsR2) armorLegs._item._runeList[1] = rune;
        else if (slot == armorLegsR3) armorLegs._item._runeList[2] = rune;
        else if (slot == armorLegsR4) armorLegs._item._runeList[3] = rune;
        else if (slot == armorLegsR5) armorLegs._item._runeList[4] = rune;
        else if (slot == armorLegsR6) armorLegs._item._runeList[5] = rune;
    }
    public void RemoveRuneFromItem(GameObject slot)
    {
        Debug.Log("Removing rune from " + slot);
        if (slot == weaponRightHandR1)
        {
            rightHand._item._runeList[0] = null;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR1.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponRightHandR2)
        {
            rightHand._item._runeList[1] = null;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR2.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponRightHandR3)
        {
            rightHand._item._runeList[2] = null;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR3.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponRightHandR4)
        {
            rightHand._item._runeList[3] = null;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR4.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponRightHandR5)
        {
            rightHand._item._runeList[4] = null;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR5.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponRightHandR6)
        {
            rightHand._item._runeList[5] = null;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR6.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }

        else if (slot == weaponLeftHandR1)
        {
            leftHand._item._runeList[0] = null;
            if (leftHand._item.item.isTwoHander) weaponRightHandR1.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponLeftHandR2)
        {
            leftHand._item._runeList[1] = null;
            if (leftHand._item.item.isTwoHander) weaponRightHandR2.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponLeftHandR3)
        {
            leftHand._item._runeList[2] = null;
            if (leftHand._item.item.isTwoHander) weaponRightHandR3.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponLeftHandR4)
        {
            leftHand._item._runeList[3] = null;
            if (leftHand._item.item.isTwoHander) weaponRightHandR4.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponLeftHandR5)
        {
            leftHand._item._runeList[4] = null;
            if (leftHand._item.item.isTwoHander) weaponRightHandR5.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponLeftHandR6)
        {
            leftHand._item._runeList[5] = null;
            if (leftHand._item.item.isTwoHander) weaponRightHandR6.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }

        else if (slot == armorHeadR1) armorHead._item._runeList[0] = null;
        else if (slot == armorHeadR2) armorHead._item._runeList[1] = null;
        else if (slot == armorHeadR3) armorHead._item._runeList[2] = null;
        else if (slot == armorHeadR4) armorHead._item._runeList[3] = null;
        else if (slot == armorHeadR5) armorHead._item._runeList[4] = null;
        else if (slot == armorHeadR6) armorHead._item._runeList[5] = null;

        else if (slot == armorChestR1) armorChest._item._runeList[0] = null;
        else if (slot == armorChestR2) armorChest._item._runeList[1] = null;
        else if (slot == armorChestR3) armorChest._item._runeList[2] = null;
        else if (slot == armorChestR4) armorChest._item._runeList[3] = null;
        else if (slot == armorChestR5) armorChest._item._runeList[4] = null;
        else if (slot == armorChestR6) armorChest._item._runeList[5] = null;

        else if (slot == armorLegsR1) armorLegs._item._runeList[0] = null;
        else if (slot == armorLegsR2) armorLegs._item._runeList[1] = null;
        else if (slot == armorLegsR3) armorLegs._item._runeList[2] = null;
        else if (slot == armorLegsR4) armorLegs._item._runeList[3] = null;
        else if (slot == armorLegsR5) armorLegs._item._runeList[4] = null;
        else if (slot == armorLegsR6) armorLegs._item._runeList[5] = null;
    }
    public void DropItem(Item droppedItem)
    {
        Debug.Log("Dropped item " + droppedItem.item.name);
        GameObject groundItem = Instantiate(itemOnGround, transform.position, Quaternion.identity);
        groundItem.GetComponent<ItemOnGround>()._item = droppedItem;
        groundItem.GetComponent<SpriteRenderer>().sprite = droppedItem.item.iconSprite;
    }
}
