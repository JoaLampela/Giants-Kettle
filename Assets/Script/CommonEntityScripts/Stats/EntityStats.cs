using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    GameEventManager gameEventManager;
    EntityEvents events;

    public int level;

    [Header("0 = neutral, 1 = AI, 2 = Player, 3 = Map")]
    public int team = 0;

    [Header("base resourses")]
    public int baseHealth;
    public int baseSpirit;

    [Header("base resourse regens")]
    public int baseHealthRegen;
    public int baseSpiritRegen;

    [Header("movement speed base stats")]
    public int baseSpeed;
    public int baseSlow;
    public int baseTenacity;

    [Header("base combat stats")]
    public int basePhysicalDamage;
    public int baseCriticalStrikeChance = 1;
    public int baseSpellHaste;
    public int baseArmor;
    public int baseAttackSpeed = 1;

    [Header("current resourses")]
    public float currentShield;
    [HideInInspector] public int currentMaxHealth;
    [HideInInspector] public int currentMaxSpirit;

    [Header("current resourse regens")]
    [HideInInspector] public int currentHealthRegen;
    [HideInInspector] public int currentSpiritRegen;

    [Header("current movement speed stats")]
    [HideInInspector] public int currentSpeed;
    [HideInInspector] public int currentSlow;
    [HideInInspector] public int currentTenacity;

    [Header("current combat stats")]
    [HideInInspector] public int currentPhysicalDamage;
    [HideInInspector] public int currentCriticalStrikeChance;
    [HideInInspector] public int currentSpellHaste;
    [HideInInspector] public int currentArmor;
    [HideInInspector] public int currentAttackSpeed;


    //Total Stat Multipliers
    private int totalHealthMultiplier = 100;
    private int totalSpiritMultiplier = 100;

    private int totalHealthRegenMultiplier = 100;
    private int totalSpiritRegenMultiplier = 100;

    private int totalSpeedMultiplier = 100;
    private int totalTenacityMultiplier = 100;

    private int totalPhysicalDamageMultiplier = 100;
    private int totalSpiritDamageMultiplier = 100;
    private int totalCriticalStrikeChanceMultiplier = 100;

    private int totalSpellHasteMultiplayer = 100;
    private int totalArmorMultiplier = 100;
    private int totalAttackSpeedMultiplier = 100;




    //Status
    [HideInInspector] public bool isInvisible = false;
    [HideInInspector] public bool isInCombat = false;
    [HideInInspector] public bool isInAction = false;
    [HideInInspector] public bool isTakingDamage = false;
    [HideInInspector] public bool isStunned = false;
    [HideInInspector] public bool onHealthRegen = false;
    public bool isOnFire = false;

    private void Awake()
    {
        events = GetComponent<EntityEvents>();
        gameEventManager = GameObject.Find("Game Manager").GetComponent<GameEventManager>();
    }
    private void Start()
    {
        level = gameEventManager.globalLevel;

        UpdateMaxHealth();
        UpdateMaxSpirit();
        UpdateHealthRegen();
        UpdateSpiritRegen();
        UpdateSpeed();
        UpdateTenacity();
        UpdatePhysicalDamage();
        UpdateCriticalStrikeChance();
        UpdateAttackSpeed();
        UpdateSpellHaste();
        UpdateArmor();


        events.SetHealth(currentMaxHealth);
        events.SetSpirit(currentMaxSpirit);
    }


    public void UpdateMaxHealth()
    {
        currentMaxHealth = (int)((baseHealth  * totalHealthMultiplier / 100f));
    }
    public void UpdateMaxSpirit()
    {
        currentMaxSpirit = (int)((baseSpirit * totalSpiritMultiplier / 100f));
    }
    public void UpdateHealthRegen()
    {
        currentHealthRegen = (int)((baseHealthRegen *  totalHealthRegenMultiplier / 100f));
    }
    public void UpdateSpiritRegen()
    {
        currentSpiritRegen = (int)((baseSpiritRegen *  totalSpiritRegenMultiplier / 100f));
    }
    public void UpdateSpeed()
    {
        currentSpeed = (int)(((baseSpeed - baseSlow) *  totalSpeedMultiplier / 100f));
    }
    public void UpdateTenacity()
    {
        currentTenacity = (int)((baseTenacity * totalTenacityMultiplier / 100f));
    }
    public void UpdatePhysicalDamage()
    {
        currentPhysicalDamage = (int)((basePhysicalDamage * totalPhysicalDamageMultiplier / 100f));
    }
    public void UpdateCriticalStrikeChance()
    {
        currentCriticalStrikeChance = (int)((baseCriticalStrikeChance * totalCriticalStrikeChanceMultiplier / 100f));
    }
    public void UpdateAttackSpeed()
    {
        currentAttackSpeed = (int)((baseAttackSpeed * totalAttackSpeedMultiplier / 100f));
    }
    public void UpdateSpellHaste()
    {
        currentSpellHaste = (int)((baseSpellHaste *  totalSpellHasteMultiplayer / 100f));
    }
    public void UpdateArmor()
    {
        currentArmor = (int)((baseArmor *  totalArmorMultiplier / 100f));
    }

    public enum BuffType
    {
        HealthMultiplier,
        HealthRegenMultiplier,
        SpeedMultiplier,
        TenacityMultiplier,
        PhysicalDamageMultiplier,
        CriticalStrikeChanceMultiplier,
        SpellHasteMultiplayer,
        ArmorMultiplier,
        AttackSpeedMultiplier,
        Health,
        HealthRegen,
        Speed,
        Slow,
        Tenacity,
        PhysicalDamage,
        CriticalStrikeChance,
        SpellHaste,
        Armor,
        AttackSpeed,
        Invisibility,
        Burning


    }
    public void UpdateBuff(BuffType buffType, int value)
    {
        int temp;
        switch (buffType)
        {
            case BuffType.HealthMultiplier:
                temp = currentMaxHealth;
                totalHealthMultiplier += value;
                UpdateMaxHealth();
                if (value > 0) events.GainHealth(currentMaxHealth - temp);
                break;
            case BuffType.HealthRegenMultiplier:
                totalHealthRegenMultiplier += value;
                UpdateHealthRegen();
                break;
            case BuffType.SpeedMultiplier:
                totalSpeedMultiplier += value;
                UpdateSpeed();
                break;
            case BuffType.TenacityMultiplier:
                totalTenacityMultiplier += value;
                UpdateTenacity();
                break;
            case BuffType.PhysicalDamageMultiplier:
                totalPhysicalDamageMultiplier += value;
                UpdatePhysicalDamage();
                break;
            case BuffType.CriticalStrikeChanceMultiplier:
                totalCriticalStrikeChanceMultiplier += value;
                UpdateCriticalStrikeChance();
                break;
            case BuffType.SpellHasteMultiplayer:
                totalSpellHasteMultiplayer += value;
                UpdateSpellHaste();
                break;
            case BuffType.ArmorMultiplier:
                totalArmorMultiplier += value;
                UpdateArmor();
                break;
            case BuffType.AttackSpeedMultiplier:
                totalAttackSpeedMultiplier += value;
                UpdateAttackSpeed();
                break;
            case BuffType.Health:
                temp = currentMaxHealth;
                baseHealth += value;
                UpdateMaxHealth();
                if (value > 0) events.GainHealth(currentMaxHealth - temp);
                break;
            case BuffType.HealthRegen:
                baseHealthRegen += value;
                UpdateHealthRegen();
                break;
            case BuffType.Speed:
                baseSpeed += value;
                UpdateSpeed();
                break;
            case BuffType.Slow:
                baseSlow += value;
                UpdateSpeed();
                break;
            case BuffType.Tenacity:
                baseTenacity += value;
                UpdateTenacity();
                break;
            case BuffType.PhysicalDamage:
                basePhysicalDamage += value;
                UpdatePhysicalDamage();
                break;
            case BuffType.CriticalStrikeChance:
                baseCriticalStrikeChance += value;
                UpdateCriticalStrikeChance();
                break;
            case BuffType.SpellHaste:
                baseSpellHaste += value;
                UpdateSpellHaste();
                break;
            case BuffType.Armor:
                baseArmor += value;
                UpdateArmor();
                break;
            case BuffType.AttackSpeed:
                baseAttackSpeed += value;
                UpdateAttackSpeed();
                break;
            case BuffType.Burning:
                if (value == 1)
                {
                    isOnFire = true;
                }
                else
                {
                    isOnFire = false;
                }
                break;

            case BuffType.Invisibility:
                if (value == 1)
                {
                    isInvisible = true;
                    gameEventManager.UpdateAggro();
                }
                else
                {
                    isInvisible = false;
                    gameEventManager.UpdateAggro();
                }
                break;
        }
    }                                                                                  
}

