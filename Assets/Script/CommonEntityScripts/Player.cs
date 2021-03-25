using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {
        GetComponent<EntityAbilityManager>().SetAbility(2, GetComponent<AbilityGroundSlam>());
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            //GetComponent<EntityEvents>().NewBuff("test", "invisibility", 1, 5);
            //Debug.Log("invis");
        }
        if (Input.GetMouseButtonDown(1))
        {
            GetComponent<EntityEvents>().ChangeTeam(1);
            Debug.Log("invis");
        }
    }
}
