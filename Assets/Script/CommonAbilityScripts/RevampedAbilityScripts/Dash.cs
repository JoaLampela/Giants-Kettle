using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour, IAbility
{
    private Animator animator;
    EntityEvents _entityEvents;
    [SerializeField] private int _spellSlot;
    private IAbilityTargetPosition targetPositionScript;
    private Vector2 targetPosAtStart;
    private float dashTime = 0.10f;
    private Item dashItemContainer;



    private Rigidbody2D rb;


    private void Start()
    {
        Subscribe();
        GetComponent<EntityAbilityManager>().SetAbility(3, this);




    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (dashItemContainer == null)
        {
            if (GetComponent<Inventory>()) dashItemContainer = GetComponent<Inventory>().dashItem;
            else if (GetComponent<AiInventory>()) dashItemContainer = GetComponent<AiInventory>().dashItem;
        }
        if (dashItemContainer.currentCooldownAbility1 <= 0)
        {
            if (_spellSlot == slot)
            {
                animator.SetTrigger("Dash");
                _entityEvents.Dash();
                _entityEvents.OnAnimationTriggerPoint += InstatiateHitBox;
                dashItemContainer.currentCooldownAbility1 = dashItemContainer.maxCooldownAbility1;
                targetPosAtStart = targetPositionScript.GetTargetPosition() - (Vector2)transform.position;
                _entityEvents.CastAbility();

                SoundManager.PlaySound(SoundManager.Sound.Dash, transform.position);
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
