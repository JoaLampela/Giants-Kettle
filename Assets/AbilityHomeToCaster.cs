using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHomeToCaster : MonoBehaviour
{
    private float _velocity = 0f;
    [SerializeField] float _velocityStart = 0f;
    [SerializeField] float _velocityMax = 0f;
    [SerializeField] float _acceleration = 0f;
    [SerializeField] float _exponentialAcceleration = 0f;
    [Header("0 = static, 1 = accelerating, 2 = exponential")]
    [SerializeField] private int _travelType;
    private bool isAccelerating = false;
    private bool isExponential = false;

    private void Awake()
    {
        SetTravelType();
    }

    private void Start()
    {
        _velocity = _velocityStart;
    }

    private void Update()
    {
        Debug.Log(transform.position);
        Vector2 dir = ((Vector2)GetComponent<AbilityEvents>()._abilityCastSource.transform.position - (Vector2)transform.position).normalized;
        if (isAccelerating)
        {
            if (_velocity > _velocityMax)
            {
                _velocity = _velocityMax;
            }
            else
            {
                _velocity += _acceleration * Time.deltaTime;
            }
        }
        else if (isExponential)
        {
            if (_velocity < _velocityMax)
            {
                if (_velocity + Mathf.Pow(_velocity, _exponentialAcceleration) * Time.deltaTime > _velocityMax)
                {
                    _velocity = _velocityMax;
                }
                else
                {
                    _velocity += Mathf.Pow(_velocity, _exponentialAcceleration) * Time.deltaTime;
                }

            }
            else
            {
                _velocity = _velocityMax;
            }

        }
        gameObject.GetComponent<Rigidbody2D>().velocity = _velocity * dir;

    }
    private void SetTravelType()
    {
        switch (_travelType)
        {
            case 0:
                break;
            case 1:
                isAccelerating = true;
                break;
            case 2:
                isExponential = true;
                break;
        }
    }
}
