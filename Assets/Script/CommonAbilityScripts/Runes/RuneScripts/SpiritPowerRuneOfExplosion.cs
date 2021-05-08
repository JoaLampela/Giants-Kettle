using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritPowerRuneOfExplosion : MonoBehaviour, IRuneScript
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
        _entityEvents.RemoveBuff("SpiritPowerRuneOfExplosionArmor");
        _entityEvents.RemoveBuff("SpiritPowerRuneOfExplosionWeapon");

        if (duplicateCountArmor != 0)
        {
            _entityEvents.NewBuff("SpiritPowerRuneOfExplosionArmor", EntityStats.BuffType.SpellHaste, duplicateCountArmor * 10);
        }

        if (duplicateCountWeapon != 0)
        {
            _entityEvents.NewBuff("SpiritPowerRuneOfExplosionWeapon", EntityStats.BuffType.PhysicalDamage, duplicateCountWeapon * 10);
        }
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
        if (_entityEvents != null) _entityEvents.RemoveBuff("SpiritPowerRuneOfExplosionArmor");
        if (_entityEvents != null) _entityEvents.RemoveBuff("SpiritPowerRuneOfExplosionWeapon");

        if (gameObject.GetComponent<EntityEvents>())
        {
            UnsubscribeEntity();
        }

        if (gameObject.GetComponent<AbilityEvents>())
        {
            UnsubscribeAbility();
        }
    }

    public void ActivateWeapon(Damage damage, GameObject target)
    {
        GameObject explosion = RuneAssets.i.RuneExplosion;
        explosion.GetComponent<AbilityEvents>().SetSource(gameObject.GetComponent<AbilityEvents>()._abilityCastSource);

        explosion = Instantiate(explosion, target.transform.position, Quaternion.identity);
        StartCoroutine(SetExplosionStatsWeapon(explosion));
    }

    public void ActivateArmor(GameObject target, Damage damage)
    {
        GameObject explosion = RuneAssets.i.RuneExplosion;
        explosion.GetComponent<AbilityEvents>().SetSource(gameObject);

        explosion = Instantiate(explosion, target.transform.position, Quaternion.identity);
        StartCoroutine(SetExplosionStatsArmor(explosion));
    }

    private IEnumerator SetExplosionStatsWeapon(GameObject projectile)
    {
        yield return new WaitForEndOfFrame();
        projectile.GetComponent<AbilityEvents>().damageParentMultiplier = gameObject.GetComponent<AbilityEvents>().damageMultiplier;
    }

    private IEnumerator SetExplosionStatsArmor(GameObject projectile)
    {
        yield return new WaitForEndOfFrame();
        projectile.GetComponent<AbilityEvents>().damageParentMultiplier = 50;
    }

    //Subs and Unsubs
    public void SubscribeAbility()
    {
        _abilityEvents._onDestroy += UnsubscribeAbility;
        _abilityEvents._onDealDamage += ActivateWeapon;
    }

    public void SubscribeEntity()
    {
        _entityEvents.OnBasicAttackHit += ActivateArmor;
    }

    public void UnsubscribeAbility()
    {
        _abilityEvents._onDestroy -= UnsubscribeAbility;
        _abilityEvents._onDealDamage -= ActivateWeapon;
    }

    public void UnsubscribeEntity()
    {
        _entityEvents.OnBasicAttackHit -= ActivateArmor;
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
