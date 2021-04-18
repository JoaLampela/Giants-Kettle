using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RightAbility : MonoBehaviour
{
    public Image iconFill;
    public Image icon;
    private bool isCooldown = false;
    private Item _item;
    private bool usedRightAbility = false;


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
        if (_item != null)
        {
            if (_item.currentCooldownAbility1 > 0) usedRightAbility = true;
            if (isCooldown == false && Time.timeScale != 0 && usedRightAbility)
            {
                Debug.Log("Used Right");
                isCooldown = true;
                iconFill.fillAmount = 1;
            }

            if (isCooldown && Time.timeScale != 0)
            {
                iconFill.fillAmount -= 1 / _item.maxCooldownAbility1 * Time.deltaTime;
                if (iconFill.fillAmount <= 0)
                {
                    iconFill.fillAmount = 0;
                    isCooldown = false;
                    usedRightAbility = false;
                }
            }
        }
    }


    public void SetAbility(Item item, Inventory.Hand hand)
    {
        
        _item = item;
        Debug.Log("New Ability Set " + _item.item);
        SetIcon();
    }

    public void RemoveAbility()
    {
        Debug.Log("Removed ability");
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
            Debug.Log("Else");
            iconFill.sprite = iconSprite;
            icon.sprite = iconSprite;
        }
    }
}
