using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuButtons;
    public GameObject selectLevel;
    [SerializeField] GameDifficultyManagerScript gameDifficultyManagerScript;


    public void PlayLevel1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Easy()
    {
        gameDifficultyManagerScript.difficulty = GameDifficultyManagerScript.Difficulty.Easy;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        
    }

    public void Normal()
    {
        gameDifficultyManagerScript.difficulty = GameDifficultyManagerScript.Difficulty.Normal;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Hard()
    {
        gameDifficultyManagerScript.difficulty = GameDifficultyManagerScript.Difficulty.Hard;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Lunatic()
    {
        gameDifficultyManagerScript.difficulty = GameDifficultyManagerScript.Difficulty.Lunatic;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void QuitGame()
    {
        Debug.Log("Quit.");
        Application.Quit();
    }
}
