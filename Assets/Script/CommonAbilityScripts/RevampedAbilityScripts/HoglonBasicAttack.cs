using System.Collections;
using UnityEngine;

public class HoglonBasicAttack : MonoBehaviour, IAbility
{
    private IEntityAnimations playerAnimations;
    private MovementScript movementScript;
    private Animator animator;
    EntityEvents _entityEvents;
    EntityAbilityManager abilityManager;
    [SerializeField] private int _spellSlot;
    private IAbilityTargetPosition targetPositionScript;
    private Vector2 targetPosAtStart;
    private float basicAttackCooldown = 2f;
    public bool basicAttackOffCooldown = true;

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
            basicAttackOffCooldown = false;
            targetPosAtStart = targetPositionScript.GetTargetPosition() - (Vector2)transform.position;
            _entityEvents.OnAnimationTriggerPoint += InstatiateHitBox;
            animator.SetTrigger("Attack");
            GetComponent<Rigidbody2D>().AddForce(targetPosAtStart.normalized * 3000f);
            SoundManager.PlaySound(SoundManager.Sound.TwoHandedBasicAttack, transform.position);
            GetComponent<EnemyMovementController>().SetMoveSlow(true);
        }
    }

    private IEnumerator basicAttackCooldownFunction()
    {
        float trueCooldown = basicAttackCooldown * 10f / (10f + GetComponent<EntityStats>().currentAttackSpeed);
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
            else
            {
                if (GameObject.FindGameObjectWithTag("Player"))
                {
                    enemyDirection = GameObject.FindGameObjectWithTag("Player").transform.position;
                }
                else
                {
                    enemyDirection = new Vector2(0, 0);
                }
            }
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
        GameObject basicAttack = Instantiate(GetComponent<EntityAbilityManager>().basicAttackTwoHandedSword, direction + GetComponent<CircleCollider2D>().offset, rotation);
        basicAttack.GetComponent<AbilityEvents>()._targetPositionAtStart = targetPosAtStart;
        basicAttack.GetComponent<AbilityEvents>().SetSource(gameObject);
        basicAttack.GetComponent<AbilityEvents>().UseAbility();
        playerAnimations.SetAttacking(false);
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
        return null;
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
