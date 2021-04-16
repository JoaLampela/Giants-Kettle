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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            entityAbilityManager.CastAbility(1);
        }
        if (Input.GetMouseButtonDown(1))
        {
            entityAbilityManager.CastAbility(2);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            entityAbilityManager.CastAbility(3);
        }
        if(Input.GetMouseButtonDown(0))
        {
            entityAbilityManager.CastAbility(4);
        }
        
    }
}
