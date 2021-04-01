using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StingLeft : MonoBehaviour, IAbility
{
    [SerializeField] private GameObject _ability;
    EntityEvents _entityEvents;
    AbilityEvents _abilityEvents;
    [SerializeField] private int _spellSlot;
    [SerializeField] private int _abilityCost = 30;
    ItemWeapon _weapon;

    private void Start()
    {
        Subscribe();
        _weapon = (ItemWeapon)GetComponent<Inventory>().rightHand._item;
    }

    private void Awake()
    {
        _entityEvents = GetComponent<EntityEvents>();
        _abilityEvents = GetComponent<AbilityEvents>();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Cast(int slot)
    {
        if (_spellSlot == slot)
        {
            Debug.Log("STING");
            GameObject sting = Instantiate(_ability, gameObject.transform.position, gameObject.transform.rotation);

            foreach (GameObject rune in _weapon.runeList)
            {
                IRune temp = (IRune)sting.AddComponent(typeof(IRune));
                temp = rune.GetComponent<IRune>();
            }
            _abilityEvents.Instantiated();
        }
    }

    private void CannotAffordCast(int slot)
    {
        if (_spellSlot == slot)
        {
            Debug.Log("CANNOT AFFORD TO STING");
        }
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
        _entityEvents.TryCastAbilityCostRage(_spellSlot, _abilityCost);
    }

    private void Subscribe()
    {
        _entityEvents.OnCallBackCastAbility += Cast;
        _entityEvents.OnCanNotAffordAbility += CannotAffordCast;
    }

    private void Unsubscribe()
    {
        _entityEvents.OnCallBackCastAbility -= Cast;
        _entityEvents.OnCanNotAffordAbility -= CannotAffordCast;
    }
}
