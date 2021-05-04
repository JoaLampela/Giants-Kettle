using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RightAbility : MonoBehaviour
{
    public Image iconFill;
    public Image icon;
    private bool isCooldown = false;
    private Item _item = null;
    private bool usedRightAbility = false;
    [SerializeField] private EntityStats stats;



    [SerializeField] private Sprite iconSpriteStingRight;
    [SerializeField] private Sprite iconSpriteStingTwoHanded;
    [SerializeField] private Sprite iconSpriteShieldToss;
    [SerializeField] private Sprite iconSpriteBigProjectile;

    private void Start()
    {
        iconFill.fillAmount = 0;
        SetIcon(_item);
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
                isCooldown = true;
                iconFill.fillAmount = 1;
            }

            if (isCooldown && Time.timeScale != 0)
            {
                iconFill.fillAmount = _item.currentCooldownAbility1 / _item.maxCooldownAbility1;
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
        SetIcon(_item);
    }

    public void RemoveAbility()
    {
        _item = null;
        SetIcon(_item);
    }

    void SetIcon(Item item)
    {
        if (_item == null)
        {
            iconFill.sprite = null;
            icon.sprite = null;
        }
        else
        {
            WeaponObject weapon = (WeaponObject)item.item;
            if (weapon.weaponType == WeaponObject.WeaponType.OneHandedSword)
            {
                iconFill.sprite = iconSpriteStingRight;
                icon.sprite = iconSpriteStingRight;
            }
            if (weapon.weaponType == WeaponObject.WeaponType.TwoHandedSword)
            {
                iconFill.sprite = iconSpriteStingTwoHanded;
                icon.sprite = iconSpriteStingTwoHanded;
            }
            if (weapon.weaponType == WeaponObject.WeaponType.Shield)
            {
                iconFill.sprite = iconSpriteShieldToss;
                icon.sprite = iconSpriteShieldToss;
            }
            if (weapon.weaponType == WeaponObject.WeaponType.Staff)
            {
                iconFill.sprite = iconSpriteBigProjectile;
                icon.sprite = iconSpriteBigProjectile;
            }
        }
    }
}
