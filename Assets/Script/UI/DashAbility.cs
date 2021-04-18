using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DashAbility : MonoBehaviour
{
    public Image icon;
    private bool isCooldown = false;
    private Item dash;
    private bool dashed = false;

    private void Start()
    {
        icon.fillAmount = 0;
    }

    private void Update()
    {
        AbilityUpdate();
    }

    void AbilityUpdate()
    {
        
        if(dash == null) dash = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().dashItem;
        if (dash.currentCooldownAbility1 > 0) dashed = true;
        if (!isCooldown && Time.timeScale != 0 && dashed)
        {
            isCooldown = true;
            icon.fillAmount = 1;
        }

        if (isCooldown && Time.timeScale != 0)
        {
            icon.fillAmount -= 1 / dash.maxCooldownAbility1 * Time.deltaTime;

            if(icon.fillAmount <= 0)
            {
                icon.fillAmount = 0;
                isCooldown = false;
                dashed = false;
            }
        }
    }
}
