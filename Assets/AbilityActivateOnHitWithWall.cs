using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityActivateOnHitWithWall : MonoBehaviour
{
    private AbilityEvents _events;
    private bool activated = false;
    [SerializeField] private float activationDelay;
    float time = 0;

    private void Update()
    {
        time += Time.deltaTime;
        if(time > activationDelay && !activated)
        {
            activated = true;
            _events.Activate();
        }
    }

    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!activated && collision.gameObject != _events._abilityCastSource)
        {
            Debug.Log("Collided with " + collision.gameObject);
            activated = true;
            _events.Activate();
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (activated && collision.gameObject == _events._abilityCastSource)
        {
            _events.Destroy();
        }
    }
}
