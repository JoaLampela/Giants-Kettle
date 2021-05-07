using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{

    public int killedEnemiesTotal = 0;
    public int killedGoblins = 0;
    public int killedFlyers = 0;
    public int killedSkeletons = 0;
    public int killedSummoners = 0;
    public int killedHoglins = 0;

    public int clearedFloors = 1;
    public int clearedRooms = 0;

    public int foundItems = 0;
    public int runesPicked = 0;

    private int damageDealt = 0;
    private int topDmg = 0;
    private int totalHits = 0;

    private GameObject player;

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

    [SerializeField] private RuneObject vitalityRuneOfTenacityRune1;
    [SerializeField] private RuneObject vitalityRuneOfTenacityRune2;
    [SerializeField] private RuneObject vitalityRuneOfTenacityRune3;

    [SerializeField] private RuneObject vitalityRuneOfStaggerRune1;
    [SerializeField] private RuneObject vitalityRuneOfStaggerRune2;
    [SerializeField] private RuneObject vitalityRuneOfStaggerRune3;

    [SerializeField] private RuneObject vitalityRuneOfRecoveryRune1;
    [SerializeField] private RuneObject vitalityRuneOfRecoveryRune2;
    [SerializeField] private RuneObject vitalityRuneOfRecoveryRune3;

    [SerializeField] private RuneObject vitalityRuneOfHealthRune1;
    [SerializeField] private RuneObject vitalityRuneOfHealthRune2;
    [SerializeField] private RuneObject vitalityRuneOfHealthRune3;

    [SerializeField] private RuneObject vitalityRuneOfGrowthRune1;
    [SerializeField] private RuneObject vitalityRuneOfGrowthRune2;
    [SerializeField] private RuneObject vitalityRuneOfGrowthRune3;

    [SerializeField] private RuneObject vitalityRuneOfArmorRune1;
    [SerializeField] private RuneObject vitalityRuneOfArmorRune2;
    [SerializeField] private RuneObject vitalityRuneOfArmorRune3;


    [SerializeField] private RuneObject spiritRuneOfHasteRune1;
    [SerializeField] private RuneObject spiritRuneOfHasteRune2;
    [SerializeField] private RuneObject spiritRuneOfHasteRune3;

    [SerializeField] private RuneObject spiritRuneOfHealingRune1;
    [SerializeField] private RuneObject spiritRuneOfHealingRune2;
    [SerializeField] private RuneObject spiritRuneOfHealingRune3;

    [SerializeField] private RuneObject spiritRuneOfOrbsRune1;
    [SerializeField] private RuneObject spiritRuneOfOrbsRune2;
    [SerializeField] private RuneObject spiritRuneOfOrbsRune3;

    [SerializeField] private RuneObject spiritRuneOfRushRune1;
    [SerializeField] private RuneObject spiritRuneOfRushRune2;
    [SerializeField] private RuneObject spiritRuneOfRushRune3;

    [SerializeField] private RuneObject spiritRuneOfShieldRune1;
    [SerializeField] private RuneObject spiritRuneOfShieldRune2;
    [SerializeField] private RuneObject spiritRuneOfShieldRune3;

    [SerializeField] private RuneObject spiritRuneOfThornsRune1;
    [SerializeField] private RuneObject spiritRuneOfThornsRune2;
    [SerializeField] private RuneObject spiritRuneOfThornsRune3;

    [SerializeField] private RuneObject spiritRuneOfThunderRune1;
    [SerializeField] private RuneObject spiritRuneOfThunderRune2;
    [SerializeField] private RuneObject spiritRuneOfThunderRune3;

    [SerializeField] private RuneObject powerRuneOfBladeRune1;
    [SerializeField] private RuneObject powerRuneOfBladeRune2;
    [SerializeField] private RuneObject powerRuneOfBladeRune3;

    [SerializeField] private RuneObject powerRuneOfDevourerRune1;
    [SerializeField] private RuneObject powerRuneOfDevourerRune2;
    [SerializeField] private RuneObject powerRuneOfDevourerRune3;

    [SerializeField] private RuneObject powerRuneOfExecutionRune1;
    [SerializeField] private RuneObject powerRuneOfExecutionRune2;
    [SerializeField] private RuneObject powerRuneOfExecutionRune3;

    [SerializeField] private RuneObject powerRuneOfLeechRune1;
    [SerializeField] private RuneObject powerRuneOfLeechRune2;
    [SerializeField] private RuneObject powerRuneOfLeechRune3;

    [SerializeField] private RuneObject powerRuneOfPenetrationRune1;
    [SerializeField] private RuneObject powerRuneOfPenetrationRune2;
    [SerializeField] private RuneObject powerRuneOfPenetrationRune3;

    [SerializeField] private RuneObject powerRuneOfStrengthRune1;
    [SerializeField] private RuneObject powerRuneOfStrengthRune2;
    [SerializeField] private RuneObject powerRuneOfStrengthRune3;

    [SerializeField] private RuneObject agilityRuneOfAnalystRune1;
    [SerializeField] private RuneObject agilityRuneOfAnalystRune2;
    [SerializeField] private RuneObject agilityRuneOfAnalystRune3;

    [SerializeField] private RuneObject agilityRuneOfCritRune1;
    [SerializeField] private RuneObject agilityRuneOfCritRune2;
    [SerializeField] private RuneObject agilityRuneOfCritRune3;

    [SerializeField] private RuneObject agilityRuneOfHawkEyeRune1;
    [SerializeField] private RuneObject agilityRuneOfHawkEyeRune2;
    [SerializeField] private RuneObject agilityRuneOfHawkEyeRune3;

    [SerializeField] private RuneObject agilityRuneOfHuntingRune1;
    [SerializeField] private RuneObject agilityRuneOfHuntingRune2;
    [SerializeField] private RuneObject agilityRuneOfHuntingRune3;

    [SerializeField] private RuneObject agilityRuneOfPredatorRune1;
    [SerializeField] private RuneObject agilityRuneOfPredatorRune2;
    [SerializeField] private RuneObject agilityRuneOfPredatorRune3;

    [SerializeField] private RuneObject agilityRuneOfSpeedRune1;
    [SerializeField] private RuneObject agilityRuneOfSpeedRune2;
    [SerializeField] private RuneObject agilityRuneOfSpeedRune3;

    [SerializeField] private RuneObject agilityRuneOfSwiftnessRune1;
    [SerializeField] private RuneObject agilityRuneOfSwiftnessRune2;
    [SerializeField] private RuneObject agilityRuneOfSwiftnessRune3;

    [SerializeField] private RuneObject agilityPowerRuneOfBeautyRune1;
    [SerializeField] private RuneObject agilityPowerRuneOfBeautyRune2;
    [SerializeField] private RuneObject agilityPowerRuneOfBeautyRune3;

    [SerializeField] private RuneObject agilityPowerRuneOfFlowRune1;
    [SerializeField] private RuneObject agilityPowerRuneOfFlowRune2;
    [SerializeField] private RuneObject agilityPowerRuneOfFlowRune3;

    [SerializeField] private RuneObject spiritAgilityRuneOfKinesisRune1;
    [SerializeField] private RuneObject spiritAgilityRuneOfKinesisRune2;
    [SerializeField] private RuneObject spiritAgilityRuneOfKinesisRune3;

    [SerializeField] private RuneObject spiritAgilityRuneOfThunderDashRune1;
    [SerializeField] private RuneObject spiritAgilityRuneOfThunderDashRune2;
    [SerializeField] private RuneObject spiritAgilityRuneOfThunderDashRune3;

    [SerializeField] private RuneObject spiritPowerRuneOfExplosionRune1;
    [SerializeField] private RuneObject spiritPowerRuneOfExplosionRune2;
    [SerializeField] private RuneObject spiritPowerRuneOfExplosionRune3;

    [SerializeField] private RuneObject spiritPowerRuneOfPatienceRune1;
    [SerializeField] private RuneObject spiritPowerRuneOfPatienceRune2;
    [SerializeField] private RuneObject spiritPowerRuneOfPatienceRune3;

    [SerializeField] private RuneObject spiritVitalityRuneOfExpenditureRune1;
    [SerializeField] private RuneObject spiritVitalityRuneOfExpenditureRune2;
    [SerializeField] private RuneObject spiritVitalityRuneOfExpenditureRune3;

    [SerializeField] private RuneObject spiritVitalityRuneOfTinderRune1;
    [SerializeField] private RuneObject spiritVitalityRuneOfTinderRune2;
    [SerializeField] private RuneObject spiritVitalityRuneOfTinderRune3;

    [SerializeField] private RuneObject vitalityAgilityRuneOfEscapismRune1;
    [SerializeField] private RuneObject vitalityAgilityRuneOfEscapismRune2;
    [SerializeField] private RuneObject vitalityAgilityRuneOfEscapismRune3;

    [SerializeField] private RuneObject vitalityAgilityRuneOfRageRune1;
    [SerializeField] private RuneObject vitalityAgilityRuneOfRageRune2;
    [SerializeField] private RuneObject vitalityAgilityRuneOfRageRune3;


    [SerializeField] private RuneObject vitalityPowerRuneOfBashRune1;
    [SerializeField] private RuneObject vitalityPowerRuneOfBashRune2;
    [SerializeField] private RuneObject vitalityPowerRuneOfBashRune3;

    [SerializeField] private RuneObject vitalityPowerRuneOfStressRune1;
    [SerializeField] private RuneObject vitalityPowerRuneOfStressRune2;
    [SerializeField] private RuneObject vitalityPowerRuneOfStressRune3;




    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        Subscribe();
    }
    public void AddRune(RuneObject rune)
    {
        runesPicked++;

        if(rune == vitalityRuneOfTenacityRune1) vitalityRuneOfTenacity += 1;
        if (rune == vitalityRuneOfTenacityRune2) vitalityRuneOfTenacity += 2;
        if (rune == vitalityRuneOfTenacityRune3) vitalityRuneOfTenacity += 3;

        if (rune == vitalityRuneOfStaggerRune1) vitalityRuneOfStagger += 1;
        if (rune == vitalityRuneOfStaggerRune2) vitalityRuneOfStagger += 2;
        if (rune == vitalityRuneOfStaggerRune3) vitalityRuneOfStagger += 3;

        if (rune == vitalityRuneOfRecoveryRune1) vitalityRuneOfRecovery += 1;
        if (rune == vitalityRuneOfRecoveryRune2) vitalityRuneOfRecovery += 2;
        if (rune == vitalityRuneOfRecoveryRune3) vitalityRuneOfRecovery += 3;

        if (rune == vitalityRuneOfHealthRune1) vitalityRuneOfHealth += 1;
        if (rune == vitalityRuneOfHealthRune2) vitalityRuneOfHealth += 2;
        if (rune == vitalityRuneOfHealthRune3) vitalityRuneOfHealth += 3;

        if (rune == vitalityRuneOfGrowthRune1) vitalityRuneOfGrowth += 1;
        if (rune == vitalityRuneOfGrowthRune2) vitalityRuneOfGrowth += 2;
        if (rune == vitalityRuneOfGrowthRune3) vitalityRuneOfGrowth += 3;

        if (rune == vitalityRuneOfArmorRune1) vitalityRuneOfArmor += 1;
        if (rune == vitalityRuneOfArmorRune2) vitalityRuneOfArmor += 2;
        if (rune == vitalityRuneOfArmorRune3) vitalityRuneOfArmor += 3;


        if (rune == spiritRuneOfHasteRune1) spiritRuneOfHaste += 1;
        if (rune == spiritRuneOfHasteRune2) spiritRuneOfHaste += 2;
        if (rune == spiritRuneOfHasteRune3) spiritRuneOfHaste += 3;

        if (rune == spiritRuneOfHealingRune1) spiritRuneOfHealing += 1;
        if (rune == spiritRuneOfHealingRune2) spiritRuneOfHealing += 2;
        if (rune == spiritRuneOfHealingRune3) spiritRuneOfHealing += 3;

        if (rune == spiritRuneOfOrbsRune1) spiritRuneOfOrbs += 1;
        if (rune == spiritRuneOfOrbsRune2) spiritRuneOfOrbs += 2;
        if (rune == spiritRuneOfOrbsRune3) spiritRuneOfOrbs += 3;

        if (rune == spiritRuneOfRushRune1) spiritRuneOfRush += 1;
        if (rune == spiritRuneOfRushRune2) spiritRuneOfRush += 2;
        if (rune == spiritRuneOfRushRune3) spiritRuneOfRush += 3;

        if (rune == spiritRuneOfShieldRune1) spiritRuneOfShield += 1;
        if (rune == spiritRuneOfShieldRune2) spiritRuneOfShield += 2;
        if (rune == spiritRuneOfShieldRune3) spiritRuneOfShield += 3;

        if (rune == spiritRuneOfThornsRune1) spiritRuneOfThorns += 1;
        if (rune == spiritRuneOfThornsRune2) spiritRuneOfThorns += 2;
        if (rune == spiritRuneOfThornsRune3) spiritRuneOfThorns += 3;

        if (rune == spiritRuneOfThunderRune1) spiritRuneOfThunder += 1;
        if (rune == spiritRuneOfThunderRune2) spiritRuneOfThunder += 2;
        if (rune == spiritRuneOfThunderRune3) spiritRuneOfThunder += 3;

        if (rune == powerRuneOfBladeRune1) powerRuneOfBlade += 1;
        if (rune == powerRuneOfBladeRune2) powerRuneOfBlade += 2;
        if (rune == powerRuneOfBladeRune3) powerRuneOfBlade += 3;

        if (rune == powerRuneOfDevourerRune1) powerRuneOfDevourer += 1;
        if (rune == powerRuneOfDevourerRune2) powerRuneOfDevourer += 2;
        if (rune == powerRuneOfDevourerRune3) powerRuneOfDevourer += 3;

        if (rune == powerRuneOfExecutionRune1) powerRuneOfExecution += 1;
        if (rune == powerRuneOfExecutionRune2) powerRuneOfExecution += 2;
        if (rune == powerRuneOfExecutionRune3) powerRuneOfExecution += 3;

        if (rune == powerRuneOfLeechRune1) powerRuneOfLeech += 1;
        if (rune == powerRuneOfLeechRune2) powerRuneOfLeech += 2;
        if (rune == powerRuneOfLeechRune3) powerRuneOfLeech += 3;

        if (rune == powerRuneOfPenetrationRune1) powerRuneOfPenetration += 1;
        if (rune == powerRuneOfPenetrationRune2) powerRuneOfPenetration += 2;
        if (rune == powerRuneOfPenetrationRune3) powerRuneOfPenetration += 3;

        if (rune == powerRuneOfStrengthRune1) powerRuneOfStrength += 1;
        if (rune == powerRuneOfStrengthRune2) powerRuneOfStrength += 2;
        if (rune == powerRuneOfStrengthRune3) powerRuneOfStrength += 3;

        if (rune == agilityRuneOfAnalystRune1) agilityRuneOfAnalyst += 1;
        if (rune == agilityRuneOfAnalystRune2) agilityRuneOfAnalyst += 2;
        if (rune == agilityRuneOfAnalystRune3) agilityRuneOfAnalyst += 3;

        if (rune == agilityRuneOfCritRune1) agilityRuneOfCrit += 1;
        if (rune == agilityRuneOfCritRune2) agilityRuneOfCrit += 2;
        if (rune == agilityRuneOfCritRune3) agilityRuneOfCrit += 3;

        if (rune == agilityRuneOfHawkEyeRune1) agilityRuneOfHawkEye += 1;
        if (rune == agilityRuneOfHawkEyeRune2) agilityRuneOfHawkEye += 2;
        if (rune == agilityRuneOfHawkEyeRune3) agilityRuneOfHawkEye += 3;

        if (rune == agilityRuneOfHuntingRune1) agilityRuneOfHunting += 1;
        if (rune == agilityRuneOfHuntingRune2) agilityRuneOfHunting += 2;
        if (rune == agilityRuneOfHuntingRune3) agilityRuneOfHunting += 3;

        if (rune == agilityRuneOfPredatorRune1) agilityRuneOfPredator += 1;
        if (rune == agilityRuneOfPredatorRune2) agilityRuneOfPredator += 2;
        if (rune == agilityRuneOfPredatorRune3) agilityRuneOfPredator += 3;

        if (rune == agilityRuneOfSpeedRune1) agilityRuneOfSpeed += 1;
        if (rune == agilityRuneOfSpeedRune2) agilityRuneOfSpeed += 2;
        if (rune == agilityRuneOfSpeedRune3) agilityRuneOfSpeed += 3;

        if (rune == agilityRuneOfSwiftnessRune1) agilityRuneOfSwiftness += 1;
        if (rune == agilityRuneOfSwiftnessRune2) agilityRuneOfSwiftness += 2;
        if (rune == agilityRuneOfSwiftnessRune3) agilityRuneOfSwiftness += 3;

        if (rune == agilityPowerRuneOfBeautyRune1) agilityPowerRuneOfBeauty += 1;
        if (rune == agilityPowerRuneOfBeautyRune2) agilityPowerRuneOfBeauty += 2;
        if (rune == agilityPowerRuneOfBeautyRune3) agilityPowerRuneOfBeauty += 3;

        if (rune == agilityPowerRuneOfFlowRune1) agilityPowerRuneOfFlow += 1;
        if (rune == agilityPowerRuneOfFlowRune2) agilityPowerRuneOfFlow += 2;
        if (rune == agilityPowerRuneOfFlowRune3) agilityPowerRuneOfFlow += 3;

        if (rune == spiritAgilityRuneOfKinesisRune1) spiritAgilityRuneOfKinesis += 1;
        if (rune == spiritAgilityRuneOfKinesisRune2) spiritAgilityRuneOfKinesis += 2;
        if (rune == spiritAgilityRuneOfKinesisRune3) spiritAgilityRuneOfKinesis += 3;

        if (rune == spiritAgilityRuneOfThunderDashRune1) spiritAgilityRuneOfThunderDash += 1;
        if (rune == spiritAgilityRuneOfThunderDashRune2) spiritAgilityRuneOfThunderDash += 2;
        if (rune == spiritAgilityRuneOfThunderDashRune3) spiritAgilityRuneOfThunderDash += 3;

        if (rune == spiritPowerRuneOfExplosionRune1) spiritPowerRuneOfExplosion += 1;
        if (rune == spiritPowerRuneOfExplosionRune2) spiritPowerRuneOfExplosion += 2;
        if (rune == spiritPowerRuneOfExplosionRune3) spiritPowerRuneOfExplosion += 3;

        if (rune == spiritPowerRuneOfPatienceRune1) spiritPowerRuneOfPatience += 1;
        if (rune == spiritPowerRuneOfPatienceRune2) spiritPowerRuneOfPatience += 2;
        if (rune == spiritPowerRuneOfPatienceRune3) spiritPowerRuneOfPatience += 3;

        if (rune == spiritVitalityRuneOfExpenditureRune1) spiritVitalityRuneOfExpenditure += 1;
        if (rune == spiritVitalityRuneOfExpenditureRune2) spiritVitalityRuneOfExpenditure += 2;
        if (rune == spiritVitalityRuneOfExpenditureRune3) spiritVitalityRuneOfExpenditure += 3;

        if (rune == spiritVitalityRuneOfTinderRune1) spiritVitalityRuneOfTinder += 1;
        if (rune == spiritVitalityRuneOfTinderRune2) spiritVitalityRuneOfTinder += 2;
        if (rune == spiritVitalityRuneOfTinderRune3) spiritVitalityRuneOfTinder += 3;

        if (rune == vitalityAgilityRuneOfEscapismRune1) vitalityAgilityRuneOfEscapism += 1;
        if (rune == vitalityAgilityRuneOfEscapismRune2) vitalityAgilityRuneOfEscapism += 2;
        if (rune == vitalityAgilityRuneOfEscapismRune3) vitalityAgilityRuneOfEscapism += 3;

        if (rune == vitalityAgilityRuneOfRageRune1) vitalityAgilityRuneOfRage += 1;
        if (rune == vitalityAgilityRuneOfRageRune2) vitalityAgilityRuneOfRage += 2;
        if (rune == vitalityAgilityRuneOfRageRune3) vitalityAgilityRuneOfRage += 3;


        if (rune == vitalityPowerRuneOfBashRune1) vitalityPowerRuneOfBash += 1;
        if (rune == vitalityPowerRuneOfBashRune2) vitalityPowerRuneOfBash += 2;
        if (rune == vitalityPowerRuneOfBashRune3) vitalityPowerRuneOfBash += 3;

        if (rune == vitalityPowerRuneOfStressRune1) vitalityPowerRuneOfStress += 1;
        if (rune == vitalityPowerRuneOfStressRune2) vitalityPowerRuneOfStress += 2;
        if (rune == vitalityPowerRuneOfStressRune3) vitalityPowerRuneOfStress += 3;
    }

    public void AddEquipment(EquipmentObject equipment)
    {
        foundItems++;
    }

    public void AddKilledEnemy(GameObject entity)
    {
        killedEnemiesTotal++;
        if (entity.GetComponent<EntityStats>().entityName == "Goblin") killedGoblins++;
        else if (entity.GetComponent<EntityStats>().entityName == "Flyer") killedFlyers++;
        else if (entity.GetComponent<EntityStats>().entityName == "Skelebro") killedSkeletons++;
        else if (entity.GetComponent<EntityStats>().entityName == "Summoner") killedSummoners++;
        else if (entity.GetComponent<EntityStats>().entityName == "Hoglon") killedHoglins++;
    }

    public void AddClearedRoom()
    {
        clearedRooms++;
    }
    public void AddClearedFloor()
    {
        clearedFloors++;
    }
    private void DealDamage(Damage damage, GameObject target)
    {
        damageDealt += damage._damage;
        damageDealt += damage._trueDamage;
        totalHits++;
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        player.GetComponent<EntityEvents>().OnKillEnemy += AddKilledEnemy;
        GameObject.Find("Game Manager").GetComponent<GameEventManager>().OnRunePicked += AddRune;
        GameObject.Find("Game Manager").GetComponent<GameEventManager>().OnEquipmentDropepd += AddEquipment;
        player.GetComponent<EntityEvents>().OnHitEnemy += DealDamage;
        player.GetComponent<EntityEvents>().OnDie += SaveGameData; 
    }
    private void Unsubscribe()
    {
        //player.GetComponent<EntityEvents>().OnKillEnemy -= AddKilledEnemy;
        //GameObject.Find("Game Manager").GetComponent<GameEventManager>().OnRunePicked -= AddRune;
        //GameObject.Find("Game Manager").GetComponent<GameEventManager>().OnEquipmentDropepd -= AddEquipment;
    }

    public void EndGameSaveStats()
    {
        Debug.Log("Saving game stats");
    }

    public void SaveGameData(GameObject killer, GameObject player)
    {
        PlayerPrefs.SetInt("Tenacity", vitalityRuneOfTenacity);


        PlayerPrefs.SetInt("Tenacity", PlayerPrefs.GetInt("Tenacity", 0) + vitalityRuneOfTenacity);
        PlayerPrefs.SetInt("Stagger", PlayerPrefs.GetInt("Stagger", 0) + vitalityRuneOfStagger);
        PlayerPrefs.SetInt("Recovery", PlayerPrefs.GetInt("Recovery", 0) + vitalityRuneOfRecovery);
        PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health", 0) + vitalityRuneOfHealth);
        PlayerPrefs.SetInt("Growth", PlayerPrefs.GetInt("Growth", 0) + vitalityRuneOfGrowth);
        PlayerPrefs.SetInt("Armor", PlayerPrefs.GetInt("Armor", 0) + vitalityRuneOfArmor);

        PlayerPrefs.SetInt("Haste", PlayerPrefs.GetInt("Haste", 0) + spiritRuneOfHaste);
        PlayerPrefs.SetInt("Healing", PlayerPrefs.GetInt("Healing", 0) + spiritRuneOfHealing);
        PlayerPrefs.SetInt("Orbs", PlayerPrefs.GetInt("Orbs", 0) + spiritRuneOfOrbs);
        PlayerPrefs.SetInt("Rush", PlayerPrefs.GetInt("Rush", 0) + spiritRuneOfRush);
        PlayerPrefs.SetInt("Shield", PlayerPrefs.GetInt("Shield", 0) + spiritRuneOfShield);
        PlayerPrefs.SetInt("Thorns", PlayerPrefs.GetInt("Thorns", 0) + spiritRuneOfThorns);
        PlayerPrefs.SetInt("Thunder", PlayerPrefs.GetInt("Thunder", 0) + spiritRuneOfThunder);

        PlayerPrefs.SetInt("Blade", PlayerPrefs.GetInt("Blade", 0) + powerRuneOfBlade);
        PlayerPrefs.SetInt("Devourer", PlayerPrefs.GetInt("Devourer", 0) + powerRuneOfDevourer);
        PlayerPrefs.SetInt("Execution", PlayerPrefs.GetInt("Execution", 0) + powerRuneOfExecution);
        PlayerPrefs.SetInt("Leech", PlayerPrefs.GetInt("Leech", 0) + powerRuneOfLeech);
        PlayerPrefs.SetInt("Penetration", PlayerPrefs.GetInt("Penetration", 0) + powerRuneOfPenetration);
        PlayerPrefs.SetInt("Strength", PlayerPrefs.GetInt("Strength", 0) + powerRuneOfStrength);

        PlayerPrefs.SetInt("Analyst", PlayerPrefs.GetInt("Analyst", 0) + agilityRuneOfAnalyst);
        PlayerPrefs.SetInt("Crit", PlayerPrefs.GetInt("Crit", 0) + agilityRuneOfCrit);
        PlayerPrefs.SetInt("HawkEye", PlayerPrefs.GetInt("HawkEye", 0) + agilityRuneOfHawkEye);
        PlayerPrefs.SetInt("Hunting", PlayerPrefs.GetInt("Hunting", 0) + agilityRuneOfHunting);
        PlayerPrefs.SetInt("Predator", PlayerPrefs.GetInt("Predator", 0) + agilityRuneOfPredator);
        PlayerPrefs.SetInt("Speed", PlayerPrefs.GetInt("Speed", 0) + agilityRuneOfSpeed);
        PlayerPrefs.SetInt("Swiftness", PlayerPrefs.GetInt("Swiftness", 0) + agilityRuneOfSwiftness);

        PlayerPrefs.SetInt("Beauty", PlayerPrefs.GetInt("Beauty", 0) + agilityPowerRuneOfBeauty);
        PlayerPrefs.SetInt("Flow", PlayerPrefs.GetInt("Flow", 0) + agilityPowerRuneOfFlow);

        PlayerPrefs.SetInt("Kinesis", PlayerPrefs.GetInt("Kinesis", 0) + spiritAgilityRuneOfKinesis);
        PlayerPrefs.SetInt("ThunderDash", PlayerPrefs.GetInt("ThunderDash", 0) + spiritAgilityRuneOfThunderDash);

        PlayerPrefs.SetInt("Explosion", PlayerPrefs.GetInt("Explosion", 0) + spiritPowerRuneOfExplosion);
        PlayerPrefs.SetInt("Patience", PlayerPrefs.GetInt("Patience", 0) + spiritPowerRuneOfPatience);

        PlayerPrefs.SetInt("Expenditure", PlayerPrefs.GetInt("Expenditure", 0) + spiritVitalityRuneOfExpenditure);
        PlayerPrefs.SetInt("Tinder", PlayerPrefs.GetInt("Tinder", 0) + spiritVitalityRuneOfTinder);

        PlayerPrefs.SetInt("Escapism", PlayerPrefs.GetInt("Escapism", 0) + vitalityAgilityRuneOfEscapism);
        PlayerPrefs.SetInt("Rage", PlayerPrefs.GetInt("Rage", 0) + vitalityAgilityRuneOfRage);

        PlayerPrefs.SetInt("Bash", PlayerPrefs.GetInt("Bash", 0) + vitalityPowerRuneOfBash);
        PlayerPrefs.SetInt("Stress", PlayerPrefs.GetInt("Stress", 0) + vitalityPowerRuneOfStress);

        PlayerPrefs.SetInt("TotalEnemiesKilled", PlayerPrefs.GetInt("TotalEnemiesKilled", 0) + killedEnemiesTotal);
        PlayerPrefs.SetInt("TotalGoblinsKilled", PlayerPrefs.GetInt("TotalGoblinsKilled", 0) + killedGoblins);
        PlayerPrefs.SetInt("TotalKilledFlyers", PlayerPrefs.GetInt("TotalKilledFlyers", 0) + killedFlyers);
        PlayerPrefs.SetInt("TotalSkeletonsKilled", PlayerPrefs.GetInt("TotalSkeletonsKilled", 0) + killedSkeletons);
        PlayerPrefs.SetInt("TotalSummonersKilled", PlayerPrefs.GetInt("TotalSummonersKilled", 0) + killedSummoners);
        PlayerPrefs.SetInt("TotalKilledHoglins", PlayerPrefs.GetInt("TotalKilledHoglins", 0) + killedHoglins);
        PlayerPrefs.SetInt("TotalClearedFloors", PlayerPrefs.GetInt("TotalClearedFloors", 0) + clearedFloors);
        PlayerPrefs.SetInt("TotalClearedRooms", PlayerPrefs.GetInt("TotalClearedRooms", 0) + clearedRooms);

        PlayerPrefs.SetInt("TotalGameTime", PlayerPrefs.GetInt("TotalGameTime", 0) + (int)GameObject.Find("Game Manager").GetComponent<GameEventManager>().time);
        PlayerPrefs.SetInt("TotalRunesPicked", PlayerPrefs.GetInt("TotalRunesPicked", 0) + runesPicked);
        PlayerPrefs.SetInt("TotalItemsFound", PlayerPrefs.GetInt("TotalItemsFound", 0) + foundItems);
        PlayerPrefs.SetInt("TotalDamageDealt", PlayerPrefs.GetInt("TotalDamageDealt", 0) + damageDealt);
        if(topDmg > PlayerPrefs.GetInt("HighestDamageAttack", 0)) PlayerPrefs.SetInt("HighestDamageAttack", topDmg);
        if(damageDealt > PlayerPrefs.GetInt("HighestDamageInGame", 0)) PlayerPrefs.SetInt("HighestDamageInGame", damageDealt);
        PlayerPrefs.SetInt("TotalDeathCount", PlayerPrefs.GetInt("TotalDeathCount", 0) + 1);
        PlayerPrefs.SetInt("TotalHits", PlayerPrefs.GetInt("TotalHits", 0) + totalHits);
        PlayerPrefs.SetInt("TotalBossesKilled", PlayerPrefs.GetInt("TotalBossesKilled", 0) + killedHoglins + killedSummoners);
        if(killer.GetComponent<EntityStats>().entityName == "Goblin") PlayerPrefs.SetInt("TotalKilledByGoblin", PlayerPrefs.GetInt("TotalKilledByGoblin", 0) + 1);
        if (killer.GetComponent<EntityStats>().entityName == "Skelebro") PlayerPrefs.SetInt("TotalKilledBySkeleton", PlayerPrefs.GetInt("TotalKilledBySkeleton", 0) + 1);
        if (killer.GetComponent<EntityStats>().entityName == "Summoner") PlayerPrefs.SetInt("TotalKilledBySummoner", PlayerPrefs.GetInt("TotalKilledBySummoner", 0) + 1);
        if (killer.GetComponent<EntityStats>().entityName == "Hoglon") PlayerPrefs.SetInt("TotalKilledByHoglon", PlayerPrefs.GetInt("TotalKilledByHoglon", 0) + 1);
        if (killer.GetComponent<EntityStats>().entityName == "Flyer") PlayerPrefs.SetInt("TotalKilledByFlyer", PlayerPrefs.GetInt("TotalKilledByFlyer", 0) + 1);
    }
}
