using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDifficultyManagerScript : MonoBehaviour
{
    public static GameDifficultyManagerScript instance;

    public enum Difficulty
    {
        Easy,
        Normal,
        Hard,
        Lunatic
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public Difficulty difficulty = Difficulty.Normal;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetDifficulty(Difficulty difficulty2)
    {
        difficulty = difficulty2;
        Debug.Log("DIF "+ difficulty);
    }
}
