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
    
    public void SetUpPermanentEffects()
    {
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
            _entityEvents.NewBuff("SpiritRuneOfOrbsArmor", EntityStats.BuffType.Health, duplicateCountArmor * 100); //TODO: Spell Haste
            projectile.GetComponent<AbilityEvents>().SetSource(gameObject);

            for(int i = 0; i < 2 + duplicateCountArmor; i++)
            {
                projectile = Instantiate(projectile, gameObject.transform.position + new Vector3(3, 0, 0), Quaternion.identity, transform);
                projectile.transform.RotateAround(gameObject.transform.position, Vector3.forward, i * (360f / (2f + (float)duplicateCountArmor)));
                projectiles.Add(projectile);
            }
        }
        
        if(duplicateCountWeapon != 0)
        {
            _entityEvents.NewBuff("SpiritRuneOfOrbsWeapon", EntityStats.BuffType.PhysicalDamage, duplicateCountWeapon * 5); //TODO: Decide on a better stat buff
        }
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
        GameObject projectile = RuneAssets.i.RuneOrbWeaponProjectile;
        projectile.GetComponent<AbilityEvents>().SetSource(gameObject.GetComponent<AbilityEvents>()._abilityCastSource);
        for (int i = 0; i < duplicateCountWeapon; i++)
        {
            projectile = Instantiate(projectile, gameObject.transform.position + new Vector3(2, 0, 0), Quaternion.identity, transform);
            projectile.GetComponent<AbilityHoamToClosestEnemy>().source = gameObject;
            projectile.transform.RotateAround(gameObject.transform.position, Vector3.forward, i * (360f / (float)duplicateCountWeapon));
            projectile.GetComponent<AbilityEvents>().parentProjectile = gameObject;
        }
    }

    private IEnumerator setOrbStats(GameObject projectile)
    {
        yield return new WaitForEndOfFrame();
        projectile.GetComponent<AbilityEvents>().damageMultiplier = gameObject.GetComponent<AbilityEvents>().damageMultiplier;
    }

    //Subs and Unsubs
    public void SubscribeAbility()
    {
        _abilityEvents._onDestroy += UnsubscribeAbility;
    }

    public void SubscribeEntity()
    {
        //_entityEvents.OnCastAbility += DoStuffOnCastAbility;
    }

    public void UnsubscribeAbility()
    {
        _abilityEvents._onDestroy -= UnsubscribeAbility;
    }

    public void UnsubscribeEntity()
    {
        //_entityEvents.OnCastAbility -= DoStuffOnCastAbility;
    }

    public void SetContainerItem(Item item)
    {
        containerItem = item;
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
