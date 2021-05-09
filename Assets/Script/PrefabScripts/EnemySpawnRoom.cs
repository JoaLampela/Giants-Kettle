using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnRoom : MonoBehaviour
{

    [Range(1, 1000)]
    public int[] spawnWeight;
    public GameObject[] spawnEnemiesList;
    private int maxSpawnWeight = 0;
    public float enemyActivationTime = 0.5f;
    public float enemyAggroTime = 0.5f;

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
        yield return new WaitForSeconds(enemyActivationTime);
        enemy.SetActive(true);
        SoundManager.PlaySound(SoundManager.Sound.Summon, transform.position);
    }
    IEnumerator MakeAggro(GameObject enemy)
    {
        yield return new WaitForSeconds(enemyActivationTime + enemyAggroTime);
        if (enemy != null)
            enemy.GetComponent<EntityTargetingSystem>().IncreaseAggro(GameObject.FindGameObjectWithTag("Player"), 100);
    }
}
