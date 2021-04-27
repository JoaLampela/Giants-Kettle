using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityActivateOnCollisionWithCaster : MonoBehaviour
{
    private AbilityEvents _events;
    private bool activated = false;
    
    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!activated && collision.gameObject == _events._abilityCastSource)
        {
            activated = true;
            _events.Activate();
        }
    }
}
