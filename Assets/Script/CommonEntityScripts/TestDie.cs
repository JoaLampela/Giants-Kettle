using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDie : MonoBehaviour
{
    EntityEvents events;
    GameEventManager gameManager;

    private void Awake()
    {
        events = GetComponent<EntityEvents>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        events.OnDie += Die;
    }

    private void Unsubscribe()
    {
        events.OnDie -= Die;
    }
    

    private void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
}
