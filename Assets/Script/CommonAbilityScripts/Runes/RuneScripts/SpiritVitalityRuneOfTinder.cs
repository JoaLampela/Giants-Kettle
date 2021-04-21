using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritVitalityRuneOfTinder : MonoBehaviour, IRuneScript
{
    private AbilityEvents _abilityEvents;
    private GameObject _entity = null;
    private EntityEvents _entityEvents;
    private WeaponType _weaponType;
    [SerializeField] private int duplicateCountWeapon = 0;
    [SerializeField] private int duplicateCountArmor = 0;
    private List<GameObject> projectiles;

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

    public void IncrementDuplicateCountWeapon(int amount)
    {
        duplicateCountWeapon += amount;
        if (_entityEvents != null) SetUpPermanentEffects();
    }

    public void DecrementDuplicateCountWeapon(int amount)
    {
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
        _entityEvents.RemoveBuff("SpiritVitalityRuneOfTinderArmor");
        _entityEvents.RemoveBuff("SpiritVitalityRuneOfTinderWeapon");

        if (duplicateCountArmor != 0)
        {
            _entityEvents.NewBuff("SpiritVitalityRuneOfTinderArmor", EntityStats.BuffType.Health, duplicateCountArmor * 20);
        }

        if (duplicateCountWeapon != 0)
        {
            _entityEvents.NewBuff("SpiritVitalityRuneOfTinderWeapon", EntityStats.BuffType.PhysicalDamage, duplicateCountWeapon * 5);
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
        projectiles = new List<GameObject>();
    }

    private void OnDisable()
    {
        if (_entityEvents != null) _entityEvents.RemoveBuff("SpiritVitalityRuneOfTinderArmor");
        if (_entityEvents != null) _entityEvents.RemoveBuff("SpiritVitalityRuneOfTinderWeapon");

        foreach (GameObject projectile in projectiles)
        {
            Destroy(projectile);
        }
        projectiles.Clear();

        if (gameObject.GetComponent<EntityEvents>())
        {
            UnsubscribeEntity();
        }

        if (gameObject.GetComponent<AbilityEvents>())
        {
            UnsubscribeAbility();
        }
    }

    public void ActivateArmorEffect(Damage damage)
    {
        damage.source.GetComponent<EntityEvents>().NewBuff("Burning", EntityStats.BuffType.Burning, 1, 5);
    }

    public void ActivateWeaponEffect(Damage damage, GameObject target)
    {
        target.GetComponent<EntityEvents>().NewBuff("Burning", EntityStats.BuffType.Burning, 1, 5);
    }

    //Subs and Unsubs
    public void SubscribeAbility()
    {
        _abilityEvents._onDealDamage += ActivateWeaponEffect;
        _abilityEvents._onDestroy += UnsubscribeAbility;
    }

    public void SubscribeEntity()
    {
        _entityEvents.OnHitThis += ActivateArmorEffect;
    }

    public void UnsubscribeAbility()
    {
        _abilityEvents._onDealDamage -= ActivateWeaponEffect;
        _abilityEvents._onDestroy -= UnsubscribeAbility;
    }

    public void UnsubscribeEntity()
    {
        _entityEvents.OnHitThis -= ActivateArmorEffect;
    }
}