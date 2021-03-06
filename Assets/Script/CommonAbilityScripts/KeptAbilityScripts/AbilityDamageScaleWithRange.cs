using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDamageScaleWithRange : MonoBehaviour
{
    private AbilityEvents _events;
    [SerializeField] private float _physicalMultiplier;
    [SerializeField] private float _spiritMultiplier;
    private Damage _damage;
    private AbilityRangeManager _range;

    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
        _range = GetComponent<AbilityRangeManager>();
        //_damage = _events._damage;
    }

    private void Update()
    {
        _damage._damage = (int)( _damage._damage * _physicalMultiplier * _range.GetCurrentRange());
        //_events._damage = _damage;
    }
}
