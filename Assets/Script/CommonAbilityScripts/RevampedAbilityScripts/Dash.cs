using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour, IAbility
{
    private Player_Animations playerAnimations;
    private MovementScript movementScript;
    private Animator animator;
    EntityEvents _entityEvents;
    [SerializeField] private int _spellSlot;
    private IAbilityTargetPosition targetPositionScript;
    private WeaponObject dashItem;
    private Vector2 targetPosAtStart;
    private float dashTime = 0.10f;
    private Item dashItemContainer;



    private Rigidbody2D rb;


    private void Start()
    {
        Subscribe();
        GetComponent<EntityAbilityManager>().SetAbility(3, this);
        dashItem = GetComponent<Inventory>().dashItem;
        Debug.Log(dashItem.weaponType);
        dashItemContainer = new Item(dashItem);


       
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimations = GetComponent<Player_Animations>();
        movementScript = GetComponent<MovementScript>();
        targetPositionScript = GetComponent<IAbilityTargetPosition>();
        animator = GetComponent<Animator>();
        _entityEvents = GetComponent<EntityEvents>();
    }

    public IEnumerator DashEnumerator(float DashTime)
    {
        Debug.Log("Dash courutine");
        yield return new WaitForSeconds(DashTime);
        rb.AddForce(rb.velocity.normalized * 10000f);

    }


    private void Cast(int slot)
    {
        Debug.Log(slot + " " + dashItemContainer.currentCooldownAbility1);
        if (dashItemContainer.currentCooldownAbility1 <= 0)
        {
            if (_spellSlot == slot)
            {
                dashItemContainer.currentCooldownAbility1 = dashItemContainer.maxCooldownAbility1;
                targetPosAtStart = targetPositionScript.GetTargetPosition() - (Vector2)transform.position;
                _entityEvents.CastAbility();
                InstatiateHitBox();
                
            }
        }
        else CannotAffordCast(slot);
    }

    private void InstatiateHitBox()
    {
        _entityEvents.OnAnimationTriggerPoint -= InstatiateHitBox;
        GameObject dash = Instantiate(GetComponent<EntityAbilityManager>().dash, gameObject.transform.position, gameObject.transform.rotation);
        dash.GetComponent<AbilityEvents>()._targetPositionAtStart = targetPosAtStart;
        dash.GetComponent<AbilityEvents>().SetSource(gameObject);
        dash.GetComponent<AbilityEvents>().UseAbility();
        StartCoroutine(DashEnumerator(dashTime));
        movementScript.StopAttackSlow();
        playerAnimations.SetAttacking(false);
    }

    private void CannotAffordCast(int slot)
    {
        if (_spellSlot == slot)
        {
            Debug.Log("CANNOT AFFORD TO STING");
        }
    }

    public void SetSlot(int slot)
    {
        _spellSlot = slot;
    }

    public void TryCast()
    {
        _entityEvents.TryCastAbilityCostHealth(_spellSlot, 0);
    }

    public int GetCastValue()
    {
        return -1;
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
