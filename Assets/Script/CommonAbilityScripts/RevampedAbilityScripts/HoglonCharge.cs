using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoglonCharge : MonoBehaviour, IAbility
{
    private Hoglon playerAnimations;
    private MovementScript movementScript;
    private Animator animator;
    EntityEvents _entityEvents;
    EntityAbilityManager abilityManager;
    [SerializeField] private int _spellSlot;
    private IAbilityTargetPosition targetPositionScript;
    private Vector2 targetPosAtStart;

    private void Start()
    {
        Subscribe();
    }

    private void Awake()
    {
        playerAnimations = GetComponent<Hoglon>();
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

            animator.SetTrigger("PreCharge");
            SoundManager.PlaySound(SoundManager.Sound.StingLeft, transform.position);

            StartCoroutine(Charge());
        }
    }
    IEnumerator Charge()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("Charging", true);
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

        GameObject sting = Instantiate(GetComponent<EntityAbilityManager>().heavySting, direction + GetComponent<CircleCollider2D>().offset, rotation);
        sting.GetComponent<AbilityEvents>()._targetPositionAtStart = targetPosAtStart;
        sting.GetComponent<AbilityEvents>().SetSource(gameObject);
        sting.GetComponent<AbilityEvents>().UseAbility();
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

    public IAbility.Hand GetHand()
    {
        return IAbility.Hand.indeterminate;
    }
    public Item GetWeapon()
    {
        return null;
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
