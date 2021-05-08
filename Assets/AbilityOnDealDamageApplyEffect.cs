using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnDealDamageApplyEffect : MonoBehaviour
{
    private AbilityEvents abilityEvents;
    [SerializeField] private GameObject effect;

    private void Awake()
    {
        abilityEvents = GetComponent<AbilityEvents>();
    }

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        abilityEvents._onDealDamage += Activate;
    }

    private void Unsubscribe()
    {
        abilityEvents._onDealDamage -= Activate;
    }

    private void Activate(Damage damage, GameObject target)
    {
        Instantiate(effect, target.transform.position, target.transform.rotation);
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
}
