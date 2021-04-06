using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : EquipmentObject
{
    public enum WeaponType
    {
        OneHandedSword,
        TwoHandedSword,
        Shield,
        Bow,
        Staff
    }

    public WeaponType weaponType;
}
