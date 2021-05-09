using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{

    public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    private EntityEvents events;
    private GameEventManager gameEventManager;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameOverMenuHandler gameOverMenuHandler;
    [SerializeField] private GameObject deathAudioPlayer;
    private AudioMixer masterMixer;
    public bool inCombat = false;
    public bool lunaticMode = false;


    private void Awake()
    {
        gameEventManager = GameObject.Find("Game Manager").GetComponent<GameEventManager>();
        events = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityEvents>();
        masterMixer = Resources.Load("MasterMixer") as AudioMixer;
    }

    private void Start()
    {
        GamePaused = false;
        
        Subscribe();


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameEventManager.playerLevelUpScreenVisible)
        {
            if (gameOverScreen.activeSelf)
            {
                return;
            }
            else if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        gameEventManager.pauseMenuOpen = false;
        pauseMenuUI.SetActive(false);
        gameEventManager.ContinueTime();
        GamePaused = false;
    }


    public void StartLoad()
    {
        loadingScreen.SetActive(true);
    }
    public void StopLoad()
    {
        loadingScreen.SetActive(false);
    }

    void Pause()
    {
        gameEventManager.pauseMenuOpen = true;
        pauseMenuUI.SetActive(true);
        gameEventManager.StopTime();
        GamePaused = true;
    }

    public void LoadMenu()
    {
        gameEventManager.ContinueTime();
        GamePaused = false;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting.");
        Application.Quit();
    }

    public void GameOver(GameObject source, GameObject player)
    {
        StartCoroutine(gameOverMenuHandler.BeginningOfTheEndKills());

        StartCoroutine(gameOverMenuHandler.BeginningOFTheEndFloors());
        gameEventManager.StopTime();
        GameObject.FindGameObjectWithTag("PlayerUI").SetActive(false);
        gameOverScreen.SetActive(true);


        masterMixer.SetFloat("combatMusicVol", Mathf.Log10(0.0001f) * 20);
        masterMixer.SetFloat("musicVol", Mathf.Log10(0.0001f) * 20);
        AudioSource audioSource = deathAudioPlayer.GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.clip = SoundManager.GetAudioClip(SoundManager.Sound.YouDied);
        audioSource.outputAudioMixerGroup = masterMixer.FindMatchingGroups("SFX")[0];
        audioSource.Play();
    }
    void Subscribe()
    {
        events.OnDie += GameOver;
    }

    void Unsubscribe()
    {
        events.OnDie -= GameOver;
    }

    public void PlayLevel2()
    {
        GameObject.Find("Game Manager").GetComponent<GameEventManager>().ContinueTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private void OnDisable()
    {
        Unsubscribe();
    }
}
