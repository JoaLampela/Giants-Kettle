using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnStayDealDamage : MonoBehaviour
{
    private AbilityEvents _events;
    private List<GameObject> hitTargets;
    [SerializeField] private int baseDamage;

    private void Start()
    {
        Subscribe();
        hitTargets = new List<GameObject>();
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
        GameObject castSource = _events._abilityCastSource;

        if (collider.gameObject.GetComponent<EntityStats>())
        {
            if (collider.gameObject.GetComponent<EntityStats>().team != castSource.GetComponent<EntityStats>().team)
            {
                collider.gameObject.GetComponent<EntityEvents>().HitThis(_events._damage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!hitTargets.Contains(collision.gameObject))
        {
            hitTargets.Add(collision.gameObject);
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
