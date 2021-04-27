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


    private void Awake()
    {
        events = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityEvents>();
    }

    private void Start()
    {
        GamePaused = false;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
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
        Time.timeScale = 0;
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
