using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bow Object", menuName = "Inventory System/Items/Bow")]
public class BowObject : WeaponObject
{
    public void Awake()
    {
        type = ItemType.Weapon;
        weaponType = WeaponType.Bow;
        isTwoHander = true;
    }
}
