using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnWhiffActivate : MonoBehaviour
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

    private void Activate()
    {
        _events.Activate();
    }

    private void Subscribe()
    {
        _events._onWhiff += Activate;
    }

    private void Unsubscribe()
    {
        _events._onWhiff -= Activate;
    }
}
