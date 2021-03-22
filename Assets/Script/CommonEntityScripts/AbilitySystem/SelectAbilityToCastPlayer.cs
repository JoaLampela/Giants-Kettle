using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAbilityToCastPlayer : MonoBehaviour
{
    EntityAbilityManager entityAbilityManager;

    private void Awake()
    {
        entityAbilityManager = GetComponent<EntityAbilityManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            entityAbilityManager.CastAbility(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            entityAbilityManager.CastAbility(2);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            entityAbilityManager.CastAbility(3);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            entityAbilityManager.CastAbility(4);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            entityAbilityManager.CastAbility(5);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            entityAbilityManager.CastAbility(6);
        }
        
    }
}
