using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnActivateRetarget : MonoBehaviour
{
    private AbilityEvents _events;
    private AbilitySetTargetVector _targetVector;

    private void Start()
    {
        Subscribe();
    }

    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
        _targetVector = GetComponent<AbilitySetTargetVector>();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Activate()
    {
        _targetVector.SetTargetVector();
    }

    private void Subscribe()
    {
        _events._onActivate += Activate;
    }

    private void Unsubscribe()
    {
        _events._onActivate -= Activate;
    }
}
