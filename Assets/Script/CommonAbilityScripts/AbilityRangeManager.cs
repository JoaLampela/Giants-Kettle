using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRangeManager : MonoBehaviour
{
    private Vector2 _startingPosition;

    private void Awake()
    {
        ResetRange();
    }

    public float GetCurrentRange()
    {
        return Vector2.Distance(_startingPosition, gameObject.transform.position);
    }

    public void ResetRange()
    {
        _startingPosition = gameObject.transform.position;
    }
}
