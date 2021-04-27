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
        if (GetComponent<BoxCollider2D>()) GetComponent<BoxCollider2D>().enabled = false;
        else if (GetComponent<CircleCollider2D>()) GetComponent<CircleCollider2D>().enabled = false;
    }
}
