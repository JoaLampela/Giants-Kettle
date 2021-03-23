using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnActivateInstantiateHitbox : MonoBehaviour
{
    private AbilityEvents _events;
    [SerializeField] private GameObject _hitBox;

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
        //_hitBox.GetComponent<AbilityEvents>().SetSource(_events._abilityCastSource);
        //_hitBox.GetComponent<AbilityEvents>()._damage = new Damage(_events._abilityCastSource, _events._damage.physicalDamage, _events._damage.spiritDamage);
        GameObject temp = Instantiate(_hitBox, transform);
        temp.GetComponent<AbilityEvents>()._damage = _events._damage;
        temp.GetComponent<AbilityEvents>().SetSource(_events._abilityCastSource);
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
