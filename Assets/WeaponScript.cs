using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private Item item;

    [SerializeField] private Rune[] runes;

    private void Start()
    {
        runes = new Rune[item.runeSlots];
        if (item.baseRune != null) runes[0] = item.baseRune;
    }

    public void AddNewRune(int slot, Rune rune)
    {
        if(item.baseRune != null && slot == 0)
        {
            //Return rune
        }
        else if(runes[slot] != null)
        {
            //Return old rune
            runes[slot] = rune;
        }
        else
        {
            runes[slot] = rune;
        }
    }

    public void DropItem()
    {
        for(int i = 0; i < item.runeSlots; i++)
        {
            if(i == 0)
            {
                if (item.baseRune == null)
                {
                    //Return rune
                }
            }
            else if(runes[i] != null)
            {
                //Return rune
            }
        }
    }
}
