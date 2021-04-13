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

    public float maxCooldownAbility1;
    public float currentCooldownAbility1;

    public float maxCooldownAbility2;
    public float currentCooldownAbility2;
}
