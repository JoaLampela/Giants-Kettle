using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFireBall : MonoBehaviour, IAbility
{
    EntityEvents events;
    [SerializeField]int spellSlot;
    [SerializeField] private int abilityCostSpirit = 30;

    private void Awake()
    {
        events = GetComponent<EntityEvents>();
    }
    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        events.OnCallBackCastAbility += Cast;
    }
    private void Unsubscribe()
    {
        events.OnCallBackCastAbility -= Cast;
    }

    public void TryCast()
    {
        events.TryCastAbilityCostSpirit(spellSlot, abilityCostSpirit);
    }
    public void Cast(int slot)
    {
        if(slot == spellSlot)
        {
            events.DeteriorateSpirit(abilityCostSpirit);
            events.CastAbility();
            Debug.Log("Casted FireBall!");
            events.NewBuff("On Fire", "bonusCriticalStrikeChance", 50, 3);
        }
    }

    public int GetCastValue()
    {
        throw new System.NotImplementedException();
    }

    public void SetSlot(int slot)
    {
        spellSlot = slot;
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
}
