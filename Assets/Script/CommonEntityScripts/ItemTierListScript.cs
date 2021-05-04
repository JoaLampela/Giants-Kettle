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

    public ItemObject GiveRandomItem(int tempTier)
    {
        int tier = Random.Range(1, tempTier);
        Debug.Log(tier);
        return tier switch
        {
            0 => tier0Items[Random.Range(0, tier0Items.Length)],
            1 => tier1Items[Random.Range(0, tier1Items.Length)],
            2 => tier2Items[Random.Range(0, tier2Items.Length)],
            3 => tier3Items[Random.Range(0, tier3Items.Length)],
            4 => tier4Items[Random.Range(0, tier4Items.Length)],
            5 => tier5Items[Random.Range(0, tier5Items.Length)],
            6 => tier6Items[Random.Range(0, tier6Items.Length)],
            _ => tier6Items[Random.Range(0, tier0Items.Length)],
        };
    }
}
