using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritPowerRuneOfPatience : MonoBehaviour, IRuneScript
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
    private EntityHealth health;
    private bool redundancyCheck;
    GameEventManager _gameEventManager;

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
        _entityEvents.RemoveBuff("SpiritPowerRuneOfPatienceHaste");
        _entityEvents.RemoveBuff("SpiritPowerRuneOfPatiencePhysicalDamage");

        if (duplicateCountArmor != 0 || duplicateCountWeapon != 0)
        {
            _entityEvents.NewBuff("SpiritPowerRuneOfPatienceHaste", EntityStats.BuffType.SpellHaste, (duplicateCountArmor + duplicateCountWeapon) * 5);
            _entityEvents.NewBuff("SpiritPowerRuneOfPatiencePhysicalDamage", EntityStats.BuffType.PhysicalDamage, (duplicateCountArmor + duplicateCountWeapon) * 5);
        }
    }

    //Subs & Unsub -related Unity functions
    private void Start()
    {
        if (gameObject.GetComponent<EntityEvents>())
        {
            SubscribeEntity();
            health = _entity.GetComponent<EntityHealth>();
            redundancyCheck = true;
        }

        if (gameObject.GetComponent<AbilityEvents>())
        {
            SubscribeAbility();
        }

        SubscribeGameEvents();
        inCombat = _gameEventManager.combatOn;
    }

    private void Awake()
    {
        _entityEvents = gameObject.GetComponent<EntityEvents>();
        _abilityEvents = gameObject.GetComponent<AbilityEvents>();
        _gameEventManager = GameObject.Find("Game Manager").GetComponent<GameEventManager>();
    }

    private void OnDisable()
    {
        if (_entityEvents != null) _entityEvents.RemoveBuff("SpiritPowerRuneOfPatienceHaste");
        if (_entityEvents != null) _entityEvents.RemoveBuff("SpiritPowerRuneOfPatiencePhysicalDamage");
        if (_entityEvents != null) _entityEvents.RemoveBuff("SpiritPowerRuneOfPatienceBonus");

        if (gameObject.GetComponent<EntityEvents>())
        {
            UnsubscribeEntity();
        }

        if (gameObject.GetComponent<AbilityEvents>())
        {
            UnsubscribeAbility();
        }

        UnsubscribeGameEvents();
    }

    int stackCounter = 0;
    bool inCombat;

    private void Update()
    {
        if (gameObject.GetComponent<EntityEvents>())
        {
            if (inCombat)
            {
                if (redundancyCheck)
                {
                    stackCounter++;
                    Activate();
                    redundancyCheck = false;
                }
            }
            else
            {
                stackCounter = 0;
                _entityEvents.RemoveBuff("SpiritPowerRuneOfPatienceBonus");
                redundancyCheck = true;
            }
        }
            
    }

    public void Activate()
    {
        _entityEvents.RemoveBuff("SpiritPowerRuneOfPatienceBonus");
        _entityEvents.NewBuff("SpiritPowerRuneOfPatienceBonus", EntityStats.BuffType.PhysicalDamage, (duplicateCountArmor + duplicateCountWeapon) * stackCounter);
        StartCoroutine(RedundancyDelayTimer());
    }

    public IEnumerator RedundancyDelayTimer()
    {
        yield return new WaitForSeconds(2.0f);
        redundancyCheck = true;
    }

    public void SetActive()
    {
        inCombat = true;
    }

    public void SetInactive()
    {
        inCombat = false;
    }

    //Subs and Unsubs
    public void SubscribeAbility()
    {
        _abilityEvents._onDestroy += UnsubscribeAbility;
    }

    public void SubscribeEntity()
    {

    }

    public void SubscribeGameEvents()
    {
        _gameEventManager.OnCombatStart += SetActive;
        _gameEventManager.OnCombatEnd += SetInactive;
    }

    public void UnsubscribeAbility()
    {
        _abilityEvents._onDestroy -= UnsubscribeAbility;
    }

    public void UnsubscribeEntity()
    {

    }

    public void UnsubscribeGameEvents()
    {
        _gameEventManager.OnCombatStart -= SetActive;
        _gameEventManager.OnCombatEnd -= SetInactive;
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
