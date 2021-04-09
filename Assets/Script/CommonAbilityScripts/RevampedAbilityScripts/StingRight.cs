using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StingRight : MonoBehaviour, IAbility
{
    EntityEvents _entityEvents;
    [SerializeField] private int _spellSlot;
    [SerializeField] private int _abilityCost = 10;
    Item _weapon;

    private void Start()
    {
        Subscribe();
        _weapon = GetComponent<Inventory>().rightHand._item;
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
        if (_spellSlot == slot)
        {
            Debug.Log("cast right");
            _entityEvents.CastAbility();
            _entityEvents.DeteriorateHealth(_abilityCost);
            GameObject sting = Instantiate(GetComponent<EntityAbilityManager>().sting, gameObject.transform.position, gameObject.transform.rotation);
            sting.GetComponent<AbilityEvents>().SetSource(gameObject);
            
            for (int i = 0; i < _weapon._runeList.Length; i ++)
            {
                if(_weapon._runeList[i] != null)
                {
                    _weapon._runeList[i]._IruneContainer.Result.SetAbilityEvents(sting.GetComponent<AbilityEvents>());
                    _weapon._runeList[i]._IruneContainer.Result.SetProjectile(sting.gameObject);
                    _weapon._runeList[i]._IruneContainer.Result.SubscribeAbility();

                }
            }
            sting.GetComponent<AbilityEvents>().UseAbility();
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
        _entityEvents.TryCastAbilityCostHealth(_spellSlot, _abilityCost);
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
