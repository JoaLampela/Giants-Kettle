using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Staff Object", menuName = "Inventory System/Items/Staff")]
public class StaffObject : WeaponObject
{
    public void Awake()
    {
        type = ItemType.Weapon;
        weaponType = WeaponType.Staff;
        isTwoHander = true;
    }
}
