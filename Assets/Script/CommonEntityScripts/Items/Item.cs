using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public Rune[] _runeList;
    public ItemObject item;

    public Item(ItemObject newItem)
    {
        item = newItem;
        if((int)item.type == 1)
        {
            EquipmentObject equipment = (EquipmentObject)item;
            _runeList = new Rune[equipment.runeSlots];
        }
    }
}
