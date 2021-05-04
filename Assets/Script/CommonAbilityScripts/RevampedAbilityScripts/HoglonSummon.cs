using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoglonSummon : MonoBehaviour, IAbility
{
    private IEntityAnimations playerAnimations;
    private MovementScript movementScript;
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
    }

    private void Awake()
    {
        playerAnimations = GetComponent<IEntityAnimations>();
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
            _entityEvents.OnAnimationTriggerPoint += InstatiateHitBox;
            playerAnimations.SetAttacking(true);
            animator.SetTrigger("LeftAttack");
            _entityEvents.CastAbility();
        }
    }

    private void InstatiateHitBox()
    {
        Debug.Log("Hoglon summoning");
        _entityEvents.OnAnimationTriggerPoint -= InstatiateHitBox;
        GameObject nonProjectile = Instantiate(GetComponent<EntityAbilityManager>().nonProjectile, gameObject.transform.position, Quaternion.identity);
        nonProjectile.GetComponent<AbilityEvents>()._targetPositionAtStart = targetPosAtStart;
        nonProjectile.GetComponent<AbilityEvents>().SetSource(gameObject);
        nonProjectile.GetComponent<AbilityEvents>().UseAbility();
        playerAnimations.SetAttacking(false);
    }

    private void CannotAffordCast(int slot)
    {
        if (_spellSlot == slot) Debug.Log("NONPROJECTILE ON CD");
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

    public Item GetWeapon()
    {
        return _weapon;
    }

    public IAbility.Hand GetHand()
    {
        return IAbility.Hand.left;
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