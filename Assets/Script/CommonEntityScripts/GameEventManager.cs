using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance { get; private set; }

    public Light2D globalLight;

    public int globalLevel = 0;
    public GameObject player;
    public float time;
    public float combatDuration;
    public List<GameObject> allEntities = new List<GameObject>();
    public List<GameObject> allies = new List<GameObject>();
    public List<GameObject> neutrals = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> map = new List<GameObject>();
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

    public TextMeshProUGUI gameTimeText;

    private void Update()
    {
        time += Time.deltaTime;
        globalLevel = (int)time / 100 + 1;
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

        UpdateGameTimer();
    }

    private void UpdateGameTimer()
    {
        int hours;
        int mins;
        int seconds;

        
        mins = (int)(((int)time / 60f));
        seconds = (int)time - mins*60;
        hours = (int)(mins / 60f);
        mins -= hours * 60;
        if(hours > 99)
        {
            mins = 42;
            hours = 69;
            seconds = 00;
        }
        string hoursString;
        if (hours.ToString().Length == 1) hoursString = "0" + hours.ToString();
        else hoursString = hours.ToString();

        string minutesString;
        if (mins.ToString().Length == 1) minutesString = "0" + mins.ToString();
        else minutesString = mins.ToString();

        string secondsString;
        if (seconds.ToString().Length == 1) secondsString = "0" + seconds.ToString();
        else secondsString = seconds.ToString();
        gameTimeText.text = hoursString + ":" + minutesString + ":" + secondsString;
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
            case 3:
                if (map.Contains(entity)) map.Remove(entity);
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
            case 3:
                map.Add(entity);
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
