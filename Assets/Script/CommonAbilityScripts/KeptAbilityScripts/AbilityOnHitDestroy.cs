using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnHitDestroy : MonoBehaviour
{
    private AbilityEvents _events;

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
        if(collider.gameObject.GetComponent<EntityStats>())
        {
            if (_events._abilityCastSource.GetComponent<EntityStats>().team != collider.gameObject.GetComponent<EntityStats>().team)
            {
                _events.Destroy();
            }
        }
        else
        {
            _events.Destroy();
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
