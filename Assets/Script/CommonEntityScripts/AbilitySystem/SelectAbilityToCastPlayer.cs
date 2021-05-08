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
        if(!GameObject.Find("Game Manager").GetComponent<GameEventManager>().castingLocked)
        {
            //if (Input.GetKeyDown(KeyCode.LeftShift))
            //{
            //    entityAbilityManager.CastAbility(1);
            //    Debug.Log(" SelectAbility To cast ability 1");
            //}
            //if (Input.GetMouseButtonDown(1))
            //{
            //    entityAbilityManager.CastAbility(2);
            //}
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
           //     entityAbilityManager.CastAbility(3);
            //}
            //if (Input.GetMouseButtonDown(0))
            //{
            //    entityAbilityManager.CastAbility(4);
            //}
            if (Input.GetKey(KeyCode.LeftShift))
            {
                entityAbilityManager.CastAbility(1);
            }
            if (Input.GetMouseButton(1))
            {
                entityAbilityManager.CastAbility(2);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                entityAbilityManager.CastAbility(3);
            }
            if (Input.GetMouseButton(0))
            {
                entityAbilityManager.CastAbility(4);
            }
        }
    }
}
