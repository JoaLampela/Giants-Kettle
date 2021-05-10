using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGamePauseToggle
{
     
    public static bool pauseWhenOpen = true;
    public static bool inventoryOpen = false;


    public static void SetToggle(bool pause)
    {
        pauseWhenOpen = pause;
    }
}
