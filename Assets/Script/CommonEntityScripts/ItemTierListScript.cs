using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTierListScript : MonoBehaviour
{
    public static ItemTierListScript Instance { get; private set; }

    public ItemObject[] tier0Items; //items with 0 runeSlots
    public ItemObject[] tier1Items; //items with 1 runeSlots
    public ItemObject[] tier2Items; //items with 2 runeSlots
    public ItemObject[] tier3Items; //items with 3 runeSlots
    public ItemObject[] tier4Items; //items with 4 runeSlots
    public ItemObject[] tier5Items; //items with 5 runeSlots
    public ItemObject[] tier6Items; //items with 6 runeSlots

    public ItemObject GiveRandomItem(int tier)
    {
        switch (tier)
        {
            case 0:
                return tier0Items[Random.Range(0, tier0Items.Length)];
            case 1:
                return tier1Items[Random.Range(0, tier1Items.Length)];
            case 2:
                return tier2Items[Random.Range(0, tier2Items.Length)];
            case 3:
                return tier3Items[Random.Range(0, tier3Items.Length)];
            case 4:
                return tier4Items[Random.Range(0, tier4Items.Length)];
            case 5:
                return tier5Items[Random.Range(0, tier5Items.Length)];
            case 6:
                return tier6Items[Random.Range(0, tier6Items.Length)];
        }
        return tier0Items[Random.Range(0, tier0Items.Length)];
    }
}
