using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
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

    public enum WeaponType
    {
        OneHandedSword,
        TwoHandedSword,
        Shield,
        Bow,
        Staff
    }
    private Player_Animations player_Animations;
    private EntityEvents events;

    public UiButtonClick[] inventorySlots;
    public UiButtonClick[] equipmentSlots;
    public UiButtonClick rightHand;
    public UiButtonClick leftHand;
    public UiButtonClick armorHead;
    public UiButtonClick armorChest;
    public UiButtonClick armorLegs;
    public Item dashItem;
    public WeaponObject dashItemObject;

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

    [SerializeField] private DashAbility dashAbility;
    [SerializeField] private RightAbility rightAbility;
    [SerializeField] private LeftAbility leftAbility; 
    private void Awake()
    {
        player_Animations = GetComponent<Player_Animations>();
        events = GetComponent<EntityEvents>();
        abilityManager = GetComponent<EntityAbilityManager>();
    }
    private void Start()
    {
        dashItem = new Item(dashItemObject);
        inventorySlots = new UiButtonClick[24];
        equipmentSlots = new UiButtonClick[36];
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
                inventorySlot.SetRuneToolTipOn();
                slotFound = true;
                break;
            }
        }
        if (!slotFound) DropItem(item);
    }

    [Obsolete]
    public void UseItem(Item usedItem)
    {
        if ((int)usedItem.item.type == (int)ItemType.Consumable)
        {
            UseConsumable(usedItem);
        }
        else
        {
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
                        
                        rightHand._item = usedItem;
                        leftHand._item = usedItem;
                        rightHand.icon.sprite = usedItem.item.iconSprite;
                        leftHand.icon.sprite = usedItem.item.iconSprite;
                        Equip(usedItem, inventorySlot);
                    }
                    else if (inventorySlot._item != null)
                    {
                        if (inventorySlot._item.item.isTwoHander)
                        {
                            Item temp = leftHand._item;
                            leftHand.RemoveItemFromslot();
                            Unequip(temp, inventorySlot);


                            rightHand._item = usedItem;
                            rightHand.icon.sprite = usedItem.item.iconSprite;
                            Equip(usedItem, inventorySlot);
                            Debug.Log(usedItem + " to slot " + inventorySlot);
                            

                            
                            NewItem(temp);
                        }


                        else if (inventorySlot == rightHand)
                        {
                            Item temp = rightHand._item;
                            Unequip(temp, rightHand);
                            rightHand.SetNewItemToslot(usedItem);
                            Equip(usedItem, rightHand);
                            
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
                        
                        inventorySlot._item = usedItem;
                        inventorySlot.icon.sprite = usedItem.item.iconSprite;
                        Equip(usedItem, inventorySlot);

                    }
                    break;
                }
            }
        }
    }

    [Obsolete]
    public void Unequip(Item unequippedItem, UiButtonClick slot)
    {
        Debug.Log("Unequipped item " + unequippedItem);
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

                weaponRightHandR1.GetComponent<UiButtonClick>().SetLockOff();
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

                weaponLeftHandR1.GetComponent<UiButtonClick>().SetLockOff();
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

            armorHeadR1.GetComponent<UiButtonClick>().SetLockOff();
        }
        else if ((int)slot._type == 3)
        {
            armorChestR1.SetActive(false);
            armorChestR2.SetActive(false);
            armorChestR3.SetActive(false);
            armorChestR4.SetActive(false);
            armorChestR5.SetActive(false);
            armorChestR6.SetActive(false);

            armorChestR1.GetComponent<UiButtonClick>().SetLockOff();
        }
        else if ((int)slot._type == 4)
        {
            armorLegsR1.SetActive(false);
            armorLegsR2.SetActive(false);
            armorLegsR3.SetActive(false);
            armorLegsR4.SetActive(false);
            armorLegsR5.SetActive(false);
            armorLegsR6.SetActive(false);

            armorLegsR1.GetComponent<UiButtonClick>().SetLockOff();
        }


        if (slot == rightHand || slot == leftHand)
        {
            WeaponObject weapon = (WeaponObject)unequippedItem.item;

            switch ((int)weapon.weaponType)
            {
                case ((int)WeaponType.OneHandedSword):
                    if (slot == rightHand)
                    {
                        player_Animations.SwitchToEmptyRightHand();
                        abilityManager.RemoveAbility(2);
                        Destroy(GetComponent<StingRight>());
                        Destroy(GetComponent<OneHandedBasicAttack>());
                        rightAbility.RemoveAbility();
                    }
                    if (slot == leftHand)
                    {
                        player_Animations.SwitchToEmptyLeftHand();
                        abilityManager.RemoveAbility(1);
                        Destroy(GetComponent<StingLeft>());
                        leftAbility.RemoveAbility();
                    }
                    break;
                case ((int)WeaponType.TwoHandedSword):
                    player_Animations.SwitchToEmptyLeftHand();
                    player_Animations.SwitchToEmptyRightHand();
                    abilityManager.RemoveAbility(2);
                    abilityManager.RemoveAbility(1);
                    Destroy(GetComponent<Sting2Handed>());
                    Destroy(GetComponent<SpinAttack>());
                    Destroy(GetComponent<TwoHandedBasicAttack>());
                    rightAbility.RemoveAbility();
                    leftAbility.RemoveAbility();
                    break;
                case ((int)WeaponType.Shield):
                    if (slot == rightHand)
                    {
                        player_Animations.SwitchToEmptyRightHand();
                        abilityManager.RemoveAbility(2);
                        Destroy(GetComponent<ShieldToss>());
                        Destroy(GetComponent<ShieldSlam>());
                        rightAbility.RemoveAbility();
                        
                    }
                    if (slot == leftHand)
                    {
                        player_Animations.SwitchToEmptyLeftHand();
                        abilityManager.RemoveAbility(1);
                        Destroy(GetComponent<Block>());
                        leftAbility.RemoveAbility();
                    }
                    break;
                case ((int)WeaponType.Bow):
                    player_Animations.SwitchToEmptyLeftHand();
                    player_Animations.SwitchToEmptyRightHand();
                    abilityManager.RemoveAbility(2);
                    abilityManager.RemoveAbility(1);
                    Destroy(GetComponent<PowerShot>());
                    Destroy(GetComponent<TripleShot>());
                    rightAbility.RemoveAbility();
                    leftAbility.RemoveAbility();
                    break;
                case ((int)WeaponType.Staff):
                    player_Animations.SwitchToEmptyLeftHand();
                    player_Animations.SwitchToEmptyRightHand();
                    Debug.Log("Unequipped Staff");
                    abilityManager.RemoveAbility(2);
                    abilityManager.RemoveAbility(1);
                    Destroy(GetComponent<BigProjectile>());
                    Destroy(GetComponent<NonProjectile>());
                    Destroy(GetComponent<StaffBasicAttack>());
                    rightAbility.RemoveAbility();
                    leftAbility.RemoveAbility();
                    break;
            }
        }

        testEquipmentCount--;

        List<ItemObject> tempList = new List<ItemObject>();
        for (int i = 0; i < unequippedItem._runeList.Length; i++)
        {
            if (unequippedItem._runeList[i] != null)
            {
                tempList.Add(unequippedItem._runeList[i]);
            }
        }
        if (unequippedItem.item.isTwoHander) RemoveAffectingRune(unequippedItem, tempList, IRuneScript.Hand.dual);
        else if (slot == rightHand) RemoveAffectingRune(unequippedItem, tempList, IRuneScript.Hand.right);
        else if (slot == leftHand) RemoveAffectingRune(unequippedItem, tempList, IRuneScript.Hand.left);
    }

    public enum Hand
    {
        Right,
        Left
    }

    [Obsolete]
    public void Equip(Item equippedItem, UiButtonClick slot)
    {
        EquipmentObject equipmentObject = (EquipmentObject)equippedItem.item;
        if ((int)slot._type == 1)
        {
            if (slot == rightHand || equippedItem.item.isTwoHander)
            {
                switch (equipmentObject.runeSlots)
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

                EquipmentObject equipment = (EquipmentObject)equippedItem.item;
                if (equipment.baseRune != null)
                {
                    weaponRightHandR1.GetComponent<UiButtonClick>().SetLockOn();
                }

            }
            if (slot == leftHand || equippedItem.item.isTwoHander)
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
                if (equippedItem._runeList.Length >= 1)
                    if (equippedItem._runeList[0] != null) weaponLeftHandR1.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[0]));
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

                EquipmentObject equipment = (EquipmentObject)equippedItem.item;
                if (equipment.baseRune != null)
                {
                    weaponLeftHandR1.GetComponent<UiButtonClick>().SetLockOn();
                }
            }
        }
        else if ((int)slot._type == 2)
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
            if (equippedItem._runeList.Length >= 1)
                if (equippedItem._runeList[0] != null) armorHeadR1.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[0]));
            if (equippedItem._runeList.Length >= 2)
                if (equippedItem._runeList[1] != null) armorHeadR2.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[1]));
            if (equippedItem._runeList.Length >= 3)
                if (equippedItem._runeList[2] != null) armorHeadR3.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[2]));
            if (equippedItem._runeList.Length >= 4)
                if (equippedItem._runeList[3] != null) armorHeadR4.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[3]));
            if (equippedItem._runeList.Length >= 5)
                if (equippedItem._runeList[4] != null) armorHeadR5.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[4]));
            if (equippedItem._runeList.Length == 6)
                if (equippedItem._runeList[5] != null) armorHeadR6.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[5]));

            EquipmentObject equipment = (EquipmentObject)equippedItem.item;
            if (equipment.baseRune != null)
            {
                armorHeadR1.GetComponent<UiButtonClick>().SetLockOn();
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
            if (equippedItem._runeList.Length >= 1)
                if (equippedItem._runeList[0] != null) armorChestR1.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[0]));
            if (equippedItem._runeList.Length >= 2)
                if (equippedItem._runeList[1] != null) armorChestR2.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[1]));
            if (equippedItem._runeList.Length >= 3)
                if (equippedItem._runeList[2] != null) armorChestR3.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[2]));
            if (equippedItem._runeList.Length >= 4)
                if (equippedItem._runeList[3] != null) armorChestR4.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[3]));
            if (equippedItem._runeList.Length >= 5)
                if (equippedItem._runeList[4] != null) armorChestR5.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[4]));
            if (equippedItem._runeList.Length == 6)
                if (equippedItem._runeList[5] != null) armorChestR6.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[5]));

            EquipmentObject equipment = (EquipmentObject)equippedItem.item;
            if (equipment.baseRune != null)
            {
                armorChestR1.GetComponent<UiButtonClick>().SetLockOn();
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
            if (equippedItem._runeList.Length >= 1)
                if (equippedItem._runeList[0] != null) armorLegsR1.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[0]));
            if (equippedItem._runeList.Length >= 2)
                if (equippedItem._runeList[1] != null) armorLegsR2.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[1]));
            if (equippedItem._runeList.Length >= 3)
                if (equippedItem._runeList[2] != null) armorLegsR3.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[2]));
            if (equippedItem._runeList.Length >= 4)
                if (equippedItem._runeList[3] != null) armorLegsR4.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[3]));
            if (equippedItem._runeList.Length >= 5)
                if (equippedItem._runeList[4] != null) armorLegsR5.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[4]));
            if (equippedItem._runeList.Length == 6)
                if (equippedItem._runeList[5] != null) armorLegsR6.GetComponent<UiButtonClick>().SetNewItemToslot(new Item(equippedItem._runeList[5]));

            EquipmentObject equipment = (EquipmentObject)equippedItem.item;
            if (equipment.baseRune != null)
            {
                armorLegsR1.GetComponent<UiButtonClick>().SetLockOn();
            }
        }
        if (slot == rightHand || slot == leftHand)
        {
            WeaponObject weapon = (WeaponObject)equippedItem.item;


            switch ((int)weapon.weaponType)
            {
                case ((int)WeaponType.OneHandedSword):
                    if (slot == rightHand)
                    {
                        player_Animations.SwitchToSingleHandedSword(weapon.inGameObject);
                        StingRight stingRight = gameObject.AddComponent<StingRight>();
                        OneHandedBasicAttack oneHandedBasicAttack = gameObject.AddComponent<OneHandedBasicAttack>();
                        rightAbility.SetAbility(equippedItem, Hand.Right);
                        abilityManager.SetAbility(2, stingRight);
                        abilityManager.SetAbility(4, oneHandedBasicAttack);
                    }
                    if (slot == leftHand)
                    {
                        player_Animations.SwitchToOffHandSingleHandedSword(weapon.inGameObject);
                        StingLeft stingLeft = gameObject.AddComponent<StingLeft>();
                        leftAbility.SetAbility(equippedItem, Hand.Left);
                        abilityManager.SetAbility(1, stingLeft);
                    }
                    break;
                case ((int)WeaponType.TwoHandedSword):
                    player_Animations.SwitchToTwoHandedSword(weapon.inGameObject);
                    Sting2Handed sting2Handed = gameObject.AddComponent<Sting2Handed>();
                    TwoHandedBasicAttack twoHandedBasicAttack = gameObject.AddComponent<TwoHandedBasicAttack>();
                    abilityManager.SetAbility(2, sting2Handed);
                    SpinAttack spinAttack = gameObject.AddComponent<SpinAttack>();
                    rightAbility.SetAbility(equippedItem, Hand.Right);
                    leftAbility.SetAbility(equippedItem, Hand.Left);
                    abilityManager.SetAbility(1, spinAttack);
                    abilityManager.SetAbility(4, twoHandedBasicAttack);
                    break;
                case ((int)WeaponType.Shield):
                    if (slot == rightHand)
                    {
                        player_Animations.SwitchToShield(weapon.inGameObject);
                        ShieldToss shieldToss = gameObject.AddComponent<ShieldToss>();
                        ShieldSlam shieldSlam = gameObject.AddComponent<ShieldSlam>();
                        rightAbility.SetAbility(equippedItem, Hand.Right);
                        abilityManager.SetAbility(2, shieldToss);
                        abilityManager.SetAbility(4, shieldSlam);
                    }
                    if (slot == leftHand)
                    {
                        player_Animations.SwitchToOffHandShield(weapon.inGameObject);
                        Block block = gameObject.AddComponent<Block>();
                        leftAbility.SetAbility(equippedItem, Hand.Left);
                        abilityManager.SetAbility(1, block);
                    }
                    break;
                case ((int)WeaponType.Bow):
                    player_Animations.SwitchToBow(weapon.inGameObject);
                    PowerShot powerShot = gameObject.AddComponent<PowerShot>();
                    abilityManager.SetAbility(2, powerShot);
                    TripleShot tripleShot = gameObject.AddComponent<TripleShot>();
                    abilityManager.SetAbility(1, tripleShot);
                    rightAbility.SetAbility(equippedItem, Hand.Right);
                    leftAbility.SetAbility(equippedItem, Hand.Left);
                    break;
                case ((int)WeaponType.Staff):
                    player_Animations.SwitchToStaff(weapon.inGameObject);
                    StaffBasicAttack staffBasicAttack = gameObject.AddComponent<StaffBasicAttack>();
                    BigProjectile bigProjectile = gameObject.AddComponent<BigProjectile>();
                    abilityManager.SetAbility(2, bigProjectile);
                    NonProjectile nonProjectile = gameObject.AddComponent<NonProjectile>();
                    abilityManager.SetAbility(1, nonProjectile);
                    abilityManager.SetAbility(4, staffBasicAttack);
                    rightAbility.SetAbility(equippedItem, Hand.Right);
                    leftAbility.SetAbility(equippedItem, Hand.Left);
                    break;
            }
        }
        testEquipmentCount++;

        

        List<ItemObject> tempList = new List<ItemObject>();
        for (int i = 0; i < equippedItem._runeList.Length; i++)
        {
            if (equippedItem._runeList[i] != null)
            {
                tempList.Add(equippedItem._runeList[i]);
            }
        }
        if(equippedItem.item.isTwoHander) StartCoroutine(NewAffectingRune(equippedItem, tempList, IRuneScript.Hand.dual));
        else if (slot == rightHand) StartCoroutine(NewAffectingRune(equippedItem, tempList, IRuneScript.Hand.right));
        else if (slot == leftHand) StartCoroutine(NewAffectingRune(equippedItem, tempList, IRuneScript.Hand.left));

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
        RuneObject rune = (RuneObject)newItem.item;
        if (slot == weaponRightHandR1)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if(rightHand._item.item.isTwoHander) StartCoroutine(NewAffectingRune(rightHand._item, item, IRuneScript.Hand.dual));
            else StartCoroutine(NewAffectingRune(rightHand._item, item, IRuneScript.Hand.right));
            rightHand._item._runeList[0] = rune;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR1.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponRightHandR2)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (rightHand._item.item.isTwoHander) StartCoroutine(NewAffectingRune(rightHand._item, item, IRuneScript.Hand.dual));
            else StartCoroutine(NewAffectingRune(rightHand._item, item, IRuneScript.Hand.right));
            rightHand._item._runeList[1] = rune;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR2.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponRightHandR3)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (rightHand._item.item.isTwoHander) StartCoroutine(NewAffectingRune(rightHand._item, item, IRuneScript.Hand.dual));
            else StartCoroutine(NewAffectingRune(rightHand._item, item, IRuneScript.Hand.right));
            rightHand._item._runeList[2] = rune;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR3.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponRightHandR4)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (rightHand._item.item.isTwoHander) StartCoroutine(NewAffectingRune(rightHand._item, item, IRuneScript.Hand.dual));
            else StartCoroutine(NewAffectingRune(rightHand._item, item, IRuneScript.Hand.right));
            rightHand._item._runeList[3] = rune;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR4.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponRightHandR5)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (rightHand._item.item.isTwoHander) StartCoroutine(NewAffectingRune(rightHand._item, item, IRuneScript.Hand.dual));
            else StartCoroutine(NewAffectingRune(rightHand._item, item, IRuneScript.Hand.right));
            rightHand._item._runeList[4] = rune;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR5.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponRightHandR6)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (rightHand._item.item.isTwoHander) StartCoroutine(NewAffectingRune(rightHand._item, item, IRuneScript.Hand.dual));
            else StartCoroutine(NewAffectingRune(rightHand._item, item, IRuneScript.Hand.right));
            rightHand._item._runeList[5] = rune;
            if (rightHand._item.item.isTwoHander) weaponLeftHandR6.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }

        else if (slot == weaponLeftHandR1)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (leftHand._item.item.isTwoHander) StartCoroutine(NewAffectingRune(leftHand._item, item, IRuneScript.Hand.dual));
            else StartCoroutine(NewAffectingRune(leftHand._item, item, IRuneScript.Hand.left));
            leftHand._item._runeList[0] = rune;
            if (leftHand._item.item.isTwoHander) weaponRightHandR1.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponLeftHandR2)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (leftHand._item.item.isTwoHander) StartCoroutine(NewAffectingRune(leftHand._item, item, IRuneScript.Hand.dual));
            else StartCoroutine(NewAffectingRune(leftHand._item, item, IRuneScript.Hand.left));
            leftHand._item._runeList[1] = rune;
            if (leftHand._item.item.isTwoHander) weaponRightHandR2.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponLeftHandR3)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (leftHand._item.item.isTwoHander) StartCoroutine(NewAffectingRune(leftHand._item, item, IRuneScript.Hand.dual));
            else StartCoroutine(NewAffectingRune(leftHand._item, item, IRuneScript.Hand.left));
            leftHand._item._runeList[2] = rune;
            if (leftHand._item.item.isTwoHander) weaponRightHandR3.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponLeftHandR4)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (leftHand._item.item.isTwoHander) StartCoroutine(NewAffectingRune(leftHand._item, item, IRuneScript.Hand.dual));
            else StartCoroutine(NewAffectingRune(leftHand._item, item, IRuneScript.Hand.left));
            leftHand._item._runeList[3] = rune;
            if (leftHand._item.item.isTwoHander) weaponRightHandR4.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponLeftHandR5)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (leftHand._item.item.isTwoHander) StartCoroutine(NewAffectingRune(leftHand._item, item, IRuneScript.Hand.dual));
            else StartCoroutine(NewAffectingRune(leftHand._item, item, IRuneScript.Hand.left));
            leftHand._item._runeList[4] = rune;
            if (leftHand._item.item.isTwoHander) weaponRightHandR5.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }
        else if (slot == weaponLeftHandR6)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (leftHand._item.item.isTwoHander) StartCoroutine(NewAffectingRune(leftHand._item, item, IRuneScript.Hand.dual));
            else StartCoroutine(NewAffectingRune(leftHand._item, item, IRuneScript.Hand.left));
            leftHand._item._runeList[5] = rune;
            if (leftHand._item.item.isTwoHander) weaponRightHandR6.GetComponent<UiButtonClick>().SetNewItemToslot(newItem);
        }

        else if (slot == armorHeadR1)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorHead._item, item, IRuneScript.Hand.indeterminate));
            armorHead._item._runeList[0] = rune;
        }
        else if (slot == armorHeadR2)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorHead._item, item, IRuneScript.Hand.indeterminate));
            armorHead._item._runeList[1] = rune;
        }
        else if (slot == armorHeadR3)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorHead._item, item, IRuneScript.Hand.indeterminate));
            armorHead._item._runeList[2] = rune;
        }
        else if (slot == armorHeadR4)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorHead._item, item, IRuneScript.Hand.indeterminate));
            armorHead._item._runeList[3] = rune;
        }
        else if (slot == armorHeadR5)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorHead._item, item, IRuneScript.Hand.indeterminate));
            armorHead._item._runeList[4] = rune;
        }
        else if (slot == armorHeadR6)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorHead._item, item, IRuneScript.Hand.indeterminate));
            armorHead._item._runeList[5] = rune;
        }

        else if (slot == armorChestR1)
        {
            Debug.Log(slot);
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorChest._item, item, IRuneScript.Hand.indeterminate));
            armorChest._item._runeList[0] = rune;
        }
        else if (slot == armorChestR2)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorChest._item, item, IRuneScript.Hand.indeterminate));
            armorChest._item._runeList[1] = rune;
        }
        else if (slot == armorChestR3)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorChest._item, item, IRuneScript.Hand.indeterminate));
            armorChest._item._runeList[2] = rune;
        }
        else if (slot == armorChestR4)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorChest._item, item, IRuneScript.Hand.indeterminate));
            armorChest._item._runeList[3] = rune;
        }
        else if (slot == armorChestR5)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorChest._item, item, IRuneScript.Hand.indeterminate));
            armorChest._item._runeList[4] = rune;
        }
        else if (slot == armorChestR6)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorChest._item, item, IRuneScript.Hand.indeterminate));
            armorChest._item._runeList[5] = rune;
        }

        else if (slot == armorLegsR1)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorLegs._item, item, IRuneScript.Hand.indeterminate));
            armorLegs._item._runeList[0] = rune;
        }
        else if (slot == armorLegsR2)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorLegs._item, item, IRuneScript.Hand.indeterminate));
            armorLegs._item._runeList[1] = rune;
        }
        else if (slot == armorLegsR3)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorLegs._item, item, IRuneScript.Hand.indeterminate));
            armorLegs._item._runeList[2] = rune;
        }
        else if (slot == armorLegsR4)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorLegs._item, item, IRuneScript.Hand.indeterminate));
            armorLegs._item._runeList[3] = rune;
        }
        else if (slot == armorLegsR5)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorLegs._item, item, IRuneScript.Hand.indeterminate));
            armorLegs._item._runeList[4] = rune;
        }
        else if (slot == armorLegsR6)
        {
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            StartCoroutine(NewAffectingRune(armorLegs._item, item, IRuneScript.Hand.indeterminate));
            armorLegs._item._runeList[5] = rune;
        }
    }

    public IEnumerator NewAffectingRune(Item newItem, List<ItemObject> newRunes, IRuneScript.Hand hand)
    {
        yield return 0;
        if(leftHand._item == newItem || rightHand._item == newItem || armorHead._item == newItem || armorChest._item == newItem || armorLegs._item == newItem)
        {
            foreach (ItemObject item in newRunes)
            {
                RuneObject rune = (RuneObject)item;

                if (!gameObject.GetComponent(rune._IruneContainer.Result.GetType()))
                {
                    gameObject.AddComponent(rune._IruneContainer.Result.GetType());
                    IRuneScript tempRuneScript = (IRuneScript)gameObject.GetComponent(rune._IruneContainer.Result.GetType());
                    tempRuneScript.SetEntity(gameObject);
                    tempRuneScript.SetContainerItem(newItem, hand);
                }
                else
                {
                   
                }


                IRuneScript runeScript = (IRuneScript)gameObject.GetComponent(rune._IruneContainer.Result.GetType());

                if (!(newItem == leftHand._item || newItem == rightHand._item))
                {
                    if (rune.runeTier == RuneObject.RuneTier.basic)
                    {
                        runeScript.IncrementDuplicateCountArmor(1);
                    }
                    else if (rune.runeTier == RuneObject.RuneTier.refined)
                    {
                        runeScript.IncrementDuplicateCountArmor(2);
                    }
                    else if (rune.runeTier == RuneObject.RuneTier.perfected)
                    {
                        runeScript.IncrementDuplicateCountArmor(3);
                    }
                }
                else
                {
                    if (rune.runeTier == RuneObject.RuneTier.basic)
                    {
                        runeScript.IncrementDuplicateCountWeapon(1, hand);
                    }
                    else if (rune.runeTier == RuneObject.RuneTier.refined)
                    {
                        runeScript.IncrementDuplicateCountWeapon(2, hand);
                    }
                    else if (rune.runeTier == RuneObject.RuneTier.perfected)
                    {
                        runeScript.IncrementDuplicateCountWeapon(3, hand);
                    }
                }
            }
            List<IRuneScript> dublicateComponents = new List<IRuneScript>();
            foreach (ItemObject item in newRunes)
            {
                RuneObject rune = (RuneObject)item;
                if (gameObject.GetComponent(rune._IruneContainer.Result.GetType()) && !dublicateComponents.Contains(rune._IruneContainer.Result))
                {
                    IRuneScript tempRuneScript = (IRuneScript)gameObject.GetComponent(rune._IruneContainer.Result.GetType());
                    bool duplicate = false;
                    foreach(IRuneScript runeScript in dublicateComponents)
                    {
                        if(runeScript.GetType() == tempRuneScript.GetType())
                        {
                            duplicate = true;
                        }
                    }
                    if(!duplicate)
                    {
                        tempRuneScript.SetUpPermanentEffects();
                        dublicateComponents.Add(tempRuneScript);
                    }
                }
            }
        }
    }
    public void RemoveAffectingRune(Item newItem, List<ItemObject> removedRunes, IRuneScript.Hand hand)
    {
        if (leftHand._item == newItem || rightHand._item == newItem || armorHead._item == newItem || armorChest._item == newItem || armorLegs._item == newItem)
        {
            foreach(ItemObject item in removedRunes)
            {
                RuneObject rune = (RuneObject)item;
                IRuneScript runeScript = (IRuneScript)gameObject.GetComponent(rune._IruneContainer.Result.GetType());

                if (!(newItem == leftHand._item || newItem == rightHand._item))
                {
                    if (rune.runeTier == RuneObject.RuneTier.basic) runeScript.DecrementDuplicateCountArmor(1);
                    else if (rune.runeTier == RuneObject.RuneTier.refined) runeScript.DecrementDuplicateCountArmor(2);
                    else if (rune.runeTier == RuneObject.RuneTier.perfected) runeScript.DecrementDuplicateCountArmor(3);
                }
                else
                {
                    if (rune.runeTier == RuneObject.RuneTier.basic) runeScript.DecrementDuplicateCountWeapon(1, hand);
                    else if (rune.runeTier == RuneObject.RuneTier.refined) runeScript.DecrementDuplicateCountWeapon(2, hand);
                    else if (rune.runeTier == RuneObject.RuneTier.perfected) runeScript.DecrementDuplicateCountWeapon(3, hand);
                }

                if ((runeScript.GetDuplicateCountArmor() == 0 && runeScript.GetDuplicateCountWeapon() == 0)) runeScript.RemoveRune();
            }
            foreach (ItemObject item in removedRunes)
            {
                RuneObject rune = (RuneObject)item;
                if (gameObject.GetComponent(rune._IruneContainer.Result.GetType()))
                {
                    IRuneScript tempRuneScript = (IRuneScript)gameObject.GetComponent(rune._IruneContainer.Result.GetType());
                    tempRuneScript.SetUpPermanentEffects();
                }
            }
        }
    }

    public void RemoveRuneFromItem(GameObject slot)
    {
        if (slot == weaponRightHandR1)
        {
            rightHand._item._runeList[0] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (rightHand._item.item.isTwoHander) RemoveAffectingRune(rightHand._item, item, IRuneScript.Hand.dual);
            else RemoveAffectingRune(rightHand._item, item, IRuneScript.Hand.right);
            if (rightHand._item.item.isTwoHander) weaponLeftHandR1.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponRightHandR2)
        {
            rightHand._item._runeList[1] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (rightHand._item.item.isTwoHander) RemoveAffectingRune(rightHand._item, item, IRuneScript.Hand.dual);
            else RemoveAffectingRune(rightHand._item, item, IRuneScript.Hand.right);
            if (rightHand._item.item.isTwoHander) weaponLeftHandR2.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponRightHandR3)
        {
            rightHand._item._runeList[2] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (rightHand._item.item.isTwoHander) RemoveAffectingRune(rightHand._item, item, IRuneScript.Hand.dual);
            else RemoveAffectingRune(rightHand._item, item, IRuneScript.Hand.right);
            if (rightHand._item.item.isTwoHander) weaponLeftHandR3.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponRightHandR4)
        {
            rightHand._item._runeList[3] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (rightHand._item.item.isTwoHander) RemoveAffectingRune(rightHand._item, item, IRuneScript.Hand.dual);
            else RemoveAffectingRune(rightHand._item, item, IRuneScript.Hand.right);
            if (rightHand._item.item.isTwoHander) weaponLeftHandR4.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponRightHandR5)
        {
            rightHand._item._runeList[4] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (rightHand._item.item.isTwoHander) RemoveAffectingRune(rightHand._item, item, IRuneScript.Hand.dual);
            else RemoveAffectingRune(rightHand._item, item, IRuneScript.Hand.right);
            if (rightHand._item.item.isTwoHander) weaponLeftHandR5.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponRightHandR6)
        {
            rightHand._item._runeList[5] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (rightHand._item.item.isTwoHander) RemoveAffectingRune(rightHand._item, item, IRuneScript.Hand.dual);
            else RemoveAffectingRune(rightHand._item, item, IRuneScript.Hand.right);
            if (rightHand._item.item.isTwoHander) weaponLeftHandR6.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }

        else if (slot == weaponLeftHandR1)
        {
            leftHand._item._runeList[0] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item

            };
            if (leftHand._item.item.isTwoHander) RemoveAffectingRune(leftHand._item, item, IRuneScript.Hand.dual);
            else RemoveAffectingRune(leftHand._item, item, IRuneScript.Hand.left);
            if (leftHand._item.item.isTwoHander) weaponRightHandR1.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponLeftHandR2)
        {
            leftHand._item._runeList[1] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (leftHand._item.item.isTwoHander) RemoveAffectingRune(leftHand._item, item, IRuneScript.Hand.dual);
            else RemoveAffectingRune(leftHand._item, item, IRuneScript.Hand.left);
            if (leftHand._item.item.isTwoHander) weaponRightHandR2.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponLeftHandR3)
        {
            leftHand._item._runeList[2] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (leftHand._item.item.isTwoHander) RemoveAffectingRune(leftHand._item, item, IRuneScript.Hand.dual);
            else RemoveAffectingRune(leftHand._item, item, IRuneScript.Hand.left);
            if (leftHand._item.item.isTwoHander) weaponRightHandR3.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponLeftHandR4)
        {
            leftHand._item._runeList[3] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (leftHand._item.item.isTwoHander) RemoveAffectingRune(leftHand._item, item, IRuneScript.Hand.dual);
            else RemoveAffectingRune(leftHand._item, item, IRuneScript.Hand.left);
            if (leftHand._item.item.isTwoHander) weaponRightHandR4.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponLeftHandR5)
        {
            leftHand._item._runeList[4] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (leftHand._item.item.isTwoHander) RemoveAffectingRune(leftHand._item, item, IRuneScript.Hand.dual);
            else RemoveAffectingRune(leftHand._item, item, IRuneScript.Hand.left);
            if (leftHand._item.item.isTwoHander) weaponRightHandR5.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }
        else if (slot == weaponLeftHandR6)
        {
            leftHand._item._runeList[5] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            if (leftHand._item.item.isTwoHander) RemoveAffectingRune(leftHand._item, item, IRuneScript.Hand.dual);
            else RemoveAffectingRune(leftHand._item, item, IRuneScript.Hand.left);
            if (leftHand._item.item.isTwoHander) weaponRightHandR6.GetComponent<UiButtonClick>().RemoveItemFromslot();
        }

        else if (slot == armorHeadR1)
        {
            armorHead._item._runeList[0] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorHead._item, item, IRuneScript.Hand.indeterminate);
        }
        else if (slot == armorHeadR2)
        {
            armorHead._item._runeList[1] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorHead._item, item, IRuneScript.Hand.indeterminate);
        }
        else if (slot == armorHeadR3)
        {
            armorHead._item._runeList[2] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorHead._item, item, IRuneScript.Hand.indeterminate);
        }
        else if (slot == armorHeadR4)
        {
            armorHead._item._runeList[3] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorHead._item, item, IRuneScript.Hand.indeterminate);
        }
        else if (slot == armorHeadR5)
        {
            armorHead._item._runeList[4] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorHead._item, item, IRuneScript.Hand.indeterminate);
        }
        else if (slot == armorHeadR6)
        {
            armorHead._item._runeList[5] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorHead._item, item, IRuneScript.Hand.indeterminate);
        }

        else if (slot == armorChestR1)
        {
            armorChest._item._runeList[0] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorChest._item, item, IRuneScript.Hand.indeterminate);
        }
        else if (slot == armorChestR2)
        {
            armorChest._item._runeList[1] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorChest._item, item, IRuneScript.Hand.indeterminate);
        }
        else if (slot == armorChestR3)
        {
            armorChest._item._runeList[2] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorChest._item, item, IRuneScript.Hand.indeterminate);
        }
        else if (slot == armorChestR4)
        {
            armorChest._item._runeList[3] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorChest._item, item, IRuneScript.Hand.indeterminate);
        }
        else if (slot == armorChestR5)
        {
            armorChest._item._runeList[4] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorChest._item, item, IRuneScript.Hand.indeterminate);
        }
        else if (slot == armorChestR6)
        {
            armorChest._item._runeList[5] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorChest._item, item, IRuneScript.Hand.indeterminate);
        }

        else if (slot == armorLegsR1)
        {
            armorLegs._item._runeList[0] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorLegs._item, item, IRuneScript.Hand.indeterminate);
        }
        else if (slot == armorLegsR2)
        {
            armorLegs._item._runeList[1] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorLegs._item, item, IRuneScript.Hand.indeterminate);
        }
        else if (slot == armorLegsR3)
        {
            armorLegs._item._runeList[2] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorLegs._item, item, IRuneScript.Hand.indeterminate);
        }
        else if (slot == armorLegsR4)
        {
            armorLegs._item._runeList[3] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorLegs._item, item, IRuneScript.Hand.indeterminate);
        }
        else if (slot == armorLegsR5)
        {
            armorLegs._item._runeList[4] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorLegs._item, item, IRuneScript.Hand.indeterminate);
        }
        else if (slot == armorLegsR6)
        {
            armorLegs._item._runeList[5] = null;
            List<ItemObject> item = new List<ItemObject>
            {
                slot.GetComponent<UiButtonClick>()._item.item
            };
            RemoveAffectingRune(armorLegs._item, item, IRuneScript.Hand.indeterminate);
        }
    }
    public void DropItem(Item droppedItem)
    {
        Debug.Log("Dropped item " + droppedItem.item.name);
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        GameObject groundItem = Instantiate(itemOnGround, pos, Quaternion.identity);
        groundItem.GetComponent<ItemOnGround>()._item = droppedItem;
        //groundItem.GetComponent<SpriteRenderer>().sprite = droppedItem.item.iconSprite;
    }
    public void DropItem(Item droppedItem, Vector2 pos)
    {
        Debug.Log("Dropped item " + droppedItem.item.name);



        GameObject groundItem = Instantiate(itemOnGround, pos, Quaternion.identity);
        groundItem.GetComponent<ItemOnGround>()._item = droppedItem;
        //groundItem.GetComponent<SpriteRenderer>().sprite = droppedItem.item.iconSprite;
    }
}
