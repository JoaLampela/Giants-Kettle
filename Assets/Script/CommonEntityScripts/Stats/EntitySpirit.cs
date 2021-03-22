using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpirit : MonoBehaviour
{
    private EntityEvents events;
    private EntityStats stats;
    [SerializeField] private int spirit;
    private float oneSpirit;

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
        RegenSpirit();
    }
    private void SetStartSpirit()
    {
        spirit = stats.currentMaxSpirit;
    }
    private void Subscribe()
    {
        events.OnTryCastAbilityCostSpirit += CheckIfEnoughToCast;
        events.OnDeteriorateSpirit += LoseSpirit;
        events.OnStartStatsSet += SetStartSpirit;
        events.OnRecoverSpirit += GainSpirit;
    }
    private void Unsubscribe()
    {
        events.OnTryCastAbilityCostSpirit -= CheckIfEnoughToCast;
        events.OnDeteriorateSpirit -= LoseSpirit;
        events.OnStartStatsSet -= SetStartSpirit;
        events.OnRecoverSpirit -= GainSpirit;
    }

    private void GainSpirit(int amount)
    {
        if ((spirit + amount) > stats.currentMaxSpirit)
        {
            events.GainSpirit(stats.currentMaxSpirit - spirit);
            spirit = stats.currentMaxSpirit;
        }
        else
        {
            events.GainSpirit(amount);
            spirit += amount;
        }
    }

    private void LoseSpirit(int amount)
    {
        if ((spirit - amount) < 0)
        {
            events.LoseSpirit(spirit);
            spirit = 0;
        }
        else
        {
            events.LoseSpirit(amount);
            spirit -= amount;
        }
    }

    private void RegenSpirit()
    {
        oneSpirit += stats.currentSpiritRegen / 60f * Time.deltaTime;
        if (oneSpirit >= 1)
        {
            oneSpirit = 0;
            if (spirit < stats.currentMaxSpirit)
            {
                spirit++;
            }
        }
        if (spirit > stats.currentMaxSpirit) spirit = stats.currentMaxSpirit;
    }

    private void CheckIfEnoughToCast(int spellSlot, int amount)
    {
        if (spirit >= amount) events.CallBackCastAbility(spellSlot);
        else events.CanNotAffordAbility(spellSlot);
    }
    private void OnDisable()
    {
        Unsubscribe();
    }
}
