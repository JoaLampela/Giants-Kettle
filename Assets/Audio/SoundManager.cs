using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public static class SoundManager
{
 
    public enum Sound
    {
        PlayerMove,
        OneHandedBasicAttack,
        TwoHandedBasicAttack,
        Dash,
        StingRight,
        StingLeft,
        SpinAttack,
        EnemyHit,
        EnemyDie,
        ButtonOver,
        ButtonClick,
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    private static AudioMixer masterMixer;
    private static AudioMixerGroup sfxGroup;


    public static void Initialize()
    {
        masterMixer = Resources.Load("MasterMixer") as AudioMixer;
        sfxGroup = masterMixer.FindMatchingGroups("SFX")[0];
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerMove] = 0f;
    }

    public static void PlaySound(Sound sound, Vector3 position)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.maxDistance = 100f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.outputAudioMixerGroup = sfxGroup;
            audioSource.Play();
            
            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            if(oneShotGameObject == null)
            {
                 oneShotGameObject = new GameObject("One Shot Sound");
                 oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;

            case Sound.PlayerMove:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = 0.4f;
                    

                    if(lastTimePlayed + playerMoveTimerMax < Time.time)
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
        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.SoundAudioClipArray)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + " not found");
        return null;
    }

    /*
    public static void AddButtonSounds(this Button_UI button)
    {
        button.ClickFunc += () => SoundManager.PlaySound(Sound.ButtonClick);
        button.MouseOverOnceFunc += () => SoundManager.PlaySound(Sound.ButtonOver);
    }
    */
}