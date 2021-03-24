using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRotate : MonoBehaviour
{
    private AbilityEvents _events;
    private Vector2 _targetVector;
    private bool rotating = true;

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
        rotating = false;
    }

    public void CalculateRotation(Vector2 targetVector)
    {
        if(rotating)
        {
            _targetVector = targetVector;
            float angle = Mathf.Atan2(_targetVector.y, _targetVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


}
