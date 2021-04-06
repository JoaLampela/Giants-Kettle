using UnityEngine;

[CreateAssetMenu(fileName = "New OneHandedSword Object", menuName = "Inventory System/Items/OneHandedSword")]
public class OneHandedSwordObject : WeaponObject
{
    public void Awake()
    {
        type = ItemType.Weapon;
        weaponType = WeaponType.OneHandedSword;
    }
}
