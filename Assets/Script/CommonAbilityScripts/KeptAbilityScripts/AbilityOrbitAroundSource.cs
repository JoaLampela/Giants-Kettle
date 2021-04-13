using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOrbitAroundSource : MonoBehaviour
{
    private AbilityEvents _events;
    private GameObject _target;
    [SerializeField] private float _orbitDistance;
    [SerializeField] private float _orbitSpeed;
    
    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
        
    }
    private void Start()
    {
        Subscribe();
    }
    private void Activate()
    {
        _target = _events._abilityCastSource;
    }

    private void Subscribe()
    {
        _events._onUseAbility += Activate;
    }
    private void Unsubscribe()
    {
        _events._onUseAbility -= Activate;
    }

    private void LateUpdate()
    {
        if (_target != null)
        {
            Debug.Log(_orbitSpeed * Time.deltaTime + " " + _target.transform.position);
            transform.RotateAround(_target.transform.position, Vector3.forward, _orbitSpeed * Time.deltaTime);
        }
        else
        {
            _target = _events._abilityCastSource;
        }
    }
    private void OnDisable()
    {
        Unsubscribe();
    }
}
