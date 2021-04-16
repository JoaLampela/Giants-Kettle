using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public RuneObject[] _runeList;
    public ItemObject item;

    public float maxCooldownAbility1;
    public float currentCooldownAbility1;

    public float maxCooldownAbility2;
    public float currentCooldownAbility2;

    public Item(ItemObject newItem)
    {
        item = newItem;
        if(item.type == ItemType.Rune)
        {
            _runeList = new RuneObject[0];
        }
        else if((int)item.type == 1 || (int)item.type == 2 || (int)item.type == 3 || (int)item.type == 4)
        {
            EquipmentObject equipment = (EquipmentObject)item;
            _runeList = new RuneObject[equipment.runeSlots];

            EquipmentObject equipmentObject = (EquipmentObject)item;
            if (equipmentObject.baseRune != null) _runeList[0] = equipmentObject.baseRune;
        }
        else
        {
            _runeList = new RuneObject[0];
        }
        if (item.type == ItemType.Weapon)
        {
            Debug.Log("ADDED " + item);
            WeaponObject weapon = (WeaponObject)item;
            maxCooldownAbility1 = weapon.maxCooldownAbility1;
            currentCooldownAbility1 = weapon.currentCooldownAbility1;

            maxCooldownAbility2 = weapon.maxCooldownAbility2;
            currentCooldownAbility2 = weapon.maxCooldownAbility2;
            GameAbilityCoolDownManager.Instance.weaponsOnCooldown.Add(this);
        }
    }
}
