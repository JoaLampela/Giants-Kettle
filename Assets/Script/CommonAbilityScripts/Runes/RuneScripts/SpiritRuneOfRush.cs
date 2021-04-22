using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritRuneOfRush : MonoBehaviour, IRuneScript
{
    private AbilityEvents _abilityEvents;
    private GameObject _entity = null;
    private EntityEvents _entityEvents;
    private WeaponType _weaponType;
    [SerializeField] private int duplicateCountWeapon = 0;
    [SerializeField] private int duplicateCountArmor = 0;

    [SerializeField] private int duplicateCountWeaponRight = 0;
    [SerializeField] private int duplicateCountWeaponLeft = 0;

    private List<GameObject> projectiles;
    private Item containerItem;
    private IRuneScript.Hand _hand;


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
        if(hand == IRuneScript.Hand.right || hand == IRuneScript.Hand.dual)
        {
            duplicateCountWeaponRight += amount;
        }
        if (hand == IRuneScript.Hand.left || hand == IRuneScript.Hand.dual)
        {
            duplicateCountWeaponLeft += amount;
        }

        duplicateCountWeapon += amount;
        if (_entityEvents != null) SetUpPermanentEffects();
        Debug.Log("HAND " + _hand);
        if (_hand == IRuneScript.Hand.right || _hand == IRuneScript.Hand.dual)
        {
            float oldCdReduction;
            if (2 * (duplicateCountWeapon - amount) == 0) oldCdReduction = 0;
            else oldCdReduction = containerItem.baseMaxCooldownAbility1 - (float)containerItem.baseMaxCooldownAbility1 / ((2f / 3f) * (duplicateCountWeapon - amount));
            Debug.Log((float)containerItem.baseMaxCooldownAbility1 + " / " + "(1.26f * " + duplicateCountWeapon);
            float cdReduction = containerItem.baseMaxCooldownAbility1 - (float)containerItem.baseMaxCooldownAbility1/((2f/3f) * duplicateCountWeapon);
            cdReduction -= oldCdReduction;
            Debug.Log("CD reduction right by " + cdReduction);
            containerItem.maxCooldownAbility1 -= cdReduction;
        }
        if (_hand == IRuneScript.Hand.left || _hand == IRuneScript.Hand.dual)
        {
            float oldCdReduction;
            if (2 * (duplicateCountWeapon - amount) == 0) oldCdReduction = 0;
            else oldCdReduction = containerItem.baseMaxCooldownAbility2 - (float)containerItem.baseMaxCooldownAbility2 / ((2f / 3f) * (duplicateCountWeapon - amount));
            float cdReduction = containerItem.baseMaxCooldownAbility2 - (float)containerItem.baseMaxCooldownAbility2 / ((2f / 3f) * (duplicateCountWeapon));
            cdReduction -= oldCdReduction;
            Debug.Log("CD reduction left by " + cdReduction);
            containerItem.maxCooldownAbility2 -= cdReduction;
        }
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
        Debug.Log(duplicateCountWeapon);
        if (_hand == IRuneScript.Hand.right || _hand == IRuneScript.Hand.dual)
        {

            float oldCdReduction;
            Debug.Log(duplicateCountWeapon);
            
            oldCdReduction = containerItem.baseMaxCooldownAbility1 - (float)containerItem.baseMaxCooldownAbility1 / ((2f / 3f) * (duplicateCountWeapon + amount));
            float cdReduction;
            if (2 * duplicateCountWeapon == 0) cdReduction = 0;
            else cdReduction = containerItem.baseMaxCooldownAbility1 - (float)containerItem.baseMaxCooldownAbility1 / ((2f / 3f) * duplicateCountWeapon);
            
            oldCdReduction -= cdReduction;
            Debug.Log("CD increased right by " + oldCdReduction);
            containerItem.maxCooldownAbility1 += oldCdReduction;
        }
        if (_hand == IRuneScript.Hand.left || _hand == IRuneScript.Hand.dual)
        {
            float oldCdReduction;
            
            oldCdReduction = containerItem.baseMaxCooldownAbility2 - (float)containerItem.baseMaxCooldownAbility2 / ((2f / 3f) * (duplicateCountWeapon + amount));
            float cdReduction;
            if (2 * duplicateCountWeapon == 0) cdReduction = 0;
            else cdReduction = containerItem.baseMaxCooldownAbility2 - (float)containerItem.baseMaxCooldownAbility2 / ((2f / 3f) * duplicateCountWeapon);
            oldCdReduction -= cdReduction;
            Debug.Log("CD increased left by " + oldCdReduction);
            containerItem.maxCooldownAbility2 += oldCdReduction;
        }

        
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
        _entityEvents.RemoveBuff("SpiritRuneOfRushArmor");
        _entityEvents.RemoveBuff("SpiritRuneOfRushWeapon");

        if (duplicateCountArmor != 0)
        {
            _entityEvents.NewBuff("SpiritRuneOfRushArmor", EntityStats.BuffType.SpellHaste, duplicateCountArmor * 20);
        }

        if (duplicateCountWeapon != 0)
        {
            _entityEvents.NewBuff("SpiritRuneOfRushWeapon", EntityStats.BuffType.SpellHaste, duplicateCountWeapon * 10);
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
            _abilityEvents.damageMultiplier = _abilityEvents.damageMultiplier / (2 * duplicateCountWeapon);
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
        if (_entityEvents != null) _entityEvents.RemoveBuff("SpiritRuneOfRushArmor");
        if (_entityEvents != null) _entityEvents.RemoveBuff("SpiritRuneOfRushWeapon");

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
        //TODO: OnEnemyDeath heal %enemyHealth
    }

    public void ActivateWeaponEffect(Damage damage, GameObject target)
    {

    }

    //Subs and Unsubs
    public void SubscribeAbility()
    {
        _abilityEvents._onDealDamage += ActivateWeaponEffect;
        _abilityEvents._onDestroy += UnsubscribeAbility;
    }

    public void SubscribeEntity()
    {

    }

    public void UnsubscribeAbility()
    {
        _abilityEvents._onDealDamage -= ActivateWeaponEffect;
        _abilityEvents._onDestroy -= UnsubscribeAbility;
    }

    public void UnsubscribeEntity()
    {

    }

    public void SetContainerItem(Item item, IRuneScript.Hand hand)
    {
        Debug.Log("Setting container item");
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
