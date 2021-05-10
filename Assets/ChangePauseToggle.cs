using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePauseToggle : MonoBehaviour
{
    public Toggle toggle;
    void Start()
    {
        int isOn = PlayerPrefs.GetInt("InventoryToggle", 1);
        if(isOn == 1)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
    }


    private void SaveToggle(bool pasta)
    {
        if (pasta)
        {
            PlayerPrefs.SetInt("InventoryToggle", 1);
        }
        else
        {
            PlayerPrefs.SetInt("InventoryToggle", 0);
        }
    }

    public void SetPauseBool(bool pause)
    {
        InventoryGamePauseToggle.SetToggle(pause);
        SaveToggle(pause);
    }
}
