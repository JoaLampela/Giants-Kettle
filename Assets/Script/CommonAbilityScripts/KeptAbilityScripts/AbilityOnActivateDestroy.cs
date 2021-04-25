using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnActivateDestroy : MonoBehaviour
{
    private AbilityEvents _events;
    [SerializeField] private float delay;

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

    private void Activate()
    {
        StartCoroutine(DestroyWithDelay());
    }
    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(delay);
        _events.Destroy();
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
