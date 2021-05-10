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

    [SerializeField] float scalingIncrementPerFloor = 0;

    


    private int currentLevel;
    GameEventManager gameEventManager;

    private void Awake()
    {
        stats = GetComponent<EntityStats>();
        gameEventManager = GameObject.Find("Game Manager").GetComponent<GameEventManager>();
    }

    private void Start()
    {
        currentLevel = gameEventManager.globalLevel;
        int floors = gameEventManager.floorsPassed;
        

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

                stats.baseHealth += (int)((healthPerLevel * currentLevel + healthPerFloor * gameEventManager.floorsPassed) * (easyScaler +floors*scalingIncrementPerFloor));
                stats.baseHealthRegen += (int)((healthRegenPerLevel * currentLevel + healthRegenPerFloor * gameEventManager.floorsPassed) * (easyScaler  + floors * scalingIncrementPerFloor));
                stats.baseArmor += (int)((armorPerLevel * currentLevel + armorPerFloor * gameEventManager.floorsPassed) * (easyScaler + floors * scalingIncrementPerFloor));
                stats.basePhysicalDamage += (int)((physicalDamagePerLevel * currentLevel + physicalDamagePerFloor * gameEventManager.floorsPassed) * (easyScaler  + floors * scalingIncrementPerFloor));
                stats.baseSpeed += (int)((movementsSpeedPerFloor * gameEventManager.floorsPassed) * (easyScaler + floors * scalingIncrementPerFloor));

                

                break;

            case GameDifficultyManagerScript.Difficulty.Normal:
                stats.baseHealth = (int)(stats.baseHealth * normalScaler);
                stats.baseHealthRegen = (int)(stats.baseHealthRegen * normalScaler);
                stats.baseArmor = (int)(stats.baseArmor * normalScaler);
                stats.basePhysicalDamage = (int)(stats.basePhysicalDamage * normalScaler);

                stats.baseHealth += (int)((healthPerLevel * currentLevel + healthPerFloor * gameEventManager.floorsPassed) * (normalScaler  + floors * scalingIncrementPerFloor));
                stats.baseHealthRegen += (int)((healthRegenPerLevel * currentLevel + healthRegenPerFloor * gameEventManager.floorsPassed) * (normalScaler  + floors * scalingIncrementPerFloor));
                stats.baseArmor += (int)((armorPerLevel * currentLevel + armorPerFloor * gameEventManager.floorsPassed) * (normalScaler  + floors * scalingIncrementPerFloor));
                stats.basePhysicalDamage += (int)((physicalDamagePerLevel * currentLevel + physicalDamagePerFloor * gameEventManager.floorsPassed) * (normalScaler + floors * scalingIncrementPerFloor));
                stats.baseSpeed += (int)((movementsSpeedPerFloor * gameEventManager.floorsPassed) * (normalScaler + floors * scalingIncrementPerFloor));
                break;

            case GameDifficultyManagerScript.Difficulty.Hard:

                stats.baseHealth = (int)(stats.baseHealth * hardScaler);
                stats.baseHealthRegen = (int)(stats.baseHealthRegen * hardScaler);
                stats.baseArmor = (int)(stats.baseArmor * hardScaler);
                stats.basePhysicalDamage = (int)(stats.basePhysicalDamage * hardScaler);

                stats.baseHealth += (int)((healthPerLevel * currentLevel + healthPerFloor * gameEventManager.floorsPassed) * (hardScaler + floors * scalingIncrementPerFloor));
                stats.baseHealthRegen += (int)((healthRegenPerLevel * currentLevel + healthRegenPerFloor * gameEventManager.floorsPassed) * (hardScaler + floors * scalingIncrementPerFloor));
                stats.baseArmor += (int)((armorPerLevel * currentLevel + armorPerFloor * gameEventManager.floorsPassed) * (hardScaler + floors * scalingIncrementPerFloor));
                stats.basePhysicalDamage += (int)((physicalDamagePerLevel * currentLevel + physicalDamagePerFloor * gameEventManager.floorsPassed) * (hardScaler + floors * scalingIncrementPerFloor));
                stats.baseSpeed += (int)((movementsSpeedPerFloor * gameEventManager.floorsPassed) * (hardScaler + floors * scalingIncrementPerFloor));
                break;

            case GameDifficultyManagerScript.Difficulty.Lunatic:

                stats.baseHealth = (int)(stats.baseHealth * lunaticScaler);
                stats.baseHealthRegen = (int)(stats.baseHealthRegen * lunaticScaler);
                stats.baseArmor = (int)(stats.baseArmor * lunaticScaler);
                stats.basePhysicalDamage = (int)(stats.basePhysicalDamage * lunaticScaler);


                stats.baseHealth += (int)((healthPerLevel * currentLevel + healthPerFloor * gameEventManager.floorsPassed) * (lunaticScaler + floors * scalingIncrementPerFloor));
                stats.baseHealthRegen += (int)((healthRegenPerLevel * currentLevel + healthRegenPerFloor * gameEventManager.floorsPassed) * (lunaticScaler + floors * scalingIncrementPerFloor));
                stats.baseArmor += (int)((armorPerLevel * currentLevel + armorPerFloor * gameEventManager.floorsPassed) * (lunaticScaler + floors * scalingIncrementPerFloor));
                stats.basePhysicalDamage += (int)((physicalDamagePerLevel * currentLevel + physicalDamagePerFloor * gameEventManager.floorsPassed) * (lunaticScaler + floors * scalingIncrementPerFloor));
                stats.baseSpeed += (int)((movementsSpeedPerFloor * gameEventManager.floorsPassed) * (lunaticScaler + floors * scalingIncrementPerFloor));
                break;
        }
    }

    private void Update()
    {
        if (currentLevel != stats.level)
        {
            currentLevel = stats.level;


            

            
            switch (gameEventManager.difficulty)
            {

                case GameDifficultyManagerScript.Difficulty.Easy:

                    stats.baseHealth += (int)(healthPerLevel *easyScaler);
                    stats.baseHealthRegen += (int)(healthRegenPerLevel * easyScaler);
                    stats.baseArmor += (int)(armorPerLevel * easyScaler);
                    stats.basePhysicalDamage += (int)(physicalDamagePerLevel * easyScaler);

                    break;
                case GameDifficultyManagerScript.Difficulty.Normal:

                    stats.baseHealth += (int)(healthPerLevel *normalScaler);
                    stats.baseHealthRegen += (int)(healthRegenPerLevel * normalScaler);
                    stats.baseArmor += (int)(armorPerLevel * normalScaler);
                    stats.basePhysicalDamage += (int)(physicalDamagePerLevel * normalScaler);

                    break;
                case GameDifficultyManagerScript.Difficulty.Hard:

                    stats.baseHealth += (int)(healthPerLevel *hardScaler);
                    stats.baseHealthRegen += (int)(healthRegenPerLevel * hardScaler);
                    stats.baseArmor += (int)(armorPerLevel * hardScaler);
                    stats.basePhysicalDamage += (int)(physicalDamagePerLevel * hardScaler);

                    break;
                case GameDifficultyManagerScript.Difficulty.Lunatic:

                    stats.baseHealth += (int)(healthPerLevel * lunaticScaler);
                    stats.baseHealthRegen += (int)(healthRegenPerLevel * lunaticScaler);
                    stats.baseArmor += (int)(armorPerLevel * lunaticScaler);
                    stats.basePhysicalDamage += (int)(physicalDamagePerLevel * lunaticScaler);

                    break;
            }

            stats.UpdateMaxHealth();
            stats.UpdateHealthRegen();
            stats.UpdateArmor();
            stats.UpdatePhysicalDamage();

        }
            
    }
}
