using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnDestroyDestroy : MonoBehaviour
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
        Destroy(gameObject);
    }

    private void Subscribe()
    {
        _events._onDestroy += Activate;
    }

    private void Unsubscribe()
    {
        _events._onDestroy -= Activate;
    }
}
