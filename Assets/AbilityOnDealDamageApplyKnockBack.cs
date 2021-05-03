using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnDealDamageApplyKnockBack : MonoBehaviour
{
    private AbilityEvents _events;
    [SerializeField] float knockBackAmount;

    private void Start()
    {
        Subscribe();
    }

    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Activate(Damage damage, GameObject target)
    {
        if(target.GetComponent<EnemyMovementController>())
        {
            target.GetComponent<EnemyMovementController>().KnockBack(knockBackAmount, gameObject);
        }
    }

    private void Subscribe()
    {
        _events._onDealDamage += Activate;
    }

    private void Unsubscribe()
    {
        _events._onDealDamage -= Activate;
    }
}
