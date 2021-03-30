using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public int inventorySlot;
    public string Itemname;
    public string description;
    public Sprite sprite;
    public bool isTwoHander;
    public List<ItemBuff> buffs;
    public string scriptName;
}


[System.Serializable]
public class ItemBuff
{
    public string sourceID;
    public string effectID;
    public int effectiveness;
    public float duration;
}
