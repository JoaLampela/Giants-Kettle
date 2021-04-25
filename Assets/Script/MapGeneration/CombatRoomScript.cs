using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoomScript : MonoBehaviour
{

    public int waveEnemyAmount = 4;
    public int wavesLeft = 3;
    public List<GameObject> roomEnemies;
    public List<GameObject> spawnPoints;


    private bool activated = false;
    private bool inCombat = false;
    public void StartCombat()
    {

        if (!activated && !inCombat)
        {
            inCombat = true;
            activated = true;
            Debug.Log("Starting combat");
            GetComponent<DoorScript>().CloseDoors();
            for (int i = 0; i < waveEnemyAmount; i++)
            {
                int randomSpawnPoint = (int)Mathf.Round(Random.Range(-0.49f, spawnPoints.Count - 0.51f));
                GameObject enemy = spawnPoints[randomSpawnPoint].GetComponent<DefaultEnemySpawn>().Spawn();
                if (enemy != null)
                {
                    Debug.Log("Spawning enemy," + enemy);
                    enemy.GetComponent<EntityEvents>().OnDie += DeleteEnemyFromList;
                    roomEnemies.Add(enemy);
                }
            }
        }
    }

    private void DeleteEnemyFromList(GameObject source, GameObject enemy)
    {
        Debug.Log("Removing enemy from list," + enemy);
        roomEnemies.Remove(enemy);

        enemy.GetComponent<EntityEvents>().OnDie -= DeleteEnemyFromList;
    }

    public void EndCombat()
    {
        //Debug.Log("Ending combat");
        inCombat = false;
        GetComponent<DoorScript>().OpenDoors();
    }

    private void Update()
    {


        if (activated && inCombat)
        {

            if (roomEnemies.Count == 0)
            {
                if (wavesLeft == 0)
                    EndCombat();
                else
                {
                    for (int i = 0; i < waveEnemyAmount; i++)
                    {
                        int randomSpawnPoint = (int)Mathf.Round(Random.Range(-0.49f, spawnPoints.Count - 0.51f));
                        GameObject enemy = spawnPoints[randomSpawnPoint].GetComponent<DefaultEnemySpawn>().Spawn();
                        if (enemy != null)
                        {
                            Debug.Log("Spawning enemy," + enemy);
                            enemy.GetComponent<EntityEvents>().OnDie += DeleteEnemyFromList;
                            roomEnemies.Add(enemy);
                        }
                    }
                    wavesLeft--;
                }

            }
        }
    }
}