using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnHitApplyEffect : MonoBehaviour
{
    [SerializeField] private GameObject effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(effect, collision.transform.position, collision.transform.rotation);
    }
}
