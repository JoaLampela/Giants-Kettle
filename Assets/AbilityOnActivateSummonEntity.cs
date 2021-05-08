using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnActivateSummonEntity : MonoBehaviour
{
    private AbilityEvents _events;
    [SerializeField] GameObject[] possibleSummons;
    [SerializeField] float maxdistance;
    [SerializeField] float minDistance;
    [SerializeField] int summonCount;
    [SerializeField] GameObject summonEffect;



    private void Start()
    {
        Subscribe();
    }

    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Activate()
    {
        SoundManager.PlaySound(SoundManager.Sound.SummonStart, transform.position);
        int sumNum = Random.Range(1, summonCount);
        Debug.Log("in activate " + sumNum);
        for (int i = 0; i < sumNum; i++)
        {

            int randomAngle = Random.Range(0, 360);
            float y = Random.Range(minDistance, maxdistance);
            Vector2 dir = new Vector2(0, y);
            dir = Quaternion.Euler(0, 0, randomAngle) * dir;
            Vector2 pos = (Vector2)transform.position + dir;


            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 0.1f);
            if (hit)
            {
                Debug.Log("summon failed");
            }
            else
            {
                Instantiate(summonEffect, pos, transform.rotation);
                StartCoroutine(SummonEnemy(pos));
            }

        }
        StartCoroutine(SummonEnemySound());

    }
    private IEnumerator SummonEnemy(Vector2 pos)
    {
        yield return new WaitForSeconds(2);
        GameObject summon = possibleSummons[Random.Range(0, possibleSummons.Length)];
        Instantiate(summon, pos, Quaternion.identity);
    }

    private IEnumerator SummonEnemySound()
    {
        yield return new WaitForSeconds(2);
        SoundManager.PlaySound(SoundManager.Sound.Summon, transform.position);
    }


    private void Subscribe()
    {
        _events._onActivate += Activate;
    }

    private void Unsubscribe()
    {
        _events._onActivate -= Activate;
    }
}
