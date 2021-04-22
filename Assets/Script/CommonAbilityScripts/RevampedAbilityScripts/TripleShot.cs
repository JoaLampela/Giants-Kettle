using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : MonoBehaviour, IAbility
{
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
        if (_weapon.currentCooldownAbility2 <= 0)
        {
            if (_spellSlot == slot)
            {
                targetPosAtStart = targetPositionScript.GetTargetPosition() - (Vector2)transform.position;
                _entityEvents.OnAnimationTriggerPoint += InstatiateHitBox;

                animator.SetTrigger("Special");

                _entityEvents.CastAbility();
            }
        }
        else CannotAffordCast(slot);
    }

    private void InstatiateHitBox()
    {
        _entityEvents.OnAnimationTriggerPoint -= InstatiateHitBox;
        GameObject tripleShot = Instantiate(GetComponent<EntityAbilityManager>().tripleShot, abilityManager.rightHandGameObject.transform.position, abilityManager.rightHandGameObject.transform.rotation);
        tripleShot.GetComponent<AbilityEvents>()._targetPositionAtStart = targetPosAtStart;
        for (int i = 0; i < _weapon._runeList.Length; i++)
        {
            if (_weapon._runeList[i] != null)
            {

                if (!tripleShot.GetComponent(_weapon._runeList[i]._IruneContainer.Result.GetType()))
                {
                    tripleShot.AddComponent(_weapon._runeList[i]._IruneContainer.Result.GetType());
                    IRuneScript runeScript = (IRuneScript)tripleShot.GetComponent(_weapon._runeList[i]._IruneContainer.Result.GetType());
                    IRuneScript runeScriptOnPlayer = (IRuneScript)GetComponent(_weapon._runeList[i]._IruneContainer.Result.GetType());
                    runeScript.SetDuplicateCountWeapon(runeScriptOnPlayer.GetDuplicateCountWeapon());
                }
            }
        }
        tripleShot.GetComponent<AbilityEvents>().SetSource(gameObject);
        tripleShot.GetComponent<AbilityEvents>().UseAbility();
    }

    private void CannotAffordCast(int slot)
    {
        if (_spellSlot == slot) Debug.Log("CANNOT AFFORD TO TRIPLE SHOT");
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
