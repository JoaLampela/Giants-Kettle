using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalityRuneOfStagger : MonoBehaviour, IRuneScript
{
    private AbilityEvents _abilityEvents;
    private GameObject _entity = null;
    private EntityEvents _entityEvents;
    private WeaponType _weaponType;
    [SerializeField] private int duplicateCountWeapon = 0;
    [SerializeField] private int duplicateCountArmor = 0;
    private List<GameObject> projectiles;
    private Item containerItem;
    private IRuneScript.Hand _hand;

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
        _entityEvents.RemoveBuff("VitalityRuneOfStaggerArmor");
        _entityEvents.RemoveBuff("VitalityRuneOfStaggerHealth");

        if (duplicateCountArmor != 0 || duplicateCountWeapon != 0)
        {
            _entityEvents.NewBuff("VitalityRuneOfStaggerArmor", EntityStats.BuffType.Armor, (duplicateCountArmor + duplicateCountWeapon) * 5);
            _entityEvents.NewBuff("VitalityRuneOfStaggerHealth", EntityStats.BuffType.Health, (duplicateCountArmor + duplicateCountWeapon) * 5);
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
        if (_entityEvents != null) _entityEvents.RemoveBuff("VitalityRuneOfStaggerArmor");
        if (_entityEvents != null) _entityEvents.RemoveBuff("VitalityRuneOfStaggerHealth");

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
        damage.source.GetComponent<EntityEvents>().NewBuff("VitalityRuneOfStaggerArmorStun", EntityStats.BuffType.Stunned, 1, 0.5f);
        damage.source.GetComponent<EntityEvents>().NewBuff("VitalityRuneOfStaggerArmorStunMovementImpair", EntityStats.BuffType.Slow, 999999, 0.5f);

        GameObject stunEffect = RuneAssets.i.RuneStun;
        stunEffect = Instantiate(stunEffect, damage.source.transform.position, Quaternion.identity, damage.source.transform);
        Destroy(stunEffect, 0.5f);
    }

    public void ActivateWeaponEffect(Damage damage, GameObject target)
    {
        if(UnityEngine.Random.Range(0, 100) <= 100 - Mathf.Pow(0.95f, duplicateCountWeapon) * 100)
        {
            target.GetComponent<EntityEvents>().NewBuff("VitalityRuneOfStaggerWeaponStun", EntityStats.BuffType.Stunned, 1, 1.0f);
            target.GetComponent<EntityEvents>().NewBuff("VitalityRuneOfStaggerWeaponStunMovementImpair", EntityStats.BuffType.Slow, 999999, 1.0f);

            GameObject stunEffect = RuneAssets.i.RuneStun;
            stunEffect = Instantiate(stunEffect, target.transform.position, Quaternion.identity, target.transform);
            Destroy(stunEffect, 1.0f);
        }
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
