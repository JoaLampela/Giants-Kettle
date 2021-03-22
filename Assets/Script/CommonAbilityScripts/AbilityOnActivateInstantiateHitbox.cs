using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnActivateInstantiateHitbox : MonoBehaviour
{
    private AbilityEvents _events;
    [SerializeField] private GameObject _hitBox;

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
        Instantiate(_hitBox, transform);
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
