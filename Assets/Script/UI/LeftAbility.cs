using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LeftAbility : MonoBehaviour
{
    public Image iconFill;
    public Image icon;



    private bool isCooldown = false;
    private Item _item = null;
    private bool usedLeftAbility = false;
    [SerializeField] private EntityStats stats;

    [SerializeField] private Sprite iconSpriteStingLeft;
    [SerializeField] private Sprite iconSpriteSpinAttack;
    [SerializeField] private Sprite iconSpriteBlock;
    [SerializeField] private Sprite iconSpriteNonProjectile;



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
        SetIcon(item);
    }

    public void RemoveAbility()
    {
        _item = null;
        SetIcon(_item);
    }

    void SetIcon(Item item)
    {
        if(_item == null)
        {
            iconFill.sprite = null;
            icon.sprite = null;
        }
        else
        {
            Debug.Log("Else in 2");
            WeaponObject weapon = (WeaponObject)item.item;
            if(weapon.weaponType == WeaponObject.WeaponType.OneHandedSword)
            {
                iconFill.sprite = iconSpriteStingLeft;
                icon.sprite = iconSpriteStingLeft;
            }
            if (weapon.weaponType == WeaponObject.WeaponType.TwoHandedSword)
            {
                iconFill.sprite = iconSpriteSpinAttack;
                icon.sprite = iconSpriteSpinAttack;
            }
            if (weapon.weaponType == WeaponObject.WeaponType.Shield)
            {
                iconFill.sprite = iconSpriteBlock;
                icon.sprite = iconSpriteBlock;
            }
            if (weapon.weaponType == WeaponObject.WeaponType.Staff)
            {
                iconFill.sprite = iconSpriteNonProjectile;
                icon.sprite = iconSpriteNonProjectile;
            }

            
        }
    }
}
