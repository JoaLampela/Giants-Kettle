using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MusicManager
{

    public enum Sound
    {
        MainMenuMusic,
        BackgroundMusic,
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    public static void PlayMusic(Sound music)
    {
        if (CanPlaySound(music))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.PlayOneShot(GetAudioClip(music));
        }
    }

    private static bool CanPlaySound(Sound music)
    {
        switch (music)
        {
            default:
                return true;

                /*
            case Sound.PlayerMove:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = 0.4f;


                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
                */
        }
    }

    private static AudioClip GetAudioClip(Sound music)
    {
        foreach (GameAssets.MusicAudioClip musicAudioClip in GameAssets.i.MusicAudioClipArray)
        {
            if (musicAudioClip.music == music)
            {
                return musicAudioClip.musicClip;
            }
        }
        Debug.LogError("Sound" + music + " not found");
        return null;
    }
}
