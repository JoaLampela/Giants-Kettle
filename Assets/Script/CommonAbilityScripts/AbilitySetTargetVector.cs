using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySetTargetVector : MonoBehaviour
{
    private IAbilityTargetPosition _iTargetPosition;
    private Vector2 _targetPosition;
    private AbilityEvents _events;
    private AbilityRotate _abilityRotate;
    private Vector2 _targetVector;
    [SerializeField] private bool _targetPositionUpdating = false;
    [SerializeField] private bool _targetVectorUpdating = false;

    private void Start()
    {
        _iTargetPosition = _events._abilityCastSource.GetComponent<IAbilityTargetPosition>();
        _targetPosition = _iTargetPosition.GetTargetPosition();
        _targetVector = ((Vector2)transform.position - _targetPosition).normalized;

    }

    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
        _abilityRotate = GetComponent<AbilityRotate>();
    }

    private void Update()
    {
        if (_targetVectorUpdating)
        {
            if (_targetPositionUpdating)
            {
                _targetPosition = _iTargetPosition.GetTargetPosition();
            }
            SetTargetVector();
        }
        SetAbilityRotation(_iTargetPosition.GetTargetPosition());
    }

    public void SetAbilityRotation(Vector2 targetPosition)
    {
        _abilityRotate.CalculateRotation(_targetVector);
    }

    public void SetTargetVector()
    {
        _targetVector = ((Vector2)transform.position - _targetPosition).normalized;
    }

    public Vector2 GetTargetVector()
    {
        return _targetVector;
    }
}
