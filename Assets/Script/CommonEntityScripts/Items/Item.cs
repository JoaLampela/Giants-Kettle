using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public RuneObject[] _runeList;
    public ItemObject item;

    [HideInInspector] public float baseMaxCooldownAbility1;
    public float maxCooldownAbility1;
    public float currentCooldownAbility1;

    [HideInInspector] public float baseMaxCooldownAbility2;
    public float maxCooldownAbility2;
    public float currentCooldownAbility2;

    public Sprite[] runeEffects;



    public Item(ItemObject newItem)
    {
        item = newItem;
        runeEffects = new Sprite[0];
        if(item.type == ItemType.Rune)
        {
            _runeList = new RuneObject[0];
            RuneObject rune = (RuneObject)newItem;
            runeEffects = rune.runeEffects;

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
            WeaponObject weapon = (WeaponObject)item;
            maxCooldownAbility1 = weapon.maxCooldownAbility1;
            currentCooldownAbility1 = weapon.currentCooldownAbility1;
            baseMaxCooldownAbility1 = maxCooldownAbility1;

            maxCooldownAbility2 = weapon.maxCooldownAbility2;
            currentCooldownAbility2 = weapon.maxCooldownAbility2;
            GameAbilityCoolDownManager.Instance.weaponsOnCooldown.Add(this);
            baseMaxCooldownAbility2 = maxCooldownAbility2;
        }
    }
}
