using UnityEngine;

public class EntityLevelScaling : MonoBehaviour
{
    private EntityStats stats;

    [SerializeField] private int healthPerLevel;
    [SerializeField] private int healthRegenPerLevel;
    [SerializeField] private int physicalDamagePerLevel;
    [SerializeField] private int armorPerLevel;


    private void Awake()
    {
        stats = GetComponent<EntityStats>();
    }

    private void Start()
    {
        int level = stats.level;
        stats.baseHealth += healthPerLevel * level;
        stats.baseHealthRegen += healthRegenPerLevel * level;
        stats.baseArmor += armorPerLevel * level;
        stats.basePhysicalDamage += physicalDamagePerLevel * level;

        stats.UpdateMaxHealth();
        stats.UpdateHealthRegen();
        stats.UpdateArmor();
        stats.UpdatePhysicalDamage();
    }
}
