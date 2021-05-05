using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }

    public Transform pfDamagePopup;
    public Transform pfXpPopup;
    public GameObject flameEffect;


    public SoundAudioClip[] SoundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }


    public MusicAudioClip[] MusicAudioClipArray;

    [System.Serializable]
    public class MusicAudioClip
    {
        public MusicManager.Sound music;
        public AudioClip musicClip;
    }
}
