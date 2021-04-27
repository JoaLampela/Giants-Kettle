using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnActivateGiveBuff : MonoBehaviour
{
    private AbilityEvents _events;
    [SerializeField]private EntityStats.BuffType buff;

    [SerializeField] private string buffSource;
    [SerializeField] private float buffDuration;
    [SerializeField] private int buffEffectiveness;

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
        _events._abilityCastSource.GetComponent<EntityEvents>().NewBuff(buffSource, buff, buffEffectiveness, buffDuration);
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
