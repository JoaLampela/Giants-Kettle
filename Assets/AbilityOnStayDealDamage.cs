using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnStayDealDamage : MonoBehaviour
{
    private AbilityEvents _events;
    private List<GameObject> hitTargets;
    [SerializeField] private int baseDamage;
    [SerializeField] private int damageScaling;

    private void Start()
    {
        hitTargets = new List<GameObject>();
    }

    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!hitTargets.Contains(collision.gameObject))
        {
            hitTargets.Add(collision.gameObject);
            _events.DealDamage(collision.gameObject, (int)(baseDamage * damageScaling / 100f), 0);
        }
    }
}
