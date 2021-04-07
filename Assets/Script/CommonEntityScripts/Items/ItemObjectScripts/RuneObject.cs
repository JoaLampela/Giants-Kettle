using UnityEngine;

[CreateAssetMenu(fileName = "New Rune Object", menuName = "Inventory System/Items/Rune")]
public class RuneObject : ItemObject
{
    public IRuneScriptContainer _IruneContainer = null;
    public void Awake()
    {
        type = ItemType.Rune;
    }
}
