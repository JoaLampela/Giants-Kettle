using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMoveWithSource : MonoBehaviour
{

    private AbilityEvents _events;

    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_events._abilityCastSource != null) transform.position = _events._abilityCastSource.transform.position;
    }
}
