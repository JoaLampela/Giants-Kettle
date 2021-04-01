using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemWeapon : Item
{
    [Header("1 = 1handedSword, 2 = 2handedSword, 3 = shield, 4 = bow, 5 = staff")]
    public int weaponType;
    public int physicalDamage;
    public int spiritDamage;
}
