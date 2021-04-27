using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnActivateReturnToCaster : MonoBehaviour
{
    private AbilityEvents _events;
    [SerializeField] private float delay;
    private bool activated;

    private void Start()
    {
        Subscribe();
    }
    private void Update()
    {
        if(activated)
        {
            Vector2 dir = (_events._abilityCastSource.transform.transform.position - transform.position).normalized;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0,0,angle - 90));
        }
        
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
        activated = true;
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
