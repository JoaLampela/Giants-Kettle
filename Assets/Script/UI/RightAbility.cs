using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RightAbility : MonoBehaviour
{
    public Image iconFill;
    public Image icon;
    private float cooldown;
    private bool isCooldown = false;
    private Item _item = null;



    private void Start()
    {
        iconFill.fillAmount = 0;
        SetIcon();
    }

    private void Update()
    {
        AbilityUpdate();
    }



    void AbilityUpdate()
    {
        if (_item != null)
        {
            if (isCooldown == false && Time.timeScale != 0)
            {
                isCooldown = true;
                iconFill.fillAmount = 1;
            }

            if (isCooldown && Time.timeScale != 0)
            {
                iconFill.fillAmount -= 1 / cooldown * Time.deltaTime;

                if (iconFill.fillAmount <= 0)
                {
                    iconFill.fillAmount = 0;
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
            iconFill.sprite = null;
            icon.sprite = null;
        }
        else
        {
            iconFill.sprite = Resources.Load<Sprite>("Assets/Import/goblin_1_1");
            icon.sprite = Resources.Load<Sprite>("Assets/Import/goblin_1_1");
        }
    }
}
