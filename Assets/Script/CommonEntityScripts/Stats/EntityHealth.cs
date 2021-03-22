using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    private EntityEvents events;
    private EntityStats stats;
    [SerializeField] private int health;
    private float oneHealth;

    private void Awake()
    {
        stats = GetComponent<EntityStats>();
        events = GetComponent<EntityEvents>();
    }
    private void Start()
    {
        Subscribe();
    }
    private void Update()
    {
        RegenHealth();
    }
    private void SetStartHealth()
    {
        health = stats.currentMaxHealth;
    }
    private void Subscribe()
    {
        events.OnTryCastAbilityCostHealth += CheckIfEnoughToCast;
        events.OnDeteriorateHealth += TakeDamage;
        events.OnHitThis += DamageCalculation;
        events.OnStartStatsSet += SetStartHealth;
        events.OnRecoverHealth += GainHealth;
    }
    private void Unsubscribe()
    {
        events.OnTryCastAbilityCostHealth -= CheckIfEnoughToCast;
        events.OnDeteriorateHealth -= TakeDamage;
        events.OnHitThis -= DamageCalculation;
        events.OnStartStatsSet -= SetStartHealth;
        events.OnRecoverHealth -= GainHealth;
    }

    private void DamageCalculation(Damage damage)
    {
        if (damage.physicalDamage > 0) events.PhysicalDamageTaken(damage.physicalDamage);
        if (damage.spiritDamage > 0) events.SpiritDamageTaken(damage.spiritDamage);
        TakeDamage((int)((damage.physicalDamage + damage.spiritDamage) * GetDamageReduction()));
    }

    private float GetDamageReduction()
    {
        //To be adjusted later
        int armorBalanceValue = 100;
        return (armorBalanceValue / (armorBalanceValue + stats.currentArmor));
    }

    private void TakeDamage(int damage)
    {
        events.LoseHealth(damage);
        if(health - damage <= 0) events.Die();
        else health -= damage;
    }
    private void GainHealth(int amount)
    {
        if ((health + amount) > stats.currentMaxHealth)
        {
            events.GainHealth(stats.currentMaxHealth - health);
            health = stats.currentMaxHealth;
        }
        else
        {
            events.GainHealth(amount);
            health += amount;
        }
    }

    private void RegenHealth()
    {
        oneHealth += stats.currentHealthRegen / 60f * Time.deltaTime;
        if (oneHealth >= 1)
        {
            oneHealth = 0;
            if (health < stats.currentMaxHealth)
            {
                health++;
            }
        }
        if (health > stats.currentMaxHealth) health = stats.currentMaxHealth;
    }


    private void CheckIfEnoughToCast(int spellSlot, int amount)
    {
        if (health >= amount) events.CallBackCastAbility(spellSlot);
        else events.CanNotAffordAbility(spellSlot);
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
}
