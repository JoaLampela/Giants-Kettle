using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTeamManager : MonoBehaviour
{
    private EntityEvents events;
    private EntityStats stats;
    GameEventManager gameManager;


    private void Awake()
    {
        stats = GetComponent<EntityStats>();
        events = GetComponent<EntityEvents>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameEventManager>();
    }

    void Start()
    {
        Subscribe();
        //Debug.Log(stats.team);
        SetTeam(stats.team);
    }

    private void Subscribe()
    {
        events.OnChangeTeam += SetTeam;
        events.OnDie += Die;
    }
    private void Unsubscribe()
    {
        events.OnChangeTeam -= SetTeam;
        events.OnDie -= Die;
    }

    private void SetTeam(int team)
    {
        gameManager.RemoveFromTeam(stats.team, gameObject);
        gameManager.AddToTeam(team, gameObject);
        stats.team = team;
    }
    private void Die(GameObject source, GameObject enemy)
    {
        gameManager.RemoveFromTeam(stats.team, gameObject);
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
}
