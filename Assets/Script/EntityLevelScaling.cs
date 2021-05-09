using UnityEngine;

public class EntityLevelScaling : MonoBehaviour
{
    private EntityStats stats;

    [SerializeField] private int healthPerLevel;
    [SerializeField] private int healthRegenPerLevel;
    [SerializeField] private int physicalDamagePerLevel;
    [SerializeField] private int armorPerLevel;


    [SerializeField] private int healthPerFloor;
    [SerializeField] private int healthRegenPerFloor;
    [SerializeField] private int physicalDamagePerFloor;
    [SerializeField] private int armorPerFloor;
    [SerializeField] private int movementsSpeedPerFloor;

    [SerializeField] private float easyScaler;
    [SerializeField] private float normalScaler;
    [SerializeField] private float hardScaler;
    [SerializeField] private float lunaticScaler;




    private int currentLevel;
    GameEventManager gameEventManager;

    private void Awake()
    {
        stats = GetComponent<EntityStats>();
        gameEventManager = GameObject.Find("Game Manager").GetComponent<GameEventManager>();
    }

    private void Start()
    {
        currentLevel = stats.level;

        

        stats.UpdateMaxHealth();
        stats.UpdateHealthRegen();
        stats.UpdateArmor();
        stats.UpdatePhysicalDamage();
        stats.UpdateSpeed();

        Debug.Log(gameEventManager.difficulty +" "+ hardScaler+ " " + gameObject);
        switch(gameEventManager.difficulty)
        {
            case GameDifficultyManagerScript.Difficulty.Easy:

                stats.baseHealth = (int)(stats.baseHealth * easyScaler);
                stats.baseHealthRegen = (int)(stats.baseHealthRegen * easyScaler);
                stats.baseArmor = (int)(stats.baseArmor * easyScaler);
                stats.basePhysicalDamage = (int)(stats.basePhysicalDamage * easyScaler);

                stats.baseHealth += (int)((healthPerLevel * currentLevel + healthPerFloor * gameEventManager.floorsPassed) * easyScaler);
                stats.baseHealthRegen += (int)((healthRegenPerLevel * currentLevel + healthRegenPerFloor * gameEventManager.floorsPassed) * easyScaler);
                stats.baseArmor += (int)((armorPerLevel * currentLevel + armorPerFloor * gameEventManager.floorsPassed) * easyScaler);
                stats.basePhysicalDamage += (int)((physicalDamagePerLevel * currentLevel + physicalDamagePerFloor * gameEventManager.floorsPassed) * easyScaler);
                stats.baseSpeed += (int)((movementsSpeedPerFloor * gameEventManager.floorsPassed) * easyScaler);

                

                break;

            case GameDifficultyManagerScript.Difficulty.Normal:

                stats.baseHealth = (int)(stats.baseHealth * normalScaler);
                stats.baseHealthRegen = (int)(stats.baseHealthRegen * normalScaler);
                stats.baseArmor = (int)(stats.baseArmor * normalScaler);
                stats.basePhysicalDamage = (int)(stats.basePhysicalDamage * normalScaler);

                stats.baseHealth += (int)((healthPerLevel * currentLevel + healthPerFloor * gameEventManager.floorsPassed) * normalScaler);
                stats.baseHealthRegen += (int)((healthRegenPerLevel * currentLevel + healthRegenPerFloor * gameEventManager.floorsPassed) * normalScaler);
                stats.baseArmor += (int)((armorPerLevel * currentLevel + armorPerFloor * gameEventManager.floorsPassed) * normalScaler);
                stats.basePhysicalDamage += (int)((physicalDamagePerLevel * currentLevel + physicalDamagePerFloor * gameEventManager.floorsPassed) * normalScaler);
                stats.baseSpeed += (int)((movementsSpeedPerFloor * gameEventManager.floorsPassed) * normalScaler);
                break;

            case GameDifficultyManagerScript.Difficulty.Hard:

                stats.baseHealth = (int)(stats.baseHealth * hardScaler);
                stats.baseHealthRegen = (int)(stats.baseHealthRegen * hardScaler);
                stats.baseArmor = (int)(stats.baseArmor * hardScaler);
                stats.basePhysicalDamage = (int)(stats.basePhysicalDamage * hardScaler);

                stats.baseHealth += (int)((healthPerLevel * currentLevel + healthPerFloor * gameEventManager.floorsPassed) * hardScaler);
                stats.baseHealthRegen += (int)((healthRegenPerLevel * currentLevel + healthRegenPerFloor * gameEventManager.floorsPassed) * hardScaler);
                stats.baseArmor += (int)((armorPerLevel * currentLevel + armorPerFloor * gameEventManager.floorsPassed) * hardScaler);
                stats.basePhysicalDamage += (int)((physicalDamagePerLevel * currentLevel + physicalDamagePerFloor * gameEventManager.floorsPassed) * hardScaler);
                stats.baseSpeed += (int)((movementsSpeedPerFloor * gameEventManager.floorsPassed) * hardScaler);
                break;

            case GameDifficultyManagerScript.Difficulty.Lunatic:
                stats.baseHealth += (int)((healthPerLevel * currentLevel + healthPerFloor * gameEventManager.floorsPassed) * normalScaler);
                stats.baseHealthRegen += (int)((healthRegenPerLevel * currentLevel + healthRegenPerFloor * gameEventManager.floorsPassed) * normalScaler);
                stats.baseArmor += (int)((armorPerLevel * currentLevel + armorPerFloor * gameEventManager.floorsPassed) * normalScaler);
                stats.basePhysicalDamage += (int)((physicalDamagePerLevel * currentLevel + physicalDamagePerFloor * gameEventManager.floorsPassed) * normalScaler);
                stats.baseSpeed += (int)((movementsSpeedPerFloor * gameEventManager.floorsPassed) * normalScaler);
                break;
        }
    }

    private void Update()
    {
        if(currentLevel != stats.level)
        {
            currentLevel = stats.level;


            stats.baseHealth += healthPerLevel;
            stats.baseHealthRegen += healthRegenPerLevel;
            stats.baseArmor += armorPerLevel;
            stats.basePhysicalDamage += physicalDamagePerLevel;

            stats.UpdateMaxHealth();
            stats.UpdateHealthRegen();
            stats.UpdateArmor();
            stats.UpdatePhysicalDamage();
        }
    }
}
