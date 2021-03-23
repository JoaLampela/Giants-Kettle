using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemySpawn : MonoBehaviour
{
    [Range(1, 100)]
    public int[] spawnPrecentage;
    public GameObject[] spawnEnemiesList;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (GameObject enemy in spawnEnemiesList)
        {
            if (Random.Range(1, 100) < spawnPrecentage[i])
                GameObject.Instantiate(enemy, transform.position, Quaternion.identity);
            i++;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
