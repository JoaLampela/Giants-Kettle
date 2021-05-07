using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalGameStats : MonoBehaviour
{
    public int vitalityRuneOfTenacity = 0;
    public int vitalityRuneOfStagger = 0;
    public int vitalityRuneOfRecovery = 0;
    public int vitalityRuneOfHealth = 0;
    public int vitalityRuneOfGrowth = 0;
    public int vitalityRuneOfArmor = 0;

    public int spiritRuneOfHaste = 0;
    public int spiritRuneOfHealing = 0;
    public int spiritRuneOfOrbs = 0;
    public int spiritRuneOfRush = 0;
    public int spiritRuneOfShield = 0;
    public int spiritRuneOfThorns = 0;
    public int spiritRuneOfThunder = 0;

    public int powerRuneOfBlade = 0;
    public int powerRuneOfDevourer = 0;
    public int powerRuneOfExecution = 0;
    public int powerRuneOfLeech = 0;
    public int powerRuneOfPenetration = 0;
    public int powerRuneOfStrength = 0;

    public int agilityRuneOfAnalyst = 0;
    public int agilityRuneOfCrit = 0;
    public int agilityRuneOfHawkEye = 0;
    public int agilityRuneOfHunting = 0;
    public int agilityRuneOfPredator = 0;
    public int agilityRuneOfSpeed = 0;
    public int agilityRuneOfSwiftness = 0;

    public int agilityPowerRuneOfBeauty = 0;
    public int agilityPowerRuneOfFlow = 0;

    public int spiritAgilityRuneOfKinesis = 0;
    public int spiritAgilityRuneOfThunderDash = 0;

    public int spiritPowerRuneOfExplosion = 0;
    public int spiritPowerRuneOfPatience = 0;

    public int spiritVitalityRuneOfExpenditure = 0;
    public int spiritVitalityRuneOfTinder = 0;

    public int vitalityAgilityRuneOfEscapism = 0;
    public int vitalityAgilityRuneOfRage = 0;

    public int vitalityPowerRuneOfBash = 0;
    public int vitalityPowerRuneOfStress = 0;

    public int killedEnemiesTotal = 0;
    public int killedGoblins = 0;
    public int killedFlyers = 0;
    public int killedSkeletons = 0;
    public int killedSummoners = 0;
    public int killedHoglins = 0;
    public int clearedFloors = 1;
    public int clearedRooms = 0;

    public int totalGameTime = 0;
    public int runesPicked = 0;
    public int totalItemsFound = 0;
    public int totalDamageDealt = 0;
    public int highestDamageInGame = 0;
    public int highestDamageAttack = 0;
    public int totalDeathCount = 0;
    public int totalHits = 0;
    public int totalBossesKilled = 0;
    public int killedByGoblin = 0;
    public int killedBySkeleton = 0;
    public int killedBySummoner = 0;
    public int killedByHoglon = 0;
    public int killedByFlyer = 0;



    [SerializeField] private RuneObject vitalityRuneOfTenacityRune;
    [SerializeField] private RuneObject vitalityRuneOfStaggerRune;
    [SerializeField] private RuneObject vitalityRuneOfRecoveryRune;
    [SerializeField] private RuneObject vitalityRuneOfHealthRune;
    [SerializeField] private RuneObject vitalityRuneOfGrowthRune;
    [SerializeField] private RuneObject vitalityRuneOfArmorRune;

    [SerializeField] private RuneObject spiritRuneOfHasteRune;
    [SerializeField] private RuneObject spiritRuneOfHealingRune;
    [SerializeField] private RuneObject spiritRuneOfOrbsRune;
    [SerializeField] private RuneObject spiritRuneOfRushRune;
    [SerializeField] private RuneObject spiritRuneOfShieldRune;
    [SerializeField] private RuneObject spiritRuneOfThornsRune;
    [SerializeField] private RuneObject spiritRuneOfThunderRune;

    [SerializeField] private RuneObject powerRuneOfBladeRune;
    [SerializeField] private RuneObject powerRuneOfDevourerRune;
    [SerializeField] private RuneObject powerRuneOfExecutionRune;
    [SerializeField] private RuneObject powerRuneOfLeechRune;
    [SerializeField] private RuneObject powerRuneOfPenetrationRune;
    [SerializeField] private RuneObject powerRuneOfStrengthRune;

    [SerializeField] private RuneObject agilityRuneOfAnalystRune;
    [SerializeField] private RuneObject agilityRuneOfCritRune;
    [SerializeField] private RuneObject agilityRuneOfHawkEyeRune;
    [SerializeField] private RuneObject agilityRuneOfHuntingRune;
    [SerializeField] private RuneObject agilityRuneOfPredatorRune;
    [SerializeField] private RuneObject agilityRuneOfSpeedRune;
    [SerializeField] private RuneObject agilityRuneOfSwiftnessRune;

    [SerializeField] private RuneObject agilityPowerRuneOfBeautyRune;
    [SerializeField] private RuneObject agilityPowerRuneOfFlowRune;

    [SerializeField] private RuneObject spiritAgilityRuneOfKinesisRune;
    [SerializeField] private RuneObject spiritAgilityRuneOfThunderDashRune;

    [SerializeField] private RuneObject spiritPowerRuneOfExplosionRune;
    [SerializeField] private RuneObject spiritPowerRuneOfPatienceRune;

    [SerializeField] private RuneObject spiritVitalityRuneOfExpenditureRune;
    [SerializeField] private RuneObject spiritVitalityRuneOfTinderRune;

    [SerializeField] private RuneObject vitalityAgilityRuneOfEscapismRune;
    [SerializeField] private RuneObject vitalityAgilityRuneOfRageRune;

    [SerializeField] private RuneObject vitalityPowerRuneOfBashRune;
    [SerializeField] private RuneObject vitalityPowerRuneOfStressRune;



    public int GetRuneScore(RuneObject rune)
    {
        if (rune == vitalityRuneOfTenacityRune) return vitalityRuneOfTenacity;
        if (rune == vitalityRuneOfStaggerRune) return vitalityRuneOfStagger;
        if (rune == vitalityRuneOfRecoveryRune) return vitalityRuneOfRecovery;
        if (rune == vitalityRuneOfHealthRune) return vitalityRuneOfHealth;
        if (rune == vitalityRuneOfGrowthRune) return vitalityRuneOfGrowth;
        if (rune == vitalityRuneOfArmorRune) return vitalityRuneOfArmor;

        if (rune == spiritRuneOfHasteRune) return spiritRuneOfHaste;
        if (rune == spiritRuneOfHealingRune) return spiritRuneOfHealing;
        if (rune == spiritRuneOfOrbsRune) return spiritRuneOfOrbs;
        if (rune == spiritRuneOfRushRune) return spiritRuneOfRush;
        if (rune == spiritRuneOfShieldRune) return spiritRuneOfShield;
        if (rune == spiritRuneOfThornsRune) return spiritRuneOfThorns;
        if (rune == spiritRuneOfThunderRune) return spiritRuneOfThunder;

        if (rune == powerRuneOfBladeRune) return powerRuneOfBlade;
        if (rune == powerRuneOfDevourerRune) return powerRuneOfDevourer;
        if (rune == powerRuneOfExecutionRune) return powerRuneOfExecution;
        if (rune == powerRuneOfLeechRune) return powerRuneOfLeech;
        if (rune == powerRuneOfPenetrationRune) return powerRuneOfPenetration;
        if (rune == powerRuneOfStrengthRune) return powerRuneOfStrength;

        if (rune == agilityRuneOfAnalystRune) return agilityRuneOfAnalyst;
        if (rune == agilityRuneOfCritRune) return agilityRuneOfCrit;
        if (rune == agilityRuneOfHawkEyeRune) return agilityRuneOfHawkEye;
        if (rune == agilityRuneOfHuntingRune) return agilityRuneOfHunting;
        if (rune == agilityRuneOfPredatorRune) return agilityRuneOfPredator;
        if (rune == agilityRuneOfSpeedRune) return agilityRuneOfSpeed;
        if (rune == agilityRuneOfSwiftnessRune) return agilityRuneOfSwiftness;

        if (rune == agilityPowerRuneOfBeautyRune) return agilityPowerRuneOfBeauty;
        if (rune == agilityPowerRuneOfFlowRune) return agilityPowerRuneOfFlow;

        if (rune == spiritAgilityRuneOfKinesisRune) return spiritAgilityRuneOfKinesis;
        if (rune == spiritAgilityRuneOfThunderDashRune) return spiritAgilityRuneOfThunderDash;

        if (rune == spiritPowerRuneOfExplosionRune) return spiritPowerRuneOfExplosion;
        if (rune == spiritPowerRuneOfPatienceRune) return spiritPowerRuneOfPatience;

        if (rune == spiritVitalityRuneOfExpenditureRune) return spiritVitalityRuneOfExpenditure;
        if (rune == spiritVitalityRuneOfTinderRune) return spiritVitalityRuneOfTinder;

        if (rune == vitalityAgilityRuneOfEscapismRune) return vitalityAgilityRuneOfEscapism;
        if (rune == vitalityAgilityRuneOfRageRune) return vitalityAgilityRuneOfRage;

        if (rune == vitalityPowerRuneOfBashRune) return vitalityPowerRuneOfBash;
        if (rune == vitalityPowerRuneOfStressRune) return vitalityPowerRuneOfStress;

        return 0;
    }

    private void Start()
    {
        vitalityRuneOfTenacity = PlayerPrefs.GetInt("Tenacity", 0);
        vitalityRuneOfStagger = PlayerPrefs.GetInt("Stagger", 0);
        vitalityRuneOfRecovery = PlayerPrefs.GetInt("Recovery", 0);
        vitalityRuneOfHealth = PlayerPrefs.GetInt("Health", 0);
        vitalityRuneOfGrowth = PlayerPrefs.GetInt("Growth", 0);
        vitalityRuneOfArmor = PlayerPrefs.GetInt("Armor", 0);

        spiritRuneOfHaste = PlayerPrefs.GetInt("Haste", 0);
        spiritRuneOfHealing = PlayerPrefs.GetInt("Healing", 0);
        spiritRuneOfOrbs = PlayerPrefs.GetInt("Orbs", 0);
        spiritRuneOfRush = PlayerPrefs.GetInt("Rush", 0);
        spiritRuneOfShield = PlayerPrefs.GetInt("Shield", 0);
        spiritRuneOfThorns = PlayerPrefs.GetInt("Thorns", 0);
        spiritRuneOfThunder = PlayerPrefs.GetInt("Thunder", 0);

        powerRuneOfBlade = PlayerPrefs.GetInt("Blade", 0);
        powerRuneOfDevourer = PlayerPrefs.GetInt("Devourer", 0);
        powerRuneOfExecution = PlayerPrefs.GetInt("Execution", 0);
        powerRuneOfLeech = PlayerPrefs.GetInt("Leech", 0);
        powerRuneOfPenetration = PlayerPrefs.GetInt("Penetration", 0);
        powerRuneOfStrength = PlayerPrefs.GetInt("Strength", 0);

        agilityRuneOfAnalyst = PlayerPrefs.GetInt("Analyst", 0);
        agilityRuneOfCrit = PlayerPrefs.GetInt("Crit", 0);
        agilityRuneOfHawkEye = PlayerPrefs.GetInt("HawkEye", 0);
        agilityRuneOfHunting = PlayerPrefs.GetInt("Hunting", 0);
        agilityRuneOfPredator = PlayerPrefs.GetInt("Predator", 0);
        agilityRuneOfSpeed = PlayerPrefs.GetInt("Speed", 0);
        agilityRuneOfSwiftness = PlayerPrefs.GetInt("Swiftness", 0);

        agilityPowerRuneOfBeauty = PlayerPrefs.GetInt("Beauty", 0);
        agilityPowerRuneOfFlow = PlayerPrefs.GetInt("Flow", 0);

        spiritAgilityRuneOfKinesis = PlayerPrefs.GetInt("Kinesis", 0);
        spiritAgilityRuneOfThunderDash = PlayerPrefs.GetInt("ThunderDash", 0);

        spiritPowerRuneOfExplosion = PlayerPrefs.GetInt("Explosion", 0);
        spiritPowerRuneOfPatience = PlayerPrefs.GetInt("Patience", 0);

        spiritVitalityRuneOfExpenditure = PlayerPrefs.GetInt("Expenditure", 0);
        spiritVitalityRuneOfTinder = PlayerPrefs.GetInt("Tinder", 0);

        vitalityAgilityRuneOfEscapism = PlayerPrefs.GetInt("Escapism", 0);
        vitalityAgilityRuneOfRage = PlayerPrefs.GetInt("Rage", 0);

        vitalityPowerRuneOfBash = PlayerPrefs.GetInt("Bash", 0);
        vitalityPowerRuneOfStress = PlayerPrefs.GetInt("Stress", 0);

        killedEnemiesTotal = PlayerPrefs.GetInt("TotalEnemiesKilled", 0);
        killedGoblins = PlayerPrefs.GetInt("TotalGoblinsKilled", 0);
        killedFlyers = PlayerPrefs.GetInt("TotalKilledFlyers", 0);
        killedSkeletons = PlayerPrefs.GetInt("TotalSkeletonsKilled", 0);
        killedSummoners = PlayerPrefs.GetInt("TotalSummonersKilled", 0);
        killedHoglins = PlayerPrefs.GetInt("TotalKilledHoglins", 0);
        clearedFloors = PlayerPrefs.GetInt("TotalClearedFloors", 0);
        clearedRooms = PlayerPrefs.GetInt("TotalClearedRooms", 0);

        totalGameTime = PlayerPrefs.GetInt("TotalGameTime", 0);
        runesPicked = PlayerPrefs.GetInt("TotalRunesPicked", 0);
        totalItemsFound = PlayerPrefs.GetInt("TotalItemsFound", 0);
        totalDamageDealt = PlayerPrefs.GetInt("TotalDamageDealt", 0);
        highestDamageInGame = PlayerPrefs.GetInt("HighestDamageInGame", 0);
        highestDamageAttack = PlayerPrefs.GetInt("HighestDamageAttack", 0);
        totalDeathCount = PlayerPrefs.GetInt("TotalDeathCount", 0);
        totalHits = PlayerPrefs.GetInt("TotalHits", 0);
        totalBossesKilled = PlayerPrefs.GetInt("TotalBossesKilled", 0);
        killedByGoblin = PlayerPrefs.GetInt("TotalKilledByGoblin", 0);
        killedBySkeleton = PlayerPrefs.GetInt("TotalKilledBySkeleton", 0);
        killedBySummoner = PlayerPrefs.GetInt("TotalKilledBySummoner", 0);
        killedByHoglon = PlayerPrefs.GetInt("TotalKilledByHoglon", 0);
        killedByFlyer = PlayerPrefs.GetInt("TotalKilledByFlyer", 0);
    }
}
