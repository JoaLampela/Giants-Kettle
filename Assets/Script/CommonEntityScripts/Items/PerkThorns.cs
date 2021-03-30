using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkThorns : MonoBehaviour, IPerk
{
    EntityEvents events;

    private void Awake()
    {
        events = GetComponent<EntityEvents>();
    }

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        events.OnHitThis += Activate;
    }
    private void Unsubscribe()
    {
        events.OnHitThis -= Activate;
    }
    private void Activate(Damage damage)
    {
        damage.source.GetComponent<EntityEvents>().HitThis(new Damage(gameObject, 0, (int)(damage.physicalDamage * 0.5f)));
    }
    private void OnDisable()
    {
        Unsubscribe();
    }
}
