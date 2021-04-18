using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DashAbility : MonoBehaviour
{
    public Image icon;
    private float cooldown;
    private bool isCooldown = false;
    private Item dash;



    private void Start()
    {
        dash = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().dashItem;
        cooldown = dash.currentCooldownAbility1;
    }

    private void Update()
    {
        AbilityUpdate();
    }

    void AbilityUpdate()
    {
        if(isCooldown == false && Time.timeScale != 0)
        {
            isCooldown = true;
            icon.fillAmount = 1;
        }

        if (isCooldown && Time.timeScale != 0)
        {
            icon.fillAmount -= 1 / cooldown * Time.deltaTime;

            if(icon.fillAmount <= 0)
            {
                icon.fillAmount = 0;
                isCooldown = false;
                cooldown = dash.currentCooldownAbility1;
            }
        }
    }
}
