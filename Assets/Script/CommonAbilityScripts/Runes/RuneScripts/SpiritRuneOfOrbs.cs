using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpiritRuneOfOrbs : MonoBehaviour, IRuneScript
{
    private GameObject _projectile;
    private AbilityEvents _abilityEvents;
    private GameObject _entity;
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
        Debug.Log("Incrementing weapon runes " + duplicateCountWeapon); 
        duplicateCountWeapon += amount;
        Debug.Log("Incrementing weapon runes " + duplicateCountWeapon);
        if(_entityEvents != null) SetUpPermanentEffects();

    }

    public void DecrementDuplicateCountWeapon(int amount)
    {
        duplicateCountWeapon -= amount;
        SetUpPermanentEffects();
    }

    public void IncrementDuplicateCountArmor(int amount)
    {
        duplicateCountArmor += amount;
        SetUpPermanentEffects();
    }

    public void DecrementDuplicateCountArmor(int amount)
    {
        duplicateCountArmor -= amount;
        SetUpPermanentEffects();
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
        SetUpPermanentEffects();
    }

    public void SetWeaponType(IRuneScript.WeaponType weaponType)
    {
        if (weaponType == IRuneScript.WeaponType.OneHandedSword) _weaponType = WeaponType.OneHandedSword;
        else if (weaponType == IRuneScript.WeaponType.TwoHandedSword) _weaponType = WeaponType.TwoHandedSword;
        else if (weaponType == IRuneScript.WeaponType.Shield) _weaponType = WeaponType.Shield;
        else if (weaponType == IRuneScript.WeaponType.Bow) _weaponType = WeaponType.Bow;
        else if (weaponType == IRuneScript.WeaponType.Staff) _weaponType = WeaponType.Staff;
    }

    
    private void SetUpPermanentEffects()
    {
        Debug.Log("Removing buffs");
        _entityEvents.RemoveBuff("SpiritRuneOfOrbsArmor");
        _entityEvents.RemoveBuff("SpiritRuneOfOrbsWeapon");

        foreach(GameObject projectile in projectiles)
        {
            Destroy(projectile);
        }
        projectiles.Clear();

        if(duplicateCountArmor != 0)
        {
            GameObject projectile = RuneAssets.i.RuneOrbArmorProjectile;
            _entityEvents.NewBuff("SpiritRuneOfOrbsArmor", EntityStats.BuffType.Health, duplicateCountArmor * 100);
            projectile.GetComponent<AbilityEvents>().SetSource(gameObject);
            float degrees = 360f / (2f + (float)duplicateCountArmor);

            for(int i = 0; i < 2 + duplicateCountArmor; i++)
            {
                projectile = Instantiate(projectile, gameObject.transform.position + new Vector3(2, 0, 0), Quaternion.identity, transform);
                projectile.transform.RotateAround(gameObject.transform.position, Vector3.forward, i * degrees);
                projectiles.Add(projectile);
            }
        }
        
        if(duplicateCountWeapon != 0)
        {
            _entityEvents.NewBuff("SpiritRuneOfOrbsWeapon", EntityStats.BuffType.PhysicalDamage, duplicateCountWeapon * 5);
        }
    }

    private void Test()
    {
        Debug.Log("IT WORKS!");
    }

    //Subs & Unsub -related Unity functions
    private void Start()
    {
        if(gameObject.GetComponent<EntityEvents>())
        {
            SubscribeEntity();
        }

        if(gameObject.GetComponent<AbilityEvents>())
        {
            SubscribeAbility();
            Activate();
            Debug.Log("Subscribe ability");
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
        if(_entityEvents != null) _entityEvents.RemoveBuff("SpiritRuneOfOrbsArmor");
        if(_entityEvents != null) _entityEvents.RemoveBuff("SpiritRuneOfOrbsWeapon");

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

    public void Activate()
    {
        Debug.Log("weapon cound " + duplicateCountWeapon);  
        Debug.Log("AbilityActivated");
        GameObject projectile = RuneAssets.i.RuneOrbWeaponProjectile;
        projectile.GetComponent<AbilityEvents>().SetSource(gameObject.GetComponent<AbilityEvents>()._abilityCastSource);
        float degrees = 360f / (float)duplicateCountWeapon;
        Debug.Log("weapon cound " + duplicateCountWeapon);
        for (int i = 0; i < duplicateCountWeapon; i++)
        {
            Debug.Log("Summoning orb");
            projectile = Instantiate(projectile, gameObject.transform.position + new Vector3(2, 0, 0), Quaternion.identity, transform);
            projectile.transform.RotateAround(gameObject.transform.position, Vector3.forward, i * degrees);
        }
    }

    //Subs and Unsubs
    public void SubscribeAbility()
    {
        _abilityEvents._onActivate += Test;
        _abilityEvents._onDestroy += UnsubscribeAbility;
    }

    public void SubscribeEntity()
    {
        _entityEvents.OnCastAbility += Test;
    }

    public void UnsubscribeAbility()
    {
        _abilityEvents._onActivate -= Test;
        _abilityEvents._onDestroy -= UnsubscribeAbility;
    }

    public void UnsubscribeEntity()
    {
        _entityEvents.OnCastAbility -= Test;
    }
}
