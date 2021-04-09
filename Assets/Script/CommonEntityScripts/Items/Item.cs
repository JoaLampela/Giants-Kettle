using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public RuneObject[] _runeList;
    public ItemObject item;

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
    }
}
