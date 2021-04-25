using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnActivateDestroyVisuals : MonoBehaviour
{
    private AbilityEvents _events;
    [SerializeField] private GameObject asset1;
    [SerializeField] private float delay1;
    [SerializeField] private GameObject asset2;
    [SerializeField] private float delay2;
    [SerializeField] private GameObject asset3;
    [SerializeField] private float delay3;

    [SerializeField] private bool disableCollider = false;

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
        if (asset1 != null) StartCoroutine(DestroyWithDelay(asset1, delay1));
        if (asset2 != null) StartCoroutine(DestroyWithDelay(asset2, delay2));
        if (asset3 != null) StartCoroutine(DestroyWithDelay(asset3, delay3));
        if (GetComponent<Collider2D>()) GetComponent<Collider2D>().enabled = false;
    }

    private IEnumerator DestroyWithDelay(GameObject asset, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(asset);
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
