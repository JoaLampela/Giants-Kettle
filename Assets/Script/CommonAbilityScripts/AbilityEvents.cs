using UnityEngine;
using System;

public class AbilityEvents : MonoBehaviour
{
    public GameObject _abilityCastSource; //Source of the ability
    [HideInInspector] public Vector2 _targetPositionAtStart;
    [HideInInspector] public Vector2 _targetPosition = new Vector2(0, 0);
    [HideInInspector] public Vector2 _targetVector = new Vector2(0, 0);

    public int damageMultiplier;
    public int bonusFlatDamage;
    public int bonusFlatTrueDamage;

    //while summoning copies set this value to false
    public bool firstCopy = true;

    //All different event types for Abilities:
    public event Action _onUseAbility;
    public event Action _onWhiff;
    public event Action<Collider2D> _onHit;
    public event Action _onActivate;
    public event Action _onInstantiated;
    public event Action _onDestroy;

    private void Start()    
    {
        UseAbility();
    }

    //Called when using an ability
    public void UseAbility()
    {
        _onUseAbility?.Invoke();
    }

    //Called when activating the ability's effect
    public void Activate()
    {
        _onActivate?.Invoke();
    }

    //Called when the ability doesn't reach a suitable target and fizzles
    public void Whiff()
    {
        _onWhiff?.Invoke();
    }

    //Called when the ability reaches its' target
    public void Hit(Collider2D collision)
    {
        _onHit?.Invoke(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hit(collision);
    }

    //Sets a source for an ability
    public void SetSource(GameObject source)
    {
        _abilityCastSource = source;
    }

    public void Destroy()
    {
        _onDestroy?.Invoke();
    }


    public void DealDamage(GameObject target, int baseDamage, int trueDamage = 0)
    {
        if(target.GetComponent<EntityStats>())
        {
            if(target.GetComponent<EntityStats>().team != _abilityCastSource.GetComponent<EntityStats>().team)
            {
                if (target.GetComponent<EntityEvents>())
                {
                    Debug.Log("Dealing damage " + baseDamage + " " + bonusFlatDamage + " " + _abilityCastSource.GetComponent<EntityStats>().currentPhysicalDamage + " " + damageMultiplier / 100f);
                    target.GetComponent<EntityEvents>().HitThis(new Damage(_abilityCastSource, (int)((baseDamage + bonusFlatDamage + _abilityCastSource.GetComponent<EntityStats>().currentPhysicalDamage) * damageMultiplier / 100f), trueDamage + bonusFlatTrueDamage));
                }
            }
        }
    }
}
