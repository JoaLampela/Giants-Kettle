using UnityEngine;

[CreateAssetMenu(fileName = "New TwoHandedSword Object", menuName = "Inventory System/Items/TwoHandedSword")]
public class TwoHandedSwordObject : WeaponObject
{
    public void Awake()
    {
        type = ItemType.Weapon;
        weaponType = WeaponType.TwoHandedSword;
        isTwoHander = true;
    }
}
