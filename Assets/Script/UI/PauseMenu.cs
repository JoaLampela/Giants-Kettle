using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverScreen;
    private EntityEvents events;
    private GameEventManager gameEventManager;


    private void Awake()
    {
        gameEventManager = GameObject.Find("Game Manager").GetComponent<GameEventManager>();
        events = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityEvents>();
    }

    private void Start()
    {
        GamePaused = false;
        
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

    public void GameOver()
    {
        gameEventManager.StopTime();
        GameObject.FindGameObjectWithTag("PlayerUI").SetActive(false);
        GameObject.FindGameObjectWithTag("PauseMenu").SetActive(false);
        GameObject.FindGameObjectWithTag("GameOverScreen").SetActive(true);
    }
    /*
    void Subscribe()
    {
        events.OnDie += GameOver;
    }

    void Unsubscribe()
    {
        events.OnDie -= GameOver;
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
    */
}
