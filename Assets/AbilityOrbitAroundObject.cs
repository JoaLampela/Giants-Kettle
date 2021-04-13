using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOrbitAroundObject : MonoBehaviour
{
    private AbilityEvents _events;
    private GameObject _target;
    [SerializeField] private float _orbitDistance;
    [SerializeField] private float _orbitSpeed;

    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
        _target = transform.parent.gameObject;
    }

    
}
