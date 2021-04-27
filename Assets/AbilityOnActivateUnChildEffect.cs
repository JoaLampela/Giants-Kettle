using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnActivateUnChildEffect : MonoBehaviour
{
    private AbilityEvents _events;
    public GameObject _effect;

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
        if(_effect != null)
        {
            if (_effect.GetComponent<ParticleSystem>())
            {
                _effect.GetComponent<ParticleSystem>().Stop();
                _effect.transform.parent = null;
            }
        }
        
       
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
