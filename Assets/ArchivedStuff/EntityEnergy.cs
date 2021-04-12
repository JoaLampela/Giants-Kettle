using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEnergy : MonoBehaviour
{
    private EntityEvents events;
    private ArchivedEntityStats stats;
    [SerializeField] private int energy;
    private float oneEnergy;

    private void Awake()
    {
        stats = GetComponent<ArchivedEntityStats>();
        events = GetComponent<EntityEvents>();
    }
    private void Start()
    {
        Subscribe();
    }
    private void Update()
    {
        RegenEnergy();
    }
  
    private void Subscribe()
    {
        events.OnSetEnergy += SetEnergy;
        events.OnTryCastAbilityCostEnergy += CheckIfEnoughToCast;
        events.OnDeteriorateEnergy += LoseEnergy;
        events.OnRecoverEnergy += GainEnergy;
    }
    private void Unsubscribe()
    {
        events.OnSetEnergy -= SetEnergy;
        events.OnTryCastAbilityCostEnergy -= CheckIfEnoughToCast;
        events.OnDeteriorateEnergy -= LoseEnergy;
        events.OnRecoverEnergy -= GainEnergy;
    }

    private void SetEnergy(int value)
    {
        energy = value;
    }

    private void GainEnergy(int amount)
    {
        if ((energy + amount) > stats.currentMaxEnergy)
        {
            events.GainEnergy(stats.currentMaxEnergy - energy);
            energy = stats.currentMaxHealth;
        }
        else
        {
            events.GainEnergy(amount);
            energy += amount;
        }
    }

    private void LoseEnergy(int amount)
    {
        if ((energy - amount) < 0)
        {
            events.LoseEnergy(energy);
            energy = 0;
        }
        else
        {
            events.LoseEnergy(energy);
            energy -= amount;
        }
    }

    private void RegenEnergy()
    {
        oneEnergy += stats.currentEnergyRegen / 60f * Time.deltaTime;
        if (oneEnergy >= 1)
        {
            oneEnergy = 0;
            if (energy < stats.currentMaxEnergy)
            {
                energy++;
            }
        }
        if (energy > stats.currentMaxEnergy) energy = stats.currentMaxEnergy;
    }

    private void CheckIfEnoughToCast(int spellSlot, int amount)
    {
        if (energy >= amount) events.CallBackCastAbility(spellSlot);
        else events.CanNotAffordAbility(spellSlot);
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
}
