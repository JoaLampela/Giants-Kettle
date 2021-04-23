using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoomScript : MonoBehaviour
{
    bool activated = false;

    public void StartCombat()
    {
        if (!activated)
        {
            activated = true;
            Debug.Log("Starting combat");
            GetComponent<DoorScript>().CloseDoors();
        }

    }
    public void EndCombat()
    {
        Debug.Log("Ending combat");
        GetComponent<DoorScript>().OpenDoors();
    }

    private void Update()
    {
        EndCombat();
    }
}
