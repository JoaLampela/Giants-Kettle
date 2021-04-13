using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StingRight : MonoBehaviour, IAbility
{
    private Animator animator;
    EntityEvents _entityEvents;
    EntityAbilityManager abilityManager;
    [SerializeField] private int _spellSlot;
    [SerializeField] private int _abilityCost = 10;
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
        if (_spellSlot == slot)
        {
            targetPosAtStart = targetPositionScript.GetTargetPosition() - (Vector2)transform.position;
            Debug.Log(targetPosAtStart);
            _entityEvents.OnAnimationTriggerPoint += InstatiateHitBox;
            Debug.Log("cast right");
            animator.SetTrigger("Special");
            _entityEvents.CastAbility();
            _entityEvents.DeteriorateHealth(_abilityCost);
        }
    }

    private void InstatiateHitBox()
    {
        _entityEvents.OnAnimationTriggerPoint -= InstatiateHitBox;
        //sting.GetComponent<AbilityEvents>().SetSource(gameObject);
        
        //sting.transform.rotation = Quaternion.FromToRotation(transform.position, targetPosAtStart);
        Debug.Log(_weapon._runeList.Length);
        GameObject sting = Instantiate(GetComponent<EntityAbilityManager>().sting, abilityManager.rightHandGameObject.transform.position, abilityManager.rightHandGameObject.transform.rotation);
        sting.GetComponent<AbilityEvents>()._targetPositionAtStart = targetPosAtStart;
        for (int i = 0; i < _weapon._runeList.Length; i++)
        {
            if (_weapon._runeList[i] != null)
            {

                if(!sting.GetComponent( _weapon._runeList[i]._IruneContainer.Result.GetType()))
                {
                    Debug.Log("Adding the script");
                    sting.AddComponent(_weapon._runeList[i]._IruneContainer.Result.GetType());
                    IRuneScript temp = (IRuneScript)sting.GetComponent(_weapon._runeList[i]._IruneContainer.Result.GetType());
                    temp.SetDuplicateCountWeapon(0);
                    Debug.Log("Incrementing from sting " + temp.GetDuplicateCountWeapon());
                }
                IRuneScript runeScript = (IRuneScript)sting.GetComponent(_weapon._runeList[i]._IruneContainer.Result.GetType());
                
                if(_weapon._runeList[i].runeTier == RuneObject.RuneTier.basic)
                {
                    runeScript.IncrementDuplicateCountWeapon();
                }
                else if (_weapon._runeList[i].runeTier == RuneObject.RuneTier.refined)
                {
                    runeScript.IncrementDuplicateCountWeapon();
                    runeScript.IncrementDuplicateCountWeapon();
                }
                else if (_weapon._runeList[i].runeTier == RuneObject.RuneTier.perfected)
                {
                    runeScript.IncrementDuplicateCountWeapon();
                    runeScript.IncrementDuplicateCountWeapon();
                    runeScript.IncrementDuplicateCountWeapon();
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
        _entityEvents.OnAnimationTriggerPoint -= InstatiateHitBox;
    }
}
