using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnHitApplyEffect : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    AbilityEvents abilityEvents;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        abilityEvents = GetComponent<AbilityEvents>();
        if(collision.gameObject != abilityEvents._abilityCastSource)
            Instantiate(effect, abilityEvents._abilityCastSource.transform.position, abilityEvents._abilityCastSource.transform.rotation);
    }
}
