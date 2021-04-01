using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StingRight : MonoBehaviour, IAbility
{
    EntityEvents _entityEvents;
    AbilityEvents _abilityEvents;
    [SerializeField] private int _spellSlot;
    [SerializeField] private int _abilityCost = 0;
    ItemWeapon _weapon;

    private void Start()
    {
        Subscribe();
        _weapon = (ItemWeapon)GetComponent<Inventory>().rightHand._item;
    }

    private void Awake()
    {
        _entityEvents = GetComponent<EntityEvents>();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Cast(int slot)
    {
        if(_spellSlot == slot)
        {
            Debug.Log("STING");
            GameObject sting = Instantiate(GetComponent<EntityAbilityManager>().sting, gameObject.transform.position, gameObject.transform.rotation);

            Debug.Log("Loop before");
            foreach(GameObject rune in _weapon.runeList)
            {
                Debug.Log("Loop inside");
                MonoBehaviour temp = sting.AddComponent(typeof(MonoBehaviour)) as MonoBehaviour;
                temp = rune.GetComponent<MonoBehaviour>();
            }

            Debug.Log("Loop after");
            sting.GetComponent<AbilityEvents>().Instantiated();
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
