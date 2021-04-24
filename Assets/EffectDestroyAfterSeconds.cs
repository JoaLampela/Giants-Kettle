using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] private float destroyDelay;
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
