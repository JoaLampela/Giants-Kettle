using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    EntityEvents events;


    [Header("base resourses")]
    public int baseHealth;
    public int baseSpirit;
    public int baseEnergy;
    public int baseRage;

    [Header("base resourse regens")]
    public int baseHealthRegen;
    public int baseRageDepletion;
    public int baseSpiritRegen;
    public int baseEnergyRegen;

    [Header("movement speed base stats")]
    public int baseSpeed;
    public int baseSlow;
    public int baseTenacity;

    [Header("base combat stats")]
    public int basePhysicalDamage;
    public int baseSpiritDamage;
    public int baseCriticalStrikeChance = 1;
    public int baseSpellHaste;
    public int baseArmor;
    public int baseAttackSpeed = 1;



    [Header("current resourses")]
    public int currentMaxHealth;
    public int currentMaxSpirit;
    public int currentMaxEnergy;
    public int currentMaxRage;

    [Header("current resourse regens")]
    public int currentHealthRegen;
    public int currentRageDepletion;
    public int currentSpiritRegen;
    public int currentEnergyRegen;
    public int currentMaxRageGain;

    [Header("current movement speed stats")]
    public int currentMaxSpeed;
    public int currentMaxSlow;
    public int currentMaxTenacity;

    [Header("current combat stats")]
    public int currentMaxPhysicalDamage;
    public int currentMaxSpiritDamage;
    public int currentMaxCriticalStrikeChance;
    public int currentMaxSpellHaste;
    public int currentArmor;
    public int currentMaxAttackSpeed;


    //Bonus stats
    private int bonusHealth = 0;
    private int bonusSpirit = 0;
    private int bonusEnergy = 0;
    private int bonusRage = 0;

    private int bonusHealthRegen = 0;
    private int bonusSpiritRegen = 0;
    private int bonusEnergyRegen = 0;
    private int bonusRageDepletion = 0;

    private int bonusSpeed = 0;
    private int bonusSlow = 0;
    private int bonusTenacity = 0;

    private int bonusPhysicalDamage = 0;
    private int bonusSpiritDamage = 0;
    private int bonusCriticalStrikeChance = 0;

    private int bonusSpellHaste = 0;
    private int bonusArmor = 0;
    private int bonusAttackSpeed = 0;




    //Base Stat Multipliers
    private int baseHealthMultiplier = 100;
    private int baseSpiritMultiplier = 100;
    private int baseEnergyMultiplier = 100;
    private int baseRageMultiplier = 100;

    private int baseHealthRegenMultiplier = 100;
    private int baseSpiritRegenMultiplier = 100;
    private int baseEnergyRegenMultiplier = 100;
    private int baseRageDepletionMultiplier = 100;

    private int baseSpeedMultiplier = 100;
    private int baseTenacityMultiplier = 100;

    private int basePhysicalDamageMultiplier = 100;
    private int baseSpiritDamageMultiplier = 100;
    private int baseCriticalStrikeChanceMultiplier = 100;

    private int baseSpellHasteMultiplayer = 100;
    private int baseArmorMultiplier = 100;
    private int baseAttackSpeedMultiplier = 100;






    //Total Stat Multipliers
    private int totalHealthMultiplier = 100;
    private int totalSpiritMultiplier = 100;
    private int totalEnergyMultiplier = 100;
    private int totalRageMultiplier = 100;

    private int totalHealthRegenMultiplier = 100;
    private int totalSpiritRegenMultiplier = 100;
    private int totalEnergyRegenMultiplier = 100;
    private int totalRageDepletionMultiplier = 100;

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
    [HideInInspector] public bool onSpiritRegen = false;
    [HideInInspector] public bool onEnergyRegen = false;
    [HideInInspector] public bool onRageDepletion = false;

    private void Awake()
    {
        events = GetComponent<EntityEvents>();
    }
    private void Start()
    {
        UpdateMaxHealth();
        UpdateMaxSpirit();
        UpdateMaxEnergy();
        UpdateMaxRage();
        UpdateHealthRegen();
        UpdateSpiritRegen();
        UpdateEnergyRegen();
        UpdateRageDepletion();
        UpdateSpeed();
        UpdateTenacity();
        UpdatePhysicalDamage();
        UpdateSpiritDamage();
        UpdateCriticalStrikeChance();
        UpdateAttackSpeed();
        UpdateSpellHaste();
        UpdateArmor();
        events.StartStatsSet();
    }


    public void UpdateMaxHealth()
    {
        currentMaxHealth = (int)((baseHealth * baseHealthMultiplier / 100f + bonusHealth) * totalHealthMultiplier/100f);
    }
    public void UpdateMaxSpirit()
    {
        currentMaxSpirit = (int)((baseSpirit * baseSpiritMultiplier / 100f + bonusSpirit) * totalSpiritMultiplier/100f);
    }
    public void UpdateMaxEnergy()
    {
        currentMaxEnergy = (int)((baseEnergy * baseEnergyMultiplier / 100f + bonusEnergy) * totalEnergyMultiplier / 100f);
    }
    public void UpdateMaxRage()
    {
        currentMaxRage = (int)((baseRage * baseRageMultiplier / 100f + bonusRage) * totalRageMultiplier / 100f);
    }
    public void UpdateHealthRegen()
    {
        currentHealthRegen = (int)((baseHealthRegen * baseHealthRegenMultiplier / 100f + bonusHealthRegen) * totalHealthRegenMultiplier / 100f);
    }
    public void UpdateSpiritRegen()
    {
        currentSpiritRegen = (int)((baseSpiritRegen * baseSpiritRegenMultiplier / 100f + bonusSpiritRegen) * totalSpiritRegenMultiplier / 100f);
    }
    public void UpdateEnergyRegen()
    {
        currentEnergyRegen = (int)((baseEnergyRegen * baseEnergyRegenMultiplier / 100f + bonusEnergyRegen) * totalEnergyRegenMultiplier / 100f);
    }
    public void UpdateRageDepletion()
    {
        currentRageDepletion = (int)((baseRageDepletion * 100f / baseRageDepletionMultiplier - bonusRageDepletion) * 100f / totalRageDepletionMultiplier);
    }
    public void UpdateSpeed()
    {
        currentMaxSpeed = (int)(((baseSpeed-baseSlow) * baseSpeedMultiplier / 100f + bonusSpeed - bonusSlow) * totalSpeedMultiplier / 100f);
    }
    public void UpdateTenacity()
    {
        currentMaxTenacity = (int)((baseTenacity *baseTenacityMultiplier / 100f + bonusTenacity) * totalTenacityMultiplier / 100f);
    }
    public void UpdatePhysicalDamage()
    {
        currentMaxPhysicalDamage = (int)((basePhysicalDamage * basePhysicalDamageMultiplier / 100f + bonusPhysicalDamage) * totalPhysicalDamageMultiplier / 100f);
    }
    public void UpdateSpiritDamage()
    {
        currentMaxSpiritDamage = (int)((baseSpiritDamage * baseSpiritDamageMultiplier / 100f + bonusSpiritDamage) * totalSpiritDamageMultiplier / 100f);
    }
    public void UpdateCriticalStrikeChance()
    {
        currentMaxCriticalStrikeChance = (int)((baseCriticalStrikeChance * baseCriticalStrikeChanceMultiplier / 100f + bonusCriticalStrikeChance) * totalCriticalStrikeChanceMultiplier / 100f);
    }
    public void UpdateAttackSpeed()
    {
        currentMaxAttackSpeed = (int)((baseAttackSpeed * baseAttackSpeedMultiplier / 100f + bonusAttackSpeed) * totalAttackSpeedMultiplier / 100f);
    }
    public void UpdateSpellHaste()
    {
        currentMaxSpellHaste = (int)((baseSpellHaste * baseSpellHasteMultiplayer / 100f + bonusSpellHaste) * totalSpellHasteMultiplayer / 100f);
    }
    public int UpdateArmor()
    {
        return (int)((baseArmor * baseArmorMultiplier / 100f + bonusArmor) * totalArmorMultiplier / 100f);
    }


    public void UpdateBuff(string id, int value)
    {
        int temp;
        switch (id)
        {
            case "bonusHealth":
                temp = currentMaxHealth;
                bonusHealth += value;
                UpdateMaxHealth();
                if(value > 0) events.GainHealth(currentMaxHealth - temp);
                break;
            case "bonusSpirit":
                temp = currentMaxSpirit;
                bonusSpirit += value;
                UpdateMaxSpirit();
                if (value > 0) events.RecoverSpirit(currentMaxSpirit - temp);
                break;
            case "bonusEnergy":
                temp = currentMaxEnergy;
                bonusEnergy += value;
                UpdateMaxEnergy();
                if (value > 0) events.RecoverEnergy(currentMaxEnergy - temp);
                break;
            case "bonusRage":
                bonusRage += value;
                UpdateMaxRage();
                break;
            case "bonusHealthRegen":
                bonusHealthRegen += value;
                UpdateHealthRegen();
                break;
            case "bonusSpiritRegen":
                bonusSpiritRegen += value;
                UpdateSpiritRegen();
                break;
            case "bonusEnergyRegen":
                bonusEnergyRegen += value;
                UpdateEnergyRegen();
                break;
            case "bonusRageDepletion":
                bonusRageDepletion += value;
                UpdateRageDepletion();
                break;
            case "bonusSpeed":
                bonusSpeed += value;
                UpdateSpeed();
                break;
            case "bonusSlow":
                bonusSlow += value;
                UpdateSpeed();
                break;
            case "bonusTenacity":
                bonusTenacity += value;
                UpdateTenacity();
                break;
            case "bonusPhysicalDamage":
                bonusPhysicalDamage += value;
                UpdatePhysicalDamage();
                break;
            case "bonusSpiritDamage":
                bonusSpiritDamage += value;
                UpdateSpiritDamage();
                break;
            case "bonusCriticalStrikeChance":
                bonusCriticalStrikeChance += value;
                UpdateCriticalStrikeChance();
                break;
            case "bonusSpellHaste":
                bonusSpellHaste += value;
                UpdateSpellHaste();
                break;
            case "bonusArmor":
                bonusArmor += value;
                UpdateArmor();
                break;
            case "bonusAttackSpeed":
                bonusAttackSpeed += value;
                UpdateArmor();
                break;
            case "baseHealthMultiplier":
                temp = currentMaxHealth;
                baseHealthMultiplier += value;
                UpdateMaxHealth();
                if (value > 0) events.GainHealth(currentMaxHealth - temp);
                break;
            case "baseSpiritMultiplier":
                temp = currentMaxSpirit;
                baseSpiritMultiplier += value;
                UpdateMaxSpirit();
                if (value > 0) events.RecoverSpirit(currentMaxSpirit - temp);
                break;
            case "baseEnergyMultiplier":
                temp = currentMaxEnergy;
                baseEnergyMultiplier += value;
                UpdateMaxEnergy();
                if (value > 0) events.RecoverEnergy(currentMaxEnergy - temp);
                break;
            case "baseRageMultiplier":
                baseRageMultiplier += value;
                UpdateMaxRage();
                break;
            case "baseHealthRegenMultiplier":
                baseHealthRegenMultiplier += value;
                UpdateHealthRegen();
                break;
            case "baseSpiritRegenMultiplier":
                baseSpiritRegenMultiplier += value;
                UpdateSpiritRegen();
                break;
            case "baseEnergyRegenMultiplier":
                baseEnergyRegenMultiplier += value;
                UpdateEnergyRegen();
                break;
            case "baseRageDepletionMultiplier":
                baseRageDepletionMultiplier += value;
                UpdateRageDepletion();
                break;
            case "baseSpeedMultiplier":
                baseSpeedMultiplier += value;
                UpdateSpeed();
                break;
            case "baseTenacityMultiplier":
                baseTenacityMultiplier += value;
                UpdateTenacity();
                break;
            case "basePhysicalDamageMultiplier":
                basePhysicalDamageMultiplier += value;
                UpdatePhysicalDamage();
                break;
            case "baseSpiritDamageMultiplier":
                baseSpiritDamageMultiplier += value;
                UpdateSpiritRegen();
                break;
            case "baseCriticalStrikeChanceMultiplier":
                baseCriticalStrikeChanceMultiplier += value;
                UpdateCriticalStrikeChance();
                break;
            case "baseSpellHasteMultiplayer":
                baseSpellHasteMultiplayer += value;
                UpdateSpellHaste();
                break;
            case "baseArmorMultiplier":
                baseArmorMultiplier += value;
                UpdateArmor();
                break;
            case "baseAttackSpeedMultiplier":
                baseAttackSpeedMultiplier += value;
                UpdateAttackSpeed();
                break;
            case "totalHealthMultiplier":
                temp = currentMaxHealth;
                totalHealthMultiplier += value;
                UpdateMaxHealth();
                if (value > 0) events.GainHealth(currentMaxHealth - temp);
                break;
            case "totalSpiritMultiplier":
                temp = currentMaxSpirit;
                totalSpiritMultiplier += value;
                UpdateMaxSpirit();
                if (value > 0) events.RecoverSpirit(currentMaxSpirit - temp);
                break;
            case "totalEnergyMultiplier":
                temp = currentMaxEnergy;
                totalEnergyMultiplier += value;
                UpdateMaxEnergy();
                if (value > 0) events.RecoverEnergy(currentMaxEnergy - temp);
                break;
            case "totalRageMultiplier":
                totalRageMultiplier += value;
                UpdateMaxRage();
                break;
            case "totalHealthRegenMultiplier":
                totalHealthRegenMultiplier += value;
                UpdateHealthRegen();
                break;
            case "totalSpiritRegenMultiplier":
                totalSpiritRegenMultiplier += value;
                UpdateSpiritRegen();
                break;
            case "totalEnergyRegenMultiplier":
                totalEnergyRegenMultiplier += value;
                UpdateEnergyRegen();
                break;
            case "totalRageDepletionMultiplier":
                totalRageDepletionMultiplier += value;
                UpdateRageDepletion();
                break;
            case "totalSpeedMultiplier":
                totalSpeedMultiplier += value;
                UpdateSpeed();
                break;
            case "totalTenacityMultiplier":
                totalTenacityMultiplier += value;
                UpdateTenacity();
                break;
            case "totalPhysicalDamageMultiplier":
                totalPhysicalDamageMultiplier += value;
                UpdatePhysicalDamage();
                break;
            case "totalSpiritDamageMultiplier":
                totalSpiritDamageMultiplier += value;
                UpdateSpiritDamage();
                break;
            case "totalCriticalStrikeChanceMultiplier":
                totalCriticalStrikeChanceMultiplier += value;
                UpdateCriticalStrikeChance();
                break;   
            case "totalSpellHasteMultiplayer":
                totalSpellHasteMultiplayer += value;
                UpdateSpellHaste();
                break;
            case "totalArmorMultiplier":
                totalArmorMultiplier += value;
                UpdateArmor();
                break;
            case "totalAttackSpeedMultiplier":
                totalAttackSpeedMultiplier += value;
                UpdateAttackSpeed();
                break;
            case "baseHealth":
                temp = currentMaxHealth;
                baseHealth += value;
                UpdateMaxHealth();
                if (value > 0) events.GainHealth(currentMaxHealth - temp);
                break;
            case "baseSpirit":
                temp = currentMaxSpirit;
                baseSpirit += value;
                UpdateMaxSpirit();
                if (value > 0) events.RecoverSpirit(currentMaxSpirit - temp);
                break;
            case "baseEnergy":
                temp = currentMaxEnergy;
                baseEnergy += value;
                UpdateMaxEnergy();
                if (value > 0) events.RecoverEnergy(currentMaxEnergy - temp);
                break;
            case "baseRage":
                baseRage += value;
                UpdateMaxRage();
                break;
            case "baseHealthRegen":
                baseHealthRegen += value;
                UpdateHealthRegen();
                break;
            case "baseRageDepletion":
                baseRageDepletion -= value;
                UpdateRageDepletion();
                break;
            case "baseSpiritRegen":
                baseSpiritRegen += value;
                UpdateSpiritRegen();
                break;
            case "baseEnergyRegen":
                baseEnergyRegen += value;
                UpdateEnergyRegen();
                break;
            case "baseSpeed":
                baseSpeed += value;
                UpdateSpeed();
                break;
            case "baseSlow":
                baseSlow += value;
                UpdateSpeed();
                break;
            case "baseTenacity":
                baseTenacity += value;
                UpdateTenacity();
                break;
            case "basePhysicalDamage":
                basePhysicalDamage += value;
                UpdatePhysicalDamage();
                break;
            case "baseSpiritDamage":
                baseSpiritDamage += value;
                UpdateSpiritDamage();
                break;
            case "baseCriticalStrikeChance":
                baseCriticalStrikeChance += value;
                UpdateCriticalStrikeChance();
                break;
            case "baseSpellHaste":
                baseSpellHaste += value;
                UpdateSpellHaste();
                break;
            case "baseArmor":
                baseArmor += value;
                UpdateArmor();
                break;
            case "baseAttackSpeed":
                baseAttackSpeed += value;
                UpdateAttackSpeed();
                break;
        }
    }


}
