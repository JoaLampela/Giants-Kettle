using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance { get; private set; }


    public GameObject player;
    public float time;
    public float combatDuration;
    public List<GameObject> allies = new List<GameObject>();
    public List<GameObject> neutrals = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public bool combatOn = false;
    public bool gamePaused = false; 

    public event Action OnCombatStart;
    public event Action OnCombatEnd;

    private void Update()
    {
        time += Time.deltaTime;
        if (combatOn) combatDuration += Time.deltaTime;
    }

    public void CombatStart()
    {
        if(!combatOn)
        {
            OnCombatStart?.Invoke();
            combatOn = true;
            combatDuration = 0;
        }
    }

    public void CombatEnd()
    {
        if(combatOn)
        {
            OnCombatEnd();
            combatOn = false;
        }
    }

    public void RemoveFromTeam(int team, GameObject entity)
    {
        switch (team)
        {
            case 0:
                if (neutrals.Contains(entity)) neutrals.Remove(entity);
                break;
            case 1:
                if (enemies.Contains(entity)) enemies.Remove(entity);
                break;
            case 2:
                if (allies.Contains(entity)) allies.Remove(entity);
                break;
        }
    }
    public void AddToTeam(int team, GameObject entity)
    {
        switch (team)
        {
            case 0:
                neutrals.Add(entity);
                break;
            case 1:
                enemies.Add(entity);
                break;
            case 2:
                allies.Add(entity);
                break;
        }
    }
}
