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

        stats.baseHealth += healthPerLevel * currentLevel + healthPerFloor * gameEventManager.floorsPassed;
        stats.baseHealthRegen += healthRegenPerLevel * currentLevel + healthRegenPerFloor * gameEventManager.floorsPassed;
        stats.baseArmor += armorPerLevel * currentLevel + armorPerFloor * gameEventManager.floorsPassed;
        stats.basePhysicalDamage += physicalDamagePerLevel * currentLevel + physicalDamagePerFloor * gameEventManager.floorsPassed;
        stats.baseSpeed += movementsSpeedPerFloor * gameEventManager.floorsPassed;

        stats.UpdateMaxHealth();
        stats.UpdateHealthRegen();
        stats.UpdateArmor();
        stats.UpdatePhysicalDamage();
        stats.UpdateSpeed();
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
