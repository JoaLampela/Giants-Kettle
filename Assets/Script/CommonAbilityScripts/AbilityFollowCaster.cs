using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFollowCaster : MonoBehaviour
{
    private AbilityEvents _events;

    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
    }

    private void Update()
    {
        gameObject.transform.position = _events._abilityCastSource.transform.position;
    }
}
