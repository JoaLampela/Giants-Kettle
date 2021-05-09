using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuButtons;
    public GameObject selectLevel;
    public GameObject loadingScreen;


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
        StartCoroutine(Easy2());

    }
    IEnumerator Easy2()
    {
        loadingScreen.SetActive(true);
        GameObject.Find("GameDifficultyManager").GetComponent<GameDifficultyManagerScript>().SetDifficulty(GameDifficultyManagerScript.Difficulty.Easy);
        Debug.Log("Setting Easy");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Normal()
    {
        StartCoroutine(Normal2());
    }
    IEnumerator Normal2()
    {
        loadingScreen.SetActive(true);
        GameObject.Find("GameDifficultyManager").GetComponent<GameDifficultyManagerScript>().SetDifficulty(GameDifficultyManagerScript.Difficulty.Normal);
        Debug.Log("Setting Normal");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Hard()
    {
        StartCoroutine(Hard2());
    }
    IEnumerator Hard2()
    {
        loadingScreen.SetActive(true);
        GameObject.Find("GameDifficultyManager").GetComponent<GameDifficultyManagerScript>().SetDifficulty(GameDifficultyManagerScript.Difficulty.Hard);
        Debug.Log("Setting Hard");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Lunatic()
    {
        StartCoroutine(Lunatic2());
    }

    IEnumerator Lunatic2()
    {
        loadingScreen.SetActive(true);
        GameObject.Find("GameDifficultyManager").GetComponent<GameDifficultyManagerScript>().SetDifficulty(GameDifficultyManagerScript.Difficulty.Lunatic);
        Debug.Log("Setting Lunatic");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void QuitGame()
    {
        Debug.Log("Quit.");
        Application.Quit();
    }
}
