using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityActivateOnHit : MonoBehaviour
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
        if (collider.gameObject.GetComponent<EntityStats>())
        {
            if(_events._abilityCastSource != null)
            {
                if (_events._abilityCastSource.GetComponent<EntityStats>().team != collider.gameObject.GetComponent<EntityStats>().team)
                {
                    _events.Activate();
                }
            }
            
        }
        else
        {
            _events.Activate();
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
