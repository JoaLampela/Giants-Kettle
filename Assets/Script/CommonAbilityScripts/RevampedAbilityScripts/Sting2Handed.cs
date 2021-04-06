using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sting2Handed : MonoBehaviour, IAbility
{
    EntityEvents _entityEvents;
    [SerializeField] private int _spellSlot;
    [SerializeField] private int _abilityCost = 10;
    //ItemWeapon _weapon;


    private void Start()
    {
        Subscribe();
        //_weapon = (ItemWeapon)GetComponent<Inventory>().rightHand._item;
    }

    private void Awake()
    {
        _entityEvents = GetComponent<EntityEvents>();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    public int GetCastValue()
    {
        return -1;
    }

    public void SetSlot(int slot)
    {
        _spellSlot = slot;
    }

    public void TryCast()
    {
        _entityEvents.TryCastAbilityCostHealth(_spellSlot, _abilityCost);
    }

    private void Cast(int slot)
    {
        if (_spellSlot == slot)
        {
            Debug.Log("Casted Sting 2handed");
            _entityEvents.DeteriorateHealth(_abilityCost);
        }
    }

    private void CannotAffordCast(int slot)
    {
        if (_spellSlot == slot)
        {
            Debug.Log("CANNOT AFFORD TO CAST STING 2HANDED");
        }
    }

    private void Subscribe()
    {
        _entityEvents.OnCallBackCastAbility += Cast;
        _entityEvents.OnCanNotAffordAbility += CannotAffordCast;
    }

    public void Unsubscribe()
    {
        _entityEvents.OnCallBackCastAbility -= Cast;
        _entityEvents.OnCanNotAffordAbility -= CannotAffordCast;
    }
}
