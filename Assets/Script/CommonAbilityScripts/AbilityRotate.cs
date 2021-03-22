using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRotate : MonoBehaviour
{
    private AbilityEvents _events;
    private Vector2 _targetVector;

    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
    }

    public void CalculateRotation(Vector2 targetVector)
    {
        _targetVector = targetVector;
        float angle = Mathf.Atan2(_targetVector.y, _targetVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
