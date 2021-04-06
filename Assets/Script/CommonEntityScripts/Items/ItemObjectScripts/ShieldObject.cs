using UnityEngine;

[CreateAssetMenu(fileName = "New Shield Object", menuName = "Inventory System/Items/Shield")]
public class ShieldObject : WeaponObject
{
    public void Awake()
    {
        type = ItemType.Weapon;
        weaponType = WeaponType.Shield;
    }
}
