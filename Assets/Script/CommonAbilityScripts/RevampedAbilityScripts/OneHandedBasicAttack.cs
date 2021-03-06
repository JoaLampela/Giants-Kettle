using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHandedBasicAttack : MonoBehaviour, IAbility
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
    private float basicAttackCooldown = 1f;
    public bool basicAttackOffCooldown = true;

    private void Start()
    {
        Subscribe();
        if (GetComponent<Inventory>()) _weapon = GetComponent<Inventory>().rightHand._item;
        else _weapon = new Item(GetComponent<AiInventory>().rightHandWeapon);

    }

    private void Awake()
    {
        playerAnimations = GetComponent<IEntityAnimations>();
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

        if (basicAttackOffCooldown && !playerAnimations.GetAttacking() && !GetComponent<EntityStats>().isStunned)
        {
            if (_spellSlot == slot)
            {
                basicAttackOffCooldown = false;
                StartCoroutine(basicAttackCooldownFunction());
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
        if (trueCooldown < 0.1f) trueCooldown = 0.1f;
        yield return new WaitForSeconds(trueCooldown);
        basicAttackOffCooldown = true;
    }

    private void InstatiateHitBox()
    {
        _entityEvents.OnAnimationTriggerPoint -= InstatiateHitBox;
        Vector2 direction;
        if (GetComponent<EntityTargetingSystem>())
        {
            Vector2 enemyDirection;
            if (GetComponent<EntityTargetingSystem>().target != null)
            {
                enemyDirection = GetComponent<EntityTargetingSystem>().target.transform.position;
            }
            else enemyDirection = GameObject.FindGameObjectWithTag("Player").transform.position;
            direction = (enemyDirection - (Vector2)transform.position).normalized;
        }
        else
        {
            Vector2 mouseDirection = Input.mousePosition;
            direction = (Camera.main.ScreenToWorldPoint(mouseDirection) - transform.position).normalized;
        }

        float angle = Vector2.Angle(Vector2.up, direction);
        float sign = Mathf.Sign(Vector2.Dot(Vector2.left, direction));
        Quaternion rotation = Quaternion.Euler(0, 0, angle * sign);
        GameObject basicAttack = Instantiate(GetComponent<EntityAbilityManager>().basicAttackOneHandedSword, abilityManager.rightHandGameObject.transform.position, rotation);
        basicAttack.GetComponent<AbilityEvents>()._targetPositionAtStart = targetPosAtStart;
        basicAttack.GetComponent<AbilityEvents>().SetSource(gameObject);
        basicAttack.GetComponent<AbilityEvents>().UseAbility();
        playerAnimations.SetAttacking(false);
        SoundManager.PlaySound(SoundManager.Sound.OneHandedBasicAttack, transform.position);

    }

    private void CannotAffordCast(int slot)
    {
        if (_spellSlot == slot)
        {

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

    public Item GetWeapon()
    {
        return _weapon;
    }

    public IAbility.Hand GetHand()
    {
        return IAbility.Hand.indeterminate;
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

