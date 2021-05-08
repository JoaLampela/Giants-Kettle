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
    public ItemObject[] tier100Items; //mythic items

    public ItemObject GiveRandomItem(int tempTier)
    {
        if (Random.Range(1, 101) > 95) return tier1Items[Random.Range(0, tier100Items.Length)];

        int count = 0;
        for (int i = 0; i <= tempTier; i++)
        {
            count += i;
        }
        int tier = Random.Range(1, count + 2);
        if (tier > 26) return tier6Items[Random.Range(0, tier6Items.Length)];
        else if (tier > 14) return tier5Items[Random.Range(0, tier5Items.Length)];
        else if (tier > 8) return tier4Items[Random.Range(0, tier4Items.Length)];
        else if (tier > 3) return tier3Items[Random.Range(0, tier3Items.Length)];
        else if (tier > 1) return tier2Items[Random.Range(0, tier2Items.Length)];
        else if (tier > 0) return tier1Items[Random.Range(0, tier1Items.Length)];
        else return tier1Items[Random.Range(0, tier1Items.Length)];
    }
}
