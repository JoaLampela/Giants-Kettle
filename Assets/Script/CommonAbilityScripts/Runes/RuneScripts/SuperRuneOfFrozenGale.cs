using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRuneOfFrozenGale : MonoBehaviour, IRuneScript
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
        _entityEvents.RemoveBuff("SuperRuneOfFrozenGaleSpellHaste");
        _entityEvents.RemoveBuff("SuperRuneOfFrozenGalePhysicalDamage");

        _entityEvents.NewBuff("SuperRuneOfSupremeArcadePowerSpellHaste", EntityStats.BuffType.SpellHaste, (duplicateCountArmor + duplicateCountWeapon) * 10);
        _entityEvents.NewBuff("SuperRuneOfSupremeArcadePowerPhysicalDamage", EntityStats.BuffType.PhysicalDamage, (duplicateCountArmor + duplicateCountWeapon) * 10);
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
        if (_entityEvents != null) _entityEvents.RemoveBuff("SuperRuneOfFrozenGaleSpellHaste");
        if (_entityEvents != null) _entityEvents.RemoveBuff("SuperRuneOfFrozenGalePhysicalDamage");

        if (gameObject.GetComponent<EntityEvents>())
        {
            UnsubscribeEntity();
        }

        if (gameObject.GetComponent<AbilityEvents>())
        {
            UnsubscribeAbility();
        }
    }

    public void ActivateAbility(Damage damage, GameObject target)
    {
        GameObject freeze = RuneAssets.i.RuneFreeze;
        freeze.GetComponent<AbilityEvents>().SetSource(gameObject);

        freeze = Instantiate(freeze, target.transform.position, Quaternion.identity);
        StartCoroutine(SetExplosionStatsAbility(freeze));
    }

    public void ActivateBasic(GameObject target, Damage damage)
    {
        GameObject freeze = RuneAssets.i.RuneFreeze;
        freeze.GetComponent<AbilityEvents>().SetSource(gameObject);

        freeze = Instantiate(freeze, target.transform.position, Quaternion.identity);
        StartCoroutine(SetExplosionStatsBasic(freeze));
    }

    private IEnumerator SetExplosionStatsAbility(GameObject projectile)
    {
        yield return new WaitForEndOfFrame();
        projectile.GetComponent<AbilityEvents>().damageParentMultiplier = 100;
    }

    private IEnumerator SetExplosionStatsBasic(GameObject projectile)
    {
        yield return new WaitForEndOfFrame();
        projectile.GetComponent<AbilityEvents>().damageParentMultiplier = 50;
    }

    //Subs and Unsubs
    public void SubscribeAbility()
    {
        _abilityEvents._onDestroy += UnsubscribeAbility;
        _abilityEvents._onDealDamage += ActivateAbility;
    }

    public void SubscribeEntity()
    {
        _entityEvents.OnBasicAttackHit += ActivateBasic;
    }

    public void UnsubscribeAbility()
    {
        _abilityEvents._onDestroy -= UnsubscribeAbility;
        _abilityEvents._onDealDamage -= ActivateAbility;
    }

    public void UnsubscribeEntity()
    {
        _entityEvents.OnBasicAttackHit -= ActivateBasic;
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
