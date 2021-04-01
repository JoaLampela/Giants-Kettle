using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAssigner : MonoBehaviour
{
    private EntityAbilityManager _abilityManager;

    private void Start()
    {
        _abilityManager = GetComponent<EntityAbilityManager>();
        //_abilityManager.SetAbility(2, GetComponent<StingRight>());
    }
}
