using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnHitDealDamage : MonoBehaviour
{
    private AbilityEvents _events;

    [SerializeField] private float _levelBonusMultiplier = 1f;

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

    private void Activate(Collider2D collider)
    {
        GameObject castSource = _events._abilityCastSource;

        if (collider.gameObject.GetComponent<EntityStats>())
        {
            if(collider.gameObject.GetComponent<EntityStats>().team != castSource.GetComponent<EntityStats>().team)
            {
                collider.gameObject.GetComponent<EntityEvents>().HitThis(_events._damage);
            }
        }
    }

    private void Subscribe()
    {
        _events._onHit += Activate;
    }

    private void Unsubscribe()
    {
        _events._onHit -= Activate;
    }
}
