using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnActivateInstantiateEffect : MonoBehaviour
{
    private AbilityEvents _events;
    [SerializeField] private GameObject _effect;

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
        GameObject temp = Instantiate(_effect, transform.position, transform.rotation);
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
