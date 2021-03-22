using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGroundSlam : MonoBehaviour, IAbility
{
    [SerializeField] private GameObject ability;
    EntityEvents events;
    [SerializeField] int spellSlot;
    [SerializeField] private int abilityCostEnergy = 30;

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
        events.TryCastAbilityCostEnergy(spellSlot, abilityCostEnergy);
    }

    public void Cast(int slot)
    {
        if (slot == spellSlot)
        {
            events.DeteriorateEnergy(abilityCostEnergy);
            events.CastAbility();
            Debug.Log("Casted Ground Slam!");
            GameObject temp = Instantiate(ability, transform.position, transform.rotation);
            temp.GetComponent<AbilityEvents>().SetSource(gameObject);
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
