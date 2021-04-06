using UnityEngine;

[CreateAssetMenu(fileName = "New ArmorHead Object", menuName = "Inventory System/Items/ArmorHead")]
public class ArmorHeadObject : EquipmentObject
{
    public void Awake()
    {
        type = ItemType.ArmorHead;
    }
}
