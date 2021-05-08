using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRuneOfSupremeArcadePower : MonoBehaviour, IRuneScript
{
    private AbilityEvents _abilityEvents;
    private GameObject _entity = null;
    private EntityEvents _entityEvents;
    private WeaponType _weaponType;
    [SerializeField] private int duplicateCountWeapon = 0;
    [SerializeField] private int duplicateCountArmor = 0;
    private Item containerItem;
    private IRuneScript.Hand _hand;
    private GameObject explosion;

    [SerializeField] private int duplicateCountWeaponRight = 0;
    [SerializeField] private int duplicateCountWeaponLeft = 0;

    //Always needed functions
    public enum WeaponType
    {
        OneHandedSword,
        TwoHandedSword,
        Shield,
        Bow,
        Staff
    }

    public void SetDuplicateCountWeapon(int value)
    {
        duplicateCountWeapon = value;
    }

    public void IncrementDuplicateCountWeapon(int amount, IRuneScript.Hand hand)
    {
        if (hand == IRuneScript.Hand.right || hand == IRuneScript.Hand.dual)
        {
            duplicateCountWeaponRight += amount;
        }
        if (hand == IRuneScript.Hand.left || hand == IRuneScript.Hand.dual)
        {
            duplicateCountWeaponLeft += amount;
        }

        duplicateCountWeapon += amount;
        if (_entityEvents != null) SetUpPermanentEffects();
    }

    public void DecrementDuplicateCountWeapon(int amount, IRuneScript.Hand hand)
    {
        if (hand == IRuneScript.Hand.right || hand == IRuneScript.Hand.dual)
        {
            duplicateCountWeaponRight -= amount;
        }
        if (hand == IRuneScript.Hand.left || hand == IRuneScript.Hand.dual)
        {
            duplicateCountWeaponLeft -= amount;
        }

        duplicateCountWeapon -= amount;
    }

    public void IncrementDuplicateCountArmor(int amount)
    {
        duplicateCountArmor += amount;
        if (_entityEvents != null) SetUpPermanentEffects();
    }

    public void DecrementDuplicateCountArmor(int amount)
    {
        duplicateCountArmor -= amount;
    }

    public int GetDuplicateCountWeapon()
    {
        return duplicateCountWeapon;
    }

    public int GetDuplicateCountArmor()
    {
        return duplicateCountArmor;
    }

    public void RemoveRune()
    {
        Destroy(this);
    }

    public void SetEntity(GameObject entity)
    {
        _entity = entity;
        _entityEvents = entity.GetComponent<EntityEvents>();
        if (_entityEvents != null) SetUpPermanentEffects();
    }

    public void SetWeaponType(IRuneScript.WeaponType weaponType)
    {
        if (weaponType == IRuneScript.WeaponType.OneHandedSword) _weaponType = WeaponType.OneHandedSword;
        else if (weaponType == IRuneScript.WeaponType.TwoHandedSword) _weaponType = WeaponType.TwoHandedSword;
        else if (weaponType == IRuneScript.WeaponType.Shield) _weaponType = WeaponType.Shield;
        else if (weaponType == IRuneScript.WeaponType.Bow) _weaponType = WeaponType.Bow;
        else if (weaponType == IRuneScript.WeaponType.Staff) _weaponType = WeaponType.Staff;
    }

    public void SetUpPermanentEffects()
    {
        _entityEvents.RemoveBuff("SuperRuneOfSupremeArcadePowerSpellHaste");
        _entityEvents.RemoveBuff("SuperRuneOfSupremeArcadePowerPhysicalDamage");
        _entityEvents.RemoveBuff("SuperRuneOfSupremeArcadePowerAttackSpeed");

        _entityEvents.NewBuff("SuperRuneOfSupremeArcadePowerSpellHaste", EntityStats.BuffType.SpellHaste, (duplicateCountArmor + duplicateCountWeapon) * 10);
        _entityEvents.NewBuff("SuperRuneOfSupremeArcadePowerPhysicalDamage", EntityStats.BuffType.PhysicalDamage, (duplicateCountArmor + duplicateCountWeapon) * 10);
        _entityEvents.NewBuff("SuperRuneOfSupremeArcadePowerAttackSpeed", EntityStats.BuffType.AttackSpeed, (duplicateCountArmor + duplicateCountWeapon) * 10);
    }

    //Subs & Unsub -related Unity functions
    private void Start()
    {
        if (gameObject.GetComponent<EntityEvents>())
        {
            SubscribeEntity();
        }

        if (gameObject.GetComponent<AbilityEvents>())
        {
            SubscribeAbility();
        }
    }

    private void Awake()
    {
        _entityEvents = gameObject.GetComponent<EntityEvents>();
        _abilityEvents = gameObject.GetComponent<AbilityEvents>();
    }

    private void OnDisable()
    {
        if (_entityEvents != null) _entityEvents.RemoveBuff("SuperRuneOfSupremeArcadePowerSpellHaste");
        if (_entityEvents != null) _entityEvents.RemoveBuff("SuperRuneOfSupremeArcadePowerPhysicalDamage");
        if (_entityEvents != null) _entityEvents.RemoveBuff("SuperRuneOfSupremeArcadePowerAttackSpeed");

        if (gameObject.GetComponent<EntityEvents>())
        {
            UnsubscribeEntity();
        }

        if (gameObject.GetComponent<AbilityEvents>())
        {
            UnsubscribeAbility();
        }
    }

    public void Activate(Damage damage, GameObject target)
    {
        GameObject rainbow = RuneAssets.i.RuneRainbow;
        rainbow = Instantiate(rainbow, target.transform.position, Quaternion.identity);

        target.GetComponent<EntityEvents>().NewBuff("Arcade", EntityStats.BuffType.ArcadeBurn, 1, 5);
    }

    //Subs and Unsubs
    public void SubscribeAbility()
    {
        _abilityEvents._onDestroy += UnsubscribeAbility;
        _abilityEvents._onDealDamage += Activate;
    }

    public void SubscribeEntity()
    {
        
    }

    public void UnsubscribeAbility()
    {
        _abilityEvents._onDestroy -= UnsubscribeAbility;
        _abilityEvents._onDealDamage -= Activate;
    }

    public void UnsubscribeEntity()
    {

    }

    public void SetContainerItem(Item item, IRuneScript.Hand hand)
    {
        containerItem = item;
        _hand = hand;
    }

    public int GetDuplicateCountWeaponRight()
    {
        return duplicateCountWeaponRight;
    }

    public int GetDuplicateCountWeaponLeft()
    {
        return duplicateCountWeaponLeft;
    }
}
