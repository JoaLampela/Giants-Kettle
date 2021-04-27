using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sting2Handed : MonoBehaviour, IAbility
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
        if (_weapon.currentCooldownAbility1 <= 0)
        {
            if (_spellSlot == slot)
            {
                targetPosAtStart = targetPositionScript.GetTargetPosition() - (Vector2)transform.position;
               _weapon.currentCooldownAbility1 = _weapon.maxCooldownAbility1 * 100f/(100f + GetComponent<EntityStats>().currentSpellHaste);
                _entityEvents.OnAnimationTriggerPoint += InstatiateHitBox;
                movementScript.StartAttackSlow();
                playerAnimations.SetAttacking(true);
                animator.SetTrigger("Special");
                _entityEvents.CastAbility();
            }
        }
        else CannotAffordCast(slot);
    }

    private void InstatiateHitBox()
    {
        Vector2 direction;
        if (GetComponent<EntityTargetingSystem>())
        {
            Vector2 enemyDirection;
            if (GetComponent<EntityTargetingSystem>().target != null)
            {
                enemyDirection = GetComponent<EntityTargetingSystem>().target.transform.position;
            }
            else enemyDirection = GameObject.Find("Player").transform.position;
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
        _entityEvents.OnAnimationTriggerPoint -= InstatiateHitBox;
        GameObject sting = Instantiate(GetComponent<EntityAbilityManager>().heavySting, transform.position, rotation);
        sting.GetComponent<AbilityEvents>()._targetPositionAtStart = targetPosAtStart;
        for (int i = 0; i < _weapon._runeList.Length; i++)
        {
            if (_weapon._runeList[i] != null)
            {
                if (!sting.GetComponent(_weapon._runeList[i]._IruneContainer.Result.GetType()))
                {
                    sting.AddComponent(_weapon._runeList[i]._IruneContainer.Result.GetType());
                    IRuneScript runeScript = (IRuneScript)sting.GetComponent(_weapon._runeList[i]._IruneContainer.Result.GetType());
                    IRuneScript runeScriptOnPlayer = (IRuneScript)GetComponent(_weapon._runeList[i]._IruneContainer.Result.GetType());
                    runeScript.SetDuplicateCountWeapon(runeScriptOnPlayer.GetDuplicateCountWeapon());
                }
            }
        }
        sting.GetComponent<AbilityEvents>().SetSource(gameObject);
        sting.GetComponent<AbilityEvents>().UseAbility();

        movementScript.AttackStep(1000);
        playerAnimations.SetAttacking(false);
        movementScript.StopAttackSlow();
    }

    private void CannotAffordCast(int slot)
    {
        if (_spellSlot == slot)
        {
            Debug.Log("CANNOT AFFORD TO HEAVY STING " + _weapon.currentCooldownAbility1);
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
        return IAbility.Hand.right;
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
