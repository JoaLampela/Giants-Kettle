using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHandedBasicAttack : MonoBehaviour, IAbility
{
    private Player_Animations playerAnimations;
    private MovementScript movementScript;
    private Animator animator;
    EntityEvents _entityEvents;
    EntityAbilityManager abilityManager;
    [SerializeField] private int _spellSlot;
    private IAbilityTargetPosition targetPositionScript;
    Item _weapon;
    private Vector2 targetPosAtStart;
    private float basicAttackCooldown;
    private bool basicAttackOffCooldown = true;

    private void Start()
    {
        Subscribe();
        if (GetComponent<Inventory>()) _weapon = GetComponent<Inventory>().rightHand._item;
        else _weapon = new Item(GetComponent<AiInventory>().rightHandWeapon);

    }

    private void Awake()
    {
        playerAnimations = GetComponent<Player_Animations>();
        movementScript = GetComponent<MovementScript>();
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
        if (basicAttackOffCooldown)
        {
            if (_spellSlot == slot)
            {
                basicAttackOffCooldown = false;
                basicAttackCooldownFunction();
                targetPosAtStart = targetPositionScript.GetTargetPosition() - (Vector2)transform.position;
                _entityEvents.OnAnimationTriggerPoint += InstatiateHitBox;
                playerAnimations.SetAttacking(true);
                animator.SetTrigger("Attack");
            }
        }
        else CannotAffordCast(slot);
    }

    private IEnumerator basicAttackCooldownFunction()
    {
        float trueCooldown = basicAttackCooldown * 100f / (100f + GetComponent<EntityStats>().currentAttackSpeed);
        yield return new WaitForSeconds(trueCooldown);
        basicAttackOffCooldown = true;
    }

    private void InstatiateHitBox()
    {
        _entityEvents.OnAnimationTriggerPoint -= InstatiateHitBox;
        GameObject basicAttack = Instantiate(GetComponent<EntityAbilityManager>().basicAttackOneHandedSword, abilityManager.rightHandGameObject.transform.position, abilityManager.rightHandGameObject.transform.rotation);
        basicAttack.GetComponent<AbilityEvents>()._targetPositionAtStart = targetPosAtStart;
        basicAttack.GetComponent<AbilityEvents>().SetSource(gameObject);
        basicAttack.GetComponent<AbilityEvents>().UseAbility();
        playerAnimations.SetAttacking(false);
    }

    private void CannotAffordCast(int slot)
    {
        if (_spellSlot == slot)
        {
            Debug.Log("BASIC ATTACK IS ON COOLDOWN");
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

