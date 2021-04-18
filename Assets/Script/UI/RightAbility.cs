using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RightAbility : MonoBehaviour
{
    public Image icon;
    public Image iconBG;
    private float cooldown;
    private bool isCooldown = false;
    private Item _item = null;



    private void Start()
    {
        SetIcon();
    }

    private void Update()
    {
        icon.fillAmount = 0;
        AbilityUpdate();
    }



    void AbilityUpdate()
    {
        if (_item != null)
        {
            if (isCooldown == false && Time.timeScale != 0)
            {
                isCooldown = true;
                icon.fillAmount = 1;
            }

            if (isCooldown && Time.timeScale != 0)
            {
                icon.fillAmount -= 1 / cooldown * Time.deltaTime;

                if (icon.fillAmount <= 0)
                {
                    icon.fillAmount = 0;
                    isCooldown = false;
                    cooldown = _item.currentCooldownAbility1;
                }
            }
        }
    }


    void SetAbility(Item item)
    {
        _item = item;
        cooldown = _item.currentCooldownAbility1;
        SetIcon();
    }

    void RemoveAbility()
    {
        _item = null;
        SetIcon();
    }

    void SetIcon()
    {
        if (_item == null)
        {
            icon.sprite = null;
            iconBG.sprite = null;
        }
        else
        {
            icon.sprite = Resources.Load<Sprite>("Assets/Import/goblin_1_1");
            iconBG.sprite = Resources.Load<Sprite>("Assets/Import/goblin_1_1");
        }
    }
}
