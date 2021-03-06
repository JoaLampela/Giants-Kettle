using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IAbility
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

    private void Start()
    {
        Subscribe();
        if (GetComponent<Inventory>()) _weapon = GetComponent<Inventory>().leftHand._item;
        else _weapon = new Item(GetComponent<AiInventory>().leftHandWeapon);
    }

    private void Awake()
    {
        playerAnimations = GetComponent<Player_Animations>();
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
        
        if (_weapon.currentCooldownAbility2 <= 0)
        {
            if (_spellSlot == slot)
            {
                _weapon.currentCooldownAbility2 = _weapon.maxCooldownAbility2 * 100f / (100f + GetComponent<EntityStats>().currentSpellHaste);
                targetPosAtStart = targetPositionScript.GetTargetPosition() - (Vector2)transform.position;
                _entityEvents.OnAnimationTriggerPoint += InstatiateHitBox;
                playerAnimations.SetAttacking(true);
                animator.SetTrigger("LeftAttack");
                _entityEvents.CastAbility();
            }
        }
        else CannotAffordCast(slot);


    }

    private void InstatiateHitBox()
    {
        Debug.Log("Instantiating block");
        _entityEvents.OnAnimationTriggerPoint -= InstatiateHitBox;
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
        GameObject block = Instantiate(GetComponent<EntityAbilityManager>().block, (Vector2)transform.position + direction * 2, rotation);
        block.GetComponent<AbilityEvents>()._targetPositionAtStart = targetPosAtStart;
        for (int i = 0; i < _weapon._runeList.Length; i++)
        {
            if (_weapon._runeList[i] != null)
            {

                if (!block.GetComponent(_weapon._runeList[i]._IruneContainer.Result.GetType()))
                {
                    block.AddComponent(_weapon._runeList[i]._IruneContainer.Result.GetType());
                    IRuneScript runeScript = (IRuneScript)block.GetComponent(_weapon._runeList[i]._IruneContainer.Result.GetType());
                    IRuneScript runeScriptOnPlayer = (IRuneScript)GetComponent(_weapon._runeList[i]._IruneContainer.Result.GetType());
                    runeScript.SetDuplicateCountWeapon(runeScriptOnPlayer.GetDuplicateCountWeapon());
                }
            }
        }
        block.GetComponent<AbilityEvents>().SetSource(gameObject);
        block.GetComponent<AbilityEvents>().UseAbility();
        playerAnimations.SetAttacking(false);
        
    }

    private void CannotAffordCast(int slot)
    {
        if (_spellSlot == slot) Debug.Log("SHIELD SLAM ON CD");
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
