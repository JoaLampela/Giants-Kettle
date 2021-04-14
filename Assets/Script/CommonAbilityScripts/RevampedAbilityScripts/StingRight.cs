using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StingRight : MonoBehaviour, IAbility
{
    private Animator animator;
    EntityEvents _entityEvents;
    EntityAbilityManager abilityManager;
    [SerializeField] private int _spellSlot;
    private IAbilityTargetPosition targetPositionScript;
    Item _weapon;
    private Vector2 targetPosAtStart;

    private void Start()
    {
        Subscribe();
        _weapon = GetComponent<Inventory>().rightHand._item;
    }

    private void Awake()
    {
        abilityManager = GetComponent<EntityAbilityManager>();
        targetPositionScript = GetComponent<IAbilityTargetPosition>();
        animator = GetComponent<Animator>();
        _entityEvents = GetComponent<EntityEvents>();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Cast(int slot)
    {
        if (_weapon.currentCooldownAbility1 <= 0)
        {
            if (_spellSlot == slot)
            {
                _weapon.currentCooldownAbility1 = _weapon.maxCooldownAbility1;
                targetPosAtStart = targetPositionScript.GetTargetPosition() - (Vector2)transform.position;
                _entityEvents.OnAnimationTriggerPoint += InstatiateHitBox;
                Debug.Log("cast right");
                animator.SetTrigger("Special");
                _entityEvents.CastAbility();
            }
        }
        else CannotAffordCast(slot);
    }

    private void InstatiateHitBox()
    {
        _entityEvents.OnAnimationTriggerPoint -= InstatiateHitBox;
        Debug.Log(_weapon._runeList.Length);
        GameObject sting = Instantiate(GetComponent<EntityAbilityManager>().sting, abilityManager.rightHandGameObject.transform.position, abilityManager.rightHandGameObject.transform.rotation);
        sting.GetComponent<AbilityEvents>()._targetPositionAtStart = targetPosAtStart;
        for (int i = 0; i < _weapon._runeList.Length; i++)
        {
            if (_weapon._runeList[i] != null)
            {

                if(!sting.GetComponent( _weapon._runeList[i]._IruneContainer.Result.GetType()))
                {
                    sting.AddComponent(_weapon._runeList[i]._IruneContainer.Result.GetType());
                }
                IRuneScript runeScript = (IRuneScript)sting.GetComponent(_weapon._runeList[i]._IruneContainer.Result.GetType());
                
                if(_weapon._runeList[i].runeTier == RuneObject.RuneTier.basic)
                {
                    runeScript.IncrementDuplicateCountWeapon(1);
                }
                else if (_weapon._runeList[i].runeTier == RuneObject.RuneTier.refined)
                {
                    runeScript.IncrementDuplicateCountWeapon(2);
                }
                else if (_weapon._runeList[i].runeTier == RuneObject.RuneTier.perfected)
                {
                    runeScript.IncrementDuplicateCountWeapon(3);
                }
            }
        }
        sting.GetComponent<AbilityEvents>().SetSource(gameObject); 
        sting.GetComponent<AbilityEvents>().UseAbility();
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
        _entityEvents.TryCastAbilityCostHealth(_spellSlot, 0);
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
        _entityEvents.OnAnimationTriggerPoint -= InstatiateHitBox;
    }
}
