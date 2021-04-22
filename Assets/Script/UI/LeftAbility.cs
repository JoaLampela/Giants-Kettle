using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LeftAbility : MonoBehaviour
{
    public Image iconFill;
    public Image icon;
    private bool isCooldown = false;
    private Item _item;
    private bool usedLeftAbility = false;
    [SerializeField] private EntityStats stats;

    [SerializeField] private Sprite iconSprite;



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
        if(_item != null)
        {
            if (_item.currentCooldownAbility2 > 0) usedLeftAbility = true;
            if (isCooldown == false && Time.timeScale != 0 && usedLeftAbility)
            {
                isCooldown = true;
                iconFill.fillAmount = 1;
            }

            if (isCooldown && Time.timeScale != 0)
            {
                iconFill.fillAmount = _item.currentCooldownAbility2/_item.maxCooldownAbility2;
                if (iconFill.fillAmount <= 0)
                {
                    iconFill.fillAmount = 0;
                    isCooldown = false;
                    usedLeftAbility = false;
                }
            }
        }
    }


    public void SetAbility(Item item, Inventory.Hand hand)
    {
        _item = item;
        SetIcon();
    }

    public void RemoveAbility()
    {
        _item = null;
        SetIcon();
    }

    void SetIcon()
    {
        if(_item == null)
        {
            iconFill.sprite = null;
            icon.sprite = null;
        }
        else
        {
            Debug.Log("Else in 2");
            iconFill.sprite = iconSprite;
            icon.sprite = iconSprite;
        }
    }
}
