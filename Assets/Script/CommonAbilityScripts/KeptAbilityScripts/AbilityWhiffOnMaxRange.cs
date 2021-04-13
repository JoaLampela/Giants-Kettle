using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityWhiffOnMaxRange : MonoBehaviour
{
    private AbilityEvents _events;
    [SerializeField] private float _maxRange = 0f;
    private AbilityRangeManager _range;
    private bool _whiffed = false;

    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
        _range = GetComponent<AbilityRangeManager>();
    }

    private void Update()
    {
        if(_range.GetCurrentRange() > _maxRange && !_whiffed)
        {
            _whiffed = true;
            _events.Whiff();
        }
    }
}
