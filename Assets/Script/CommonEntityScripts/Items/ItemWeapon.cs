using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemWeapon : Item
{
    [Header("1 = 1handedSword, 3 = staff, 4 = bow")]
    public int weaponType;
    public int physicalDamage;
    public int spiritDamage;
    public string perk;
}

