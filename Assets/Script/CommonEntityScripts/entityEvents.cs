using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EntityEvents : MonoBehaviour
{

    public event Action OnStartStatsSet;

    //OnHitGameObject
    public event Action<Damage> OnHitThis;

    public event Action<int> OnRecoverHealth;
    public event Action<int> OnRecoverEnergy;

    public event Action<int> OnRecoverSpirit;

    public event Action<int> OnRecoverRage;

    public event Action<int> OnDeteriorateHealth;
    public event Action<int> OnDeteriorateEnergy;
    public event Action<int> OnDeteriorateSpirit;
    public event Action<int> OnDeteriorateRage;



    //OnHitEnemyGameObject
    public event Action<Damage> OnHitEnemy;

    //OnKillEnemy
    public event Action OnKillEnemy;

    //OnLoseSpirit
    public event Action<int> OnLoseSpirit;

    //OnGainSpirit
    public event Action<int> OnGainSpirit;

    //OnLoseEnergy
    public event Action<int> OnLoseEnergy;

    //OnGainEnergy
    public event Action<int> OnGainEnergy;

    //OnGainRage
    public event Action<int> OnGainRage;

    //OnLoseRage
    public event Action<int> OnLoseRage;

    //OnGainHealth
    public event Action<int> OnGainHealth;

    //OnLoseHealth
    public event Action<int> OnLoseHealth;

    //OnGetCCd
    public event Action<float> OnGetCCd;

    //OnCastAbility
    public event Action OnCastAbility;

    //OnNormalAttack
    public event Action OnNormalAttack;

    //OnNewBuff
    public event Action<string, string, int, float> OnNewBuff;

    //OnRemoveBuff
    public event Action<string> OnRemoveBuff;

    //OnUpdateBuff
    public event Action<string> OnUpdateBuff;



    public event Action<int> OnPhysicalDamageTaken;
    public event Action<int> OnSpiritDamageTaken;

    public event Action OnDie;



    public event Action<int, int> OnTryCastAbilityCostHealth;
    public event Action<int, int> OnTryCastAbilityCostSpirit;
    public event Action<int, int> OnTryCastAbilityCostEnergy;
    public event Action<int, int> OnTryCastAbilityCostRage;
    public event Action<int> OnCallBackCastAbility;
    public event Action<int> OnCanNotAffordAbility;


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
    public void CallBackCastAbility(int spellSlot) {
        OnCallBackCastAbility?.Invoke(spellSlot);
    }
    public void CanNotAffordAbility(int spellSlot)
    {
        OnCanNotAffordAbility?.Invoke(spellSlot);
    }



    public void StartStatsSet()
    {
        OnStartStatsSet?.Invoke();
    }
    public void HitThis(Damage damage)
    {
        OnHitThis?.Invoke(damage);
    }
    public void Heal(int amount)
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
    public void DeteriorateHealth(int amount)
    {
        OnDeteriorateHealth?.Invoke(amount);
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
    public void HitEnemy(Damage damage)
    {
        OnHitEnemy?.Invoke(damage);
    }
    public void KillEnemy()
    {
        OnKillEnemy?.Invoke();
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
    public void NewBuff(string SourceId, string id, int value, float duration = -1)
    {
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
    public void Die()
    {
        OnDie?.Invoke();
    }
}
