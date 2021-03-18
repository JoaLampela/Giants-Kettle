using UnityEngine;
using System;

public class entityEvents : MonoBehaviour
{
    //OnHitGameObject
    public event Action<Damage> OnHitThis;

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

    public void HitThis(Damage damage)
    {
        OnHitThis?.Invoke(damage);
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
}
