using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDifficultyManagerScript : MonoBehaviour
{
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard,
        Lunatic
    }

    public Difficulty difficulty = Difficulty.Easy;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
