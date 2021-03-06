using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EntityEvents : MonoBehaviour
{
    private bool dead = false;

    //Called when changing runes
    public event Action OnEquipRunesChange;

    //OnHitGameObject
    public event Action<Damage> OnHitThis;

    //Called to add hp to player
    public event Action<int> OnRecoverHealth;

    //Called to add energy to player
    public event Action<int> OnRecoverEnergy;

    //Called to add spirit to player
    public event Action<int> OnRecoverSpirit;

    //Called to add rage to player
    public event Action<int> OnRecoverRage;

    //called to lose health as a cost of ability
    public event Action<int, Damage> OnDeteriorateHealth;

    //called to lose energy as a cost of ability
    public event Action<int> OnDeteriorateEnergy;

    //called to lose spirit as a cost of ability
    public event Action<int> OnDeteriorateSpirit;

    //called to lose health as a cost of ability
    public event Action<int> OnDeteriorateRage;

    public event Action<int> OnSetHealth;
    public event Action<int> OnSetSpirit;
    public event Action<int> OnSetEnergy;


    //OnHitEnemyGameObject
    public event Action<Damage, GameObject> OnHitEnemy;

    //OnKillEnemy
    public event Action<GameObject> OnKillEnemy;

    //These events are called when entity loses these resources. DO NOT CALL THEM TO REDUCE OR ADD THESE STATS!
    public event Action<int> OnLoseSpirit;
    public event Action<int> OnGainSpirit;
    public event Action<int> OnLoseEnergy;
    public event Action<int> OnGainEnergy;
    public event Action<int> OnGainRage;
    public event Action<int> OnLoseRage;
    public event Action<int> OnGainHealth;
    public event Action<int> OnLoseHealth;
    public event Action<float> OnGetCCd;
    public event Action OnCastAbility;
    public event Action OnNormalAttack;

    //Add new Buff for entity
    public event Action<string, EntityStats.BuffType, int, float> OnNewBuff;

    //OnRemoveBuff input is buff source name
    public event Action<string> OnRemoveBuff;

    //OnUpdateBuff called when entity buffs that effect stats change
    public event Action<string> OnUpdateBuff;

    //Called when entity takes damage as a result of enemy attack. DO NOT CALL THEM TO REDUCE OR ADD THESE STATS!
    public event Action<int> OnPhysicalDamageTaken;
    public event Action<int> OnSpiritDamageTaken;


    //Called when game objects health reaches 0
    public event Action<GameObject, GameObject> OnDie;


    //TryCast is listened by corresponding resource scripts. if entity has enough
    //OnCallBackAbility is called so that Ability Manager knows that entity can cast that ability
    public event Action<int, int> OnTryCastAbilityCostHealth;
    public event Action<int, int> OnTryCastAbilityCostSpirit;
    public event Action<int, int> OnTryCastAbilityCostEnergy;
    public event Action<int, int> OnTryCastAbilityCostRage;
    public event Action<int> OnCallBackCastAbility;
    public event Action<int> OnCanNotAffordAbility;


    public event Action<int> OnChangeTeam;
    public event Action OnRemoveFromTeams;

    public event Action<GameObject, int> OnDecreaseAggro;
    public event Action<GameObject, int> OnIncreaseAggro;
    public event Action<GameObject, int> OnSetAggro;

    public event Action<ItemObject> OnAddNewItemToInventory;
    public event Action<int> OnRemoveItem;
    public event Action<ItemObject, int> OnAddNewItemToSlot;
    public event Action<UiButtonClick> OnUseItem;

    public event Action OnAnimationTriggerPoint;
    public event Action OnSpellOver;
    public event Action<GameObject, Damage> OnDealCritDamage;

    public event Action OnLockInventory;
    public event Action OnUnlockInventory;

    public event Action<int> OnTakeStep;
    public event Action<int> OnSetCurrentHealth;

    public event Action OnDash;

    public event Action OnStatChange;

    public event Action<GameObject> OnExecute;

    public event Action<GameObject, Damage> OnBasicAttackHit;

    public event Action<Damage, GameObject> OnPreDamageCalculation;

    public event Action<Damage, GameObject> OnBlock;


    public void Block(Damage damage, GameObject target)
    {
        OnBlock?.Invoke(damage, target);
    }

    public void BasicAttackHit(GameObject enemy, Damage damage)
    {
        Debug.Log(gameObject + "'s basic attack hit");
        OnBasicAttackHit?.Invoke(enemy, damage);
    }

    public void StatChange()
    {
        OnStatChange?.Invoke();
    }

    public void Dash()
    {
        OnDash?.Invoke();
    }

    public void TakeStep(int distance)
    {
        OnTakeStep?.Invoke(distance);
    }
    public void LockInventory(string test = " ")
    {
        OnLockInventory?.Invoke();
    }
    public void UnlockInventory(string test = " ")
    {
        OnUnlockInventory?.Invoke();
    }

    public void DealCritDamage(GameObject target, Damage damage)
    {
        OnDealCritDamage?.Invoke(target, damage);
    }

    public event Action<int> OnGainShield;

    public void GainShield(int amount)
    {
        Debug.Log("Gain shield event");
        OnGainShield?.Invoke(amount);
    }

    public void EquipRunesChange()
    {
        OnEquipRunesChange?.Invoke();
    }

    public void AnimationTriggerPoint()
    {
        OnAnimationTriggerPoint?.Invoke();
    }

    public void UseItem(UiButtonClick invSlot)
    {
        OnUseItem?.Invoke(invSlot);
    }
    public void AddNewItemToSlot(ItemObject item, int slot)
    {
        OnAddNewItemToSlot?.Invoke(item, slot);
    }
    public void RemoveItem(int slot)
    {
        OnRemoveItem?.Invoke(slot);
    }


    public void AddNewItemToInventory(ItemObject item)
    {
        OnAddNewItemToInventory?.Invoke(item);
    }

    public void SetHealth(int value)
    {
        OnSetHealth?.Invoke(value);
    }

    public void SetCurrentHealth(int value)
    {
        OnSetCurrentHealth?.Invoke(value);
    }
    public void SetSpirit(int value)
    {
        OnSetSpirit?.Invoke(value);
    }
    public void SetEnergy(int value)
    {
        OnSetEnergy?.Invoke(value);
    }
    public void TryCastAbilityCostHealth(int spellSlot, int cost)
    {
        OnTryCastAbilityCostHealth?.Invoke(spellSlot, cost);
    }
    public void TryCastAbilityCostSpirit(int spellSlot, int cost)
    {
        OnTryCastAbilityCostSpirit?.Invoke(spellSlot, cost);
    }
    public void TryCastAbilityCostEnergy(int spellSlot, int cost)
    {
        OnTryCastAbilityCostEnergy?.Invoke(spellSlot, cost);
    }
    public void TryCastAbilityCostRage(int spellSlot, int cost)
    {
        OnTryCastAbilityCostRage?.Invoke(spellSlot, cost);
    }
    public void CallBackCastAbility(int spellSlot)
    {
        OnCallBackCastAbility?.Invoke(spellSlot);
    }
    public void CanNotAffordAbility(int spellSlot)
    {
        OnCanNotAffordAbility?.Invoke(spellSlot);
    }
    public void PreDamageCalculation(Damage damage, GameObject target)
    {
        OnPreDamageCalculation?.Invoke(damage, target);
    }
    public void HitThis(Damage damage, bool chainable = true)
    {
        if (chainable) damage.source.GetComponent<EntityEvents>().PreDamageCalculation(damage, gameObject);
        OnHitThis?.Invoke(damage);
        if(chainable) damage.source.GetComponent<EntityEvents>().HitEnemy(damage, gameObject);
    }
    public void RecoverHealth(int amount)
    {
        OnRecoverHealth?.Invoke(amount);
    }
    public void RecoverRage(int amount)
    {
        OnRecoverRage?.Invoke(amount);
    }
    public void RecoverEnergy(int amount)
    {
        OnRecoverEnergy?.Invoke(amount);
    }
    public void RecoverSpirit(int amount)
    {
        OnRecoverSpirit?.Invoke(amount);
    }
    public void DeteriorateHealth(int amount, Damage damage)
    {
        OnDeteriorateHealth?.Invoke(amount, damage);
    }
    public void DeteriorateEnergy(int amount)
    {
        OnDeteriorateEnergy?.Invoke(amount);
    }
    public void DeteriorateRage(int amount)
    {
        OnDeteriorateRage?.Invoke(amount);
    }
    public void DeteriorateSpirit(int amount)
    {
        OnDeteriorateSpirit?.Invoke(amount);
    }
    public void HitEnemy(Damage damage, GameObject target)
    {
        OnHitEnemy?.Invoke(damage, target);
    }
    public void KillEnemy(GameObject enemy)
    {
        OnKillEnemy?.Invoke(enemy);
    }
    public void LoseSpirit(int amount)
    {
        OnLoseSpirit?.Invoke(amount);
    }
    public void GainSpirit(int amount)
    {
        OnGainSpirit?.Invoke(amount);
    }
    public void LoseEnergy(int amount)
    {
        OnLoseEnergy?.Invoke(amount);
    }
    public void GainEnergy(int amount)
    {
        OnGainEnergy?.Invoke(amount);
    }
    public void LoseRage(int amount)
    {
        OnLoseRage?.Invoke(amount);
    }
    public void GainRage(int amount)
    {
        OnGainRage?.Invoke(amount);
    }
    public void LoseHealth(int amount)
    {
        OnLoseHealth?.Invoke(amount);
    }
    public void GainHealth(int amount)
    {
        OnGainHealth?.Invoke(amount);
    }
    public void GetCCd(float time)
    {
        OnGetCCd?.Invoke(time);
    }
    public void CastAbility()
    {
        OnCastAbility?.Invoke();
    }
    public void NormalAttack()
    {
        OnNormalAttack?.Invoke();
    }
    public void NewBuff(string SourceId, EntityStats.BuffType id, int value, float duration = -1)
    {
        Debug.Log(gameObject + " new buff event");
        OnNewBuff?.Invoke(SourceId, id, value, duration);
    }
    public void RemoveBuff(string sourceId)
    {
        OnRemoveBuff?.Invoke(sourceId);
    }
    public void UpdateBuff(string sourceId)
    {
        OnUpdateBuff?.Invoke(sourceId);
    }
    public void PhysicalDamageTaken(int value)
    {
        OnPhysicalDamageTaken?.Invoke(value);
    }
    public void SpiritDamageTaken(int value)
    {
        OnSpiritDamageTaken?.Invoke(value);
    }
    public void ChangeTeam(int team)
    {
        OnChangeTeam?.Invoke(team);
    }
    public void DecreaseAggro(GameObject entity, int amount)
    {
        OnDecreaseAggro?.Invoke(entity, amount);
    }
    public void IncreaseAggro(GameObject entity, int amount)
    {
        OnIncreaseAggro?.Invoke(entity, amount);
    }
    public void SetAggro(GameObject entity, int amount)
    {
        OnSetAggro?.Invoke(entity, amount);
    }

    public void RemoveFromTeam()
    {
        OnRemoveFromTeams?.Invoke();
    }
    public void Die(GameObject source)
    {
        if(!dead)
        {
            dead = true;
            OnDie?.Invoke(source, gameObject);
        }
        
    }

    public void Execute(GameObject killer)
    {
        OnExecute?.Invoke(killer);
    }
}
