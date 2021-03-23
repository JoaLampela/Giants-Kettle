using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healOnHit : MonoBehaviour
{
    EntityEvents events;


    private void Awake()
    {
        events = GetComponent<EntityEvents>();
    }

    private void Subscribe()
    {
        events.OnPhysicalDamageTaken += Effect;
    }
    private void Unsubscribe()
    {
        events.OnPhysicalDamageTaken -= Effect;
    }

    private void Effect(int amount)
    {
        events.RecoverRage(20);
    }


    // Start is called before the first frame update
    void Start()
    {
        Subscribe();
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
