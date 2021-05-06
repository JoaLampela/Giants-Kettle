using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathEffect : MonoBehaviour
{
    private EntityEvents events;
    [SerializeField] private GameObject effect;

    private void Awake()
    {
        events = GetComponent<EntityEvents>();
    }

    void Start()
    {
        Subscribe();
    }
    private void OnDisable()
    {
        Unsubscribe();
    }

    private void InstantiateEffect(GameObject killer, GameObject entity)
    {
        Instantiate(effect, transform.position, transform.rotation);
    }

    private void Subscribe()
    {
        events.OnDie += InstantiateEffect;
    }
    private void Unsubscribe()
    {
        events.OnDie -= InstantiateEffect;
    }
}
