using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRage : MonoBehaviour
{
    private EntityEvents events;
    private EntityStats stats;
    [SerializeField] private int rage;
    private float oneRage;

    private void Awake()
    {
        stats = GetComponent<EntityStats>();
        events = GetComponent<EntityEvents>();
    }
    private void Start()
    {
        Subscribe();
        rage = 0;
    }
    private void Update()
    {
        DepleateRage();
    }
    
    private void Subscribe()
    {
        events.OnTryCastAbilityCostRage += CheckIfEnoughToCast;
        events.OnDeteriorateRage += LoseRage;
        events.OnRecoverRage += GainRage;
    }
    private void Unsubscribe()
    {
        events.OnTryCastAbilityCostRage -= CheckIfEnoughToCast;
        events.OnDeteriorateRage -= LoseRage;
        events.OnRecoverRage -= GainRage;
    }

    private void GainRage(int amount)
    {
        if ((rage + amount) > stats.currentMaxRage)
        {
            events.GainRage(stats.currentMaxRage - rage);
            rage = stats.currentMaxRage;
        }
        else
        {
            events.GainRage(amount);
            rage += amount;
        }
    }

    private void LoseRage(int amount)
    {
        if ((rage - amount) < 0)
        {
            events.LoseRage(rage);
            rage = 0;
        }
        else
        {
            events.LoseRage(amount);
            rage -= amount;
        }
    }

    private void DepleateRage()
    {
        oneRage += stats.currentRageDepletion / 60f * Time.deltaTime;
        if (oneRage >= 1)
        {
            oneRage = 0;
            if (rage > 0)
            {
                rage--;
            }
        }
        if (rage > stats.currentMaxRage) rage = stats.currentMaxRage;
    }

    private void CheckIfEnoughToCast(int spellSlot, int amount)
    {
        if (rage >= amount) events.CallBackCastAbility(spellSlot);
        else events.CanNotAffordAbility(spellSlot);
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
}
