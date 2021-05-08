using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBasicAttack : MonoBehaviour
{
    AbilityEvents events;

    private void Awake()
    {
        events = GetComponent<AbilityEvents>();
    }

    private void Start()
    {
        Subscribe();
    }
    private void OnDisable()
    {
        events._onDealDamage -= Activate;
    }

    private void Subscribe()
    {
        events._onDealDamage += Activate;
    }

    private void Activate(Damage damage, GameObject target) 
    {
        damage.source.GetComponent<EntityEvents>().BasicAttackHit(target, damage);
    }
}
