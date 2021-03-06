using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Audio;

public class GameEventManager : MonoBehaviour
{
    public Color lunaticColor;

    public static GameEventManager Instance { get; private set; }

    public Light2D globalLight;

    public int floorsPassed = 0;

    public GameDifficultyManagerScript.Difficulty difficulty;

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

    public event Action<RuneObject> OnRunePicked;
    public event Action<EquipmentObject> OnEquipmentDropepd;

    public int playerLevelUpPoints;
    public bool playerLevelUpScreenVisible = false;
    public GameObject LevelUpScreen;
    public TextMeshProUGUI levelPointsText;
    public TextMeshProUGUI levelPointsText2LeftCorner;
    public TextMeshProUGUI levelPointReminderText;

    public TextMeshProUGUI gameTimeText;
    public GameObject inventory;
    public bool inventoryOpen = false;

    public bool pauseMenuOpen = false;

    public bool castingLocked = false;
    private bool runesRandomized = false;

    public bool playerDead = false;

    [SerializeField] private GameStats gameStats;

    private AudioMixer masterMixer;
    private float musicVolume;
    [SerializeField] private float musicFade = 7f;
    [SerializeField] private GameObject combatMusicPlayer;
    [SerializeField] private GameObject canvas;

    [SerializeField] private NoobPanelScript noobPanelScript;

    private void Start()
    {
        StopTime();

        if (GameObject.Find("GameDifficultyManager")) difficulty = GameObject.Find("GameDifficultyManager").GetComponent<GameDifficultyManagerScript>().difficulty;
        else difficulty = GameDifficultyManagerScript.Difficulty.Normal;

        combatMusicPlayer = GameObject.FindGameObjectWithTag("CombatMusicPlayer");
        masterMixer = Resources.Load("MasterMixer") as AudioMixer;
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);

        if(difficulty == GameDifficultyManagerScript.Difficulty.Lunatic)
        {
            globalLight.color = lunaticColor;
            canvas.GetComponent<PauseMenu>().lunaticMode = true;
            playerLevelUpPoints = 3;
        }
        if (difficulty == GameDifficultyManagerScript.Difficulty.Hard)
        {
            playerLevelUpPoints = 0;
        }
        if (difficulty == GameDifficultyManagerScript.Difficulty.Normal)
        {
            playerLevelUpPoints = 0;
        }
        if (difficulty == GameDifficultyManagerScript.Difficulty.Easy)
        {
            playerLevelUpPoints = 2;
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        globalLevel = (int)time / 120;
        if (combatOn) combatDuration += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R) && playerLevelUpPoints > 0 && !playerLevelUpScreenVisible && !pauseMenuOpen && !playerDead)
        {
           
            ToggleRuneSelectionView();
            StopTime();
        }
        else if ((playerLevelUpPoints <= 0 || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Escape)) && playerLevelUpScreenVisible && !pauseMenuOpen && !playerDead)
        {
            Debug.Log("TOGGLE runes");
            ToggleRuneSelectionView();
            ContinueTime();
        }
        levelPointsText.text = playerLevelUpPoints.ToString();
        levelPointsText2LeftCorner.text = playerLevelUpPoints.ToString();

        UpdateGameTimer();

        if (Input.GetKeyDown(KeyCode.Tab) && !inventoryOpen && !pauseMenuOpen && !playerDead)
        {
            noobPanelScript.ToggleInventory();
            inventoryOpen = true;
            inventory.GetComponent<CanvasGroup>().alpha = 1;
            inventory.GetComponent<CanvasGroup>().blocksRaycasts = true;
            inventory.GetComponent<CanvasGroup>().interactable = true;
            SoundManager.PlayUISound(SoundManager.Sound.InventoryOpen);
            InventoryGamePauseToggle.inventoryOpen = true;
            if (InventoryGamePauseToggle.pauseWhenOpen)
            {
                StopTime();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && inventoryOpen && !pauseMenuOpen && !playerDead)
        {
            noobPanelScript.ToggleInventory();
            inventoryOpen = false;
            inventory.GetComponent<CanvasGroup>().alpha = 0;
            inventory.GetComponent<CanvasGroup>().blocksRaycasts = false;
            inventory.GetComponent<CanvasGroup>().interactable = false;
            SoundManager.PlayUISound(SoundManager.Sound.InventoryOpen);
            InventoryGamePauseToggle.inventoryOpen = false;
            if (InventoryGamePauseToggle.pauseWhenOpen)
            {
                ContinueTime();
            }
        }
        if (playerLevelUpPoints > 0)
        {
            levelPointReminderText.enabled = true;
            noobPanelScript.ToggleRunePointTip(true);
        }
        else
        {
            noobPanelScript.ToggleRunePointTip(false);
            levelPointReminderText.enabled = false;
        }
    }

    public void ToggleRuneSelectionView()
    {
        noobPanelScript.ToggleRuneSelection();
        if (!playerLevelUpScreenVisible)
        {
            playerLevelUpScreenVisible = true;
            LevelUpScreen.SetActive(true);
            if (!runesRandomized) LevelUpScreen.GetComponent<RuneTierListObjects>().RandomizeNewRunes();
            runesRandomized = true;
        }
        else
        {
            playerLevelUpScreenVisible = false;
            LevelUpScreen.SetActive(false);
        }
    }

    private void UpdateGameTimer()
    {
        int hours;
        int mins;
        int seconds;


        mins = (int)(((int)time / 60f));
        seconds = (int)time - mins * 60;
        hours = (int)(mins / 60f);
        mins -= hours * 60;
        if (hours > 99)
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
        gameStats.AddClearedRoom();
        Debug.Log("Room clear");
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

            if(difficulty == GameDifficultyManagerScript.Difficulty.Lunatic)
            {
                canvas.GetComponent<AudioScript>().inCombat = true;
                canvas.GetComponent<PauseMenu>().inCombat = true;
                StartCoroutine(FadeMixerGroup.StartFade(masterMixer, "musicVol", musicFade, 0.0001f));
                StartCoroutine(FadeMixerGroup.StartFade(masterMixer, "combatMusicVol", musicFade, musicVolume));
                AudioSource audioSource = combatMusicPlayer.GetComponent<AudioSource>();
                audioSource.clip = SoundManager.GetAudioClip(SoundManager.Sound.CombatMusic);
                audioSource.outputAudioMixerGroup = masterMixer.FindMatchingGroups("CombatMusic")[0];
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }

    public void CombatEnd()
    {
        if (combatOn)
        {
            OnCombatEnd?.Invoke();
            combatOn = false;
            combatDuration = 0;

            if (difficulty == GameDifficultyManagerScript.Difficulty.Lunatic)
            {
                canvas.GetComponent<AudioScript>().inCombat = false;
                canvas.GetComponent<PauseMenu>().inCombat = false;
                StartCoroutine(FadeMixerGroup.StartFade(masterMixer, "combatMusicVol", musicFade, 0.0001f));
                StartCoroutine(FadeMixerGroup.StartFade(masterMixer, "musicVol", musicFade, musicVolume));
                StartCoroutine(FadeMixerGroup.StopMusic(combatMusicPlayer, musicFade));
            }
        }
    }

    public void EquipmentDropped(EquipmentObject equipment)
    {
        OnEquipmentDropepd?.Invoke(equipment);
    }

    public void RunePicked(RuneObject rune)
    {
        OnRunePicked?.Invoke(rune);
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
        floorsPassed++;
        OnExitLevel?.Invoke();

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();
    }

    public void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void ContinueTime()
    {
        if (!playerDead) Time.timeScale = 1f;
    }
}
