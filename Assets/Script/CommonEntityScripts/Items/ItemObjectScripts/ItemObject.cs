using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
public class ItemObject : ScriptableObject
{
    public ItemType type;
    [TextArea(15,20)]
    public string description;

    
    public Sprite iconSprite;
    public GameObject inGameObject;
    [HideInInspector] public bool isTwoHander;
}

[System.Serializable]
public class ItemBuff
{
    public string sourceID;
    public EntityStats.BuffType effectID;
    public int effectiveness;
    public float duration;
}
