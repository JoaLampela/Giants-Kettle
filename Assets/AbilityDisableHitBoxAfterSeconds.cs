using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDisableHitBoxAfterSeconds : MonoBehaviour
{
    [SerializeField] private float hitboxDuration;


    private void Start()
    {
        StartCoroutine(DisableHitbox());
    }

    IEnumerator DisableHitbox()
    {
        yield return new WaitForSeconds(hitboxDuration);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
