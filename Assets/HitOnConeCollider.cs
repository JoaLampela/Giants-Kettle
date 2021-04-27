using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitOnConeCollider : MonoBehaviour
{
    [SerializeField] private float coneReachWidth;
    [SerializeField] private float coneReach;
    private Collider2D collider2d;
    private AbilityEvents _events;
    private List<GameObject> hitTargets;


    private void Awake()
    {
        collider2d = GetComponent<CircleCollider2D>();
        _events = GetComponent<AbilityEvents>();
    }

    private void Start()
    {
        hitTargets = new List<GameObject>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!hitTargets.Contains(collision.gameObject))
        {
            if(Mathf.Abs(Vector2.Angle((collision.gameObject.transform.position - transform.position), transform.forward)) < coneReachWidth)
            {
                hitTargets.Add(collision.gameObject);
                _events.DealDamage(collision.gameObject, 0, 0);
            }   
        }
    }
}
