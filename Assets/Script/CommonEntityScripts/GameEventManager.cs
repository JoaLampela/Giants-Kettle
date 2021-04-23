using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance { get; private set; }


    public GameObject player;
    public float time;
    public float combatDuration;
    public List<GameObject> allEntities = new List<GameObject>();
    public List<GameObject> allies = new List<GameObject>();
    public List<GameObject> neutrals = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public bool combatOn = false;
    public bool gamePaused = false;

    public event Action OnCombatStart;
    public event Action OnCombatEnd;
    public event Action<GameObject> OnAllEntitiesRemove;
    public event Action<GameObject> OnAllEntitiesAdd;
    public event Action<GameObject, int> OnSetAggro;
    public event Action OnExitLevel;
    public event Action OnUpdateAggro;

    public event Action OnRoomClear;
    public event Action OnWaveClear;

    public int playerLevelUpPoints;
    public bool playerLevelUpScreenVisible = false;
    public GameObject LevelUpScreen;
    public TextMeshProUGUI levelPointsText;
    private void Update()
    {
        time += Time.deltaTime;
        if (combatOn) combatDuration += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R) && playerLevelUpPoints > 0 && !playerLevelUpScreenVisible)
        {
            playerLevelUpScreenVisible = true;
            LevelUpScreen.SetActive(true);
            LevelUpScreen.GetComponent<RuneTierListObjects>().RandomizeNewRunes();
            Debug.Log("OPENED");
        }
        else if((playerLevelUpPoints <= 0 || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Escape)) && playerLevelUpScreenVisible)
        {
            playerLevelUpScreenVisible = false;
            LevelUpScreen.SetActive(false);
            Debug.Log("CLOSED");
        }
        levelPointsText.text = playerLevelUpPoints.ToString();
    }
    public void ReducePlayerLevelUpPoints()
    {
        playerLevelUpPoints--;
    }

    public void RoomClear()
    {
        OnRoomClear?.Invoke();
    }
    public void WaveClear()
    {
        OnWaveClear?.Invoke();
    }


    public void CombatStart()
    {
        if (!combatOn)
        {
            OnCombatStart?.Invoke();
            combatOn = true;
            combatDuration = 0;
        }
    }

    public void CombatEnd()
    {
        if (combatOn)
        {
            OnCombatEnd();
            combatOn = false;
        }
        

    }

    public void AllEntitiesRemove(GameObject entity)
    {
        OnAllEntitiesRemove?.Invoke(entity);
    }
    public void AllEntitiesAdd(GameObject entity)
    {
        OnAllEntitiesAdd?.Invoke(entity);
    }

    public void RemoveFromTeam(int team, GameObject entity)
    {
        AllEntitiesRemove(entity);
        if (allEntities.Contains(entity)) allEntities.Remove(entity);
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
        AllEntitiesAdd(entity);
        if (!allEntities.Contains(entity)) allEntities.Add(entity);
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
    public void SetAggro(GameObject entity, int amount)
    {
        OnSetAggro?.Invoke(entity, amount);
    }

    public void UpdateAggro()
    {
        OnUpdateAggro?.Invoke();
    }
    public void ExitLevel()
    {
        OnExitLevel?.Invoke();

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();
    }
}
