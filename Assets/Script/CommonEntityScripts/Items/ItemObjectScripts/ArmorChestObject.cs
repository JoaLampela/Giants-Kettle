using UnityEngine;

[CreateAssetMenu(fileName = "New ArmorChest Object", menuName = "Inventory System/Items/ArmorChest")]
public class ArmorChestObject : EquipmentObject
{
    public void Awake()
    {
        type = ItemType.ArmorChest;
    }
}
