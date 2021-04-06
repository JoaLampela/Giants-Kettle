using UnityEngine;

[CreateAssetMenu(fileName = "New ArmorLegs Object", menuName = "Inventory System/Items/ArmorLegs")]
public class ArmorLegsObject : EquipmentObject
{
    public void Awake()
    {
        type = ItemType.ArmorLegs;
    }
}
