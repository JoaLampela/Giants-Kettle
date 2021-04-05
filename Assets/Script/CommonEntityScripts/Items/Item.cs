using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public string Itemname;
    public int inventorySlot;
    public string description;
    public Sprite iconSprite;
    public Sprite inGameSprite;
    public bool isTwoHander;
    public List<ItemBuff> buffs;
    public int runeSlots;
    public Rune baseRune;
    public string type;
}

[System.Serializable]
public class ItemBuff
{
    public string sourceID;
    public string effectID;
    public int effectiveness;
    public float duration;
}
