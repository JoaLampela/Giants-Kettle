using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPrefab : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGeneration>().ExitLevel();
    }
}
