using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class EntityHealth : MonoBehaviour
{
    private EntityEvents events;
    private EntityStats stats;
    public int health;
    private float oneHealth;
    private bool fireTickOnCD = false;
    private float timeBetweenFireTicks = 1f;


    private GameObject flame;


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
        DepleteShield();

        if (stats.isOnFire)
        {
            if(!fireTickOnCD)
            {
                fireTickOnCD = true;
                StartCoroutine(RireTick());
                if(flame == null)
                {
                    flame = Instantiate(GameAssets.i.flameEffect, transform.position, transform.rotation);
                    flame.transform.parent = transform;
                }
                else
                {
                    flame.GetComponent<ParticleSystem>().Play();
                    flame.GetComponentInChildren<ParticleSystem>().Play();
                }
            }
        }
        else
        {
            if(flame != null)
            {
                StartCoroutine(TurnOffFlame());
                
            }
        }
    }
    private IEnumerator TurnOffFlame()
    {
        GameObject temp = flame;
        flame = null;
        temp.GetComponent<ParticleSystem>().Stop();
        temp.GetComponentInChildren<ParticleSystem>().Stop();
        
        yield return new WaitForSeconds(2);
        temp.GetComponentInChildren<LightOverTimeChange>().falloutPerSecond = 1;
        if (flame == null) Destroy(temp);


    }

    private IEnumerator RireTick()
    {
        events.HitThis(new Damage(GameObject.Find("FireDispenser"), false, (int)(stats.currentMaxHealth * 0.02f)));
        yield return new WaitForSeconds(timeBetweenFireTicks);
        fireTickOnCD = false;
    }

    private void GainShield(int amount)
    {
        if (stats.currentShield + amount > stats.currentMaxHealth)
        {
            stats.currentShield = stats.currentMaxHealth;
        }
        else stats.currentMaxHealth += amount;
    }

    private void Subscribe()
    {
        events.OnGainShield += GainShield;
        events.OnSetHealth += SetHealth;
        events.OnTryCastAbilityCostHealth += CheckIfEnoughToCast;
        events.OnDeteriorateHealth += TakeDamage;
        events.OnHitThis += DamageCalculation;
        events.OnRecoverHealth += GainHealth;
    }
    private void Unsubscribe()
    {
        events.OnGainShield -= GainShield;
        events.OnSetHealth -= SetHealth;
        events.OnTryCastAbilityCostHealth -= CheckIfEnoughToCast;
        events.OnDeteriorateHealth -= TakeDamage;
        events.OnHitThis -= DamageCalculation;
        events.OnRecoverHealth -= GainHealth;
    }

    private void SetHealth(int value)
    {
        health = value;
    }

    private void DamageCalculation(Damage damage)
    {
        Debug.Log(damage._damage);
        if (damage._damage > 0) events.PhysicalDamageTaken(damage._damage);
        TakeDamage((int)(damage._damage * GetDamageReduction()), damage);
        Debug.Log(GetDamageReduction());
    }

    private float GetDamageReduction()
    {
        //To be adjusted later
        float armorBalanceValue = 100f;
        return (armorBalanceValue / (armorBalanceValue + stats.currentArmor));
    }

    private void TakeDamage(int damage, Damage damgeContainer)
    {
        if(damgeContainer._damage > 0) DamagePopup.Create(transform.position, damage, damgeContainer._isCriticalHit, false);
        if(damgeContainer._trueDamage > 0) DamagePopup.Create(transform.position, damage, damgeContainer._isCriticalHit, true);
        events.LoseHealth(damage);
        if(stats.currentShield > 0)
        {
            if(stats.currentShield - damage < 0)
            {
                stats.currentShield = 0;
                damage = damage - (int)stats.currentShield;

                if (health - damage <= 0)
                {
                    events.Die(damgeContainer.source);
                    damgeContainer.source.GetComponent<EntityEvents>().KillEnemy(gameObject);
                }
                else health -= damage;
            }
            else
            {
                stats.currentShield -= damage;
            }
        }

        

    }
    private void GainHealth(int amount)
    {
        Debug.Log("Gained health");
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
    private void DepleteShield()
    {
        if(stats.currentShield > 0)
        {
            stats.currentShield -= (0.1f * stats.currentShield * Time.deltaTime);
        }
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
