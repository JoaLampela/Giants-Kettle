using UnityEngine;

[CreateAssetMenu(fileName = "New Rune Object", menuName = "Inventory System/Items/Rune")]
public class RuneObject : ItemObject
{
    public IRuneScriptContainer _IruneContainer = null;
    public RuneTier runeTier;
    public enum RuneTier
    {
        basic,
        refined,
        perfected
    }
    public void Awake()
    {
        type = ItemType.Rune;
    }
}
