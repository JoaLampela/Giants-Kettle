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
}
