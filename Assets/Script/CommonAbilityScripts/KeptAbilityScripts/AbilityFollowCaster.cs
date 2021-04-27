using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFollowCaster : MonoBehaviour
{
    private AbilityEvents _events;
    private bool following = true;

    private void Start()
    {
        Subscribe();
    }

    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
    }

    private void Update()
    {
        if(following && _events._abilityCastSource != null)
        {
            gameObject.transform.position = _events._abilityCastSource.transform.position;
        }
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        _events._onActivate += Activate;
    }

    private void Unsubscribe()
    {
        _events._onActivate -= Activate;
    }

    private void Activate()
    {
        following = false;
    }
}
