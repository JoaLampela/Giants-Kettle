using UnityEngine;

public class EntityLevelScaling : MonoBehaviour
{
    private EntityStats stats;

    [SerializeField] private int healthPerLevel;
    [SerializeField] private int healthRegenPerLevel;
    [SerializeField] private int physicalDamagePerLevel;
    [SerializeField] private int armorPerLevel;

    private int currentLevel;


    private void Awake()
    {
        stats = GetComponent<EntityStats>();
    }

    private void Start()
    {
        currentLevel = stats.level;

        stats.baseHealth += healthPerLevel * currentLevel;
        stats.baseHealthRegen += healthRegenPerLevel * currentLevel;
        stats.baseArmor += armorPerLevel * currentLevel;
        stats.basePhysicalDamage += physicalDamagePerLevel * currentLevel;

        stats.UpdateMaxHealth();
        stats.UpdateHealthRegen();
        stats.UpdateArmor();
        stats.UpdatePhysicalDamage();
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
