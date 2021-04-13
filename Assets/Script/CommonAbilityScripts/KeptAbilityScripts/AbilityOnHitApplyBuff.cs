using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnHitApplyBuff : MonoBehaviour
{
    private AbilityEvents _events;

    [SerializeField] private string _sourceID;
    [SerializeField] private EntityStats.BuffType _effectID;
    [SerializeField] private int _effectiveness;
    [SerializeField] private float _duration;

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

    private void Activate(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<EntityEvents>())
        {
            collider.gameObject.GetComponent<EntityEvents>().NewBuff(_sourceID, _effectID, _effectiveness, _duration);
        }
    }

    private void Subscribe()
    {
        _events._onHit += Activate;
    }

    private void Unsubscribe()
    {
        _events._onHit -= Activate;
    }
}
