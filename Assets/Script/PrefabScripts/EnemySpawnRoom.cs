using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnRoom : MonoBehaviour
{

    [Range(1, 100)]
    public int[] spawnWeight;
    public GameObject[] spawnEnemiesList;
    private int maxSpawnWeight = 0;

    private void Awake()
    {
        for (int i = 0; i < spawnWeight.Length; i++)
            maxSpawnWeight += spawnWeight[i];
    }

    // Start is called before the first frame update
    public GameObject Spawn()
    {
        float randomNumber = Random.Range(0, maxSpawnWeight);
        int currentNumber = 0;
        for (int i = 0; i < spawnWeight.Length; i++)
        {
            currentNumber += spawnWeight[i];
            if (currentNumber - spawnWeight[i] <= randomNumber && randomNumber < currentNumber)
            {
                GameObject enemy = GameObject.Instantiate(spawnEnemiesList[i], transform.position, Quaternion.identity);
                GetComponent<ParticleSystem>().Play();
                StartCoroutine(MakeAggro(enemy));
                StartCoroutine(ActivateObject(enemy));
                enemy.SetActive(false);
                return enemy;
            }
        }
        return null;


    }

    IEnumerator ActivateObject(GameObject enemy)
    {
        yield return new WaitForSeconds(0.5f);
        enemy.SetActive(true);
    }
    IEnumerator MakeAggro(GameObject enemy)
    {
        yield return new WaitForSeconds(1f);
        enemy.GetComponent<EntityTargetingSystem>().IncreaseAggro(GameObject.FindGameObjectWithTag("Player"), 100);
    }
}
