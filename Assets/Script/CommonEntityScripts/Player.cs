using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {
        GetComponent<EntityAbilityManager>().SetAbility(2, GetComponent<AbilityGroundSlam>());
    }
}
