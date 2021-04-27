using UnityEngine;

[CreateAssetMenu(fileName = "New Rune Object", menuName = "Inventory System/Items/Rune")]
public class RuneObject : ItemObject
{
    public IRuneScriptContainer _IruneContainer = null;
    public RuneTier runeTier;
    public RuneType runeType;
    public enum RuneTier
    {
        basic,
        refined,
        perfected
    }

    public enum RuneType
    {
        spirit,
        vitality,
        agility,
        power,
        spiritVitality,
        spiritAgility,
        spiritPower,
        vitalityAgility,
        vitalityPower,
        agilitypower
    }

    public void Awake()
    {
        type = ItemType.Rune;
    }
}
