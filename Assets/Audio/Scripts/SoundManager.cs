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
        SpellCast,
        SpellHit,
        SpellExplode,
        SpellAoE,
        Dash,
        StingRight,
        StingLeft,
        TwoHandedSting,
        SpinAttack,
        EnemyHit,
        GoblinDie,
        SkeletonDie,
        SummonerDie,
        BossDie,
        ButtonHover,
        ButtonClick,
        Footstep1,
        Footstep2,
        Footstep3,
        Footstep4,
        ShieldToss,
        ShieldSlam,
        Block,
        TripleShot,
        ItemEquip,
        ItemUnequip,
        RuneEquip,
        RuneUnequip,
        NormalHit,
        CriticalHit,
        Splat,
        InventoryOpen,
        PickUp,
        Ignite,
        Zap,
        ArcadeExplosion,
        Summon,
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    private static AudioMixer masterMixer;
    private static AudioMixerGroup sfxGroup;
    private static AudioMixerGroup masterGroup;
    private static bool initialized = false;


    public static void Initialize()
    {
        if (!initialized)
        {
            masterMixer = Resources.Load("MasterMixer") as AudioMixer;
            sfxGroup = masterMixer.FindMatchingGroups("SFX")[0];
            masterGroup = masterMixer.FindMatchingGroups("Master")[0];
            soundTimerDictionary = new Dictionary<Sound, float>();
            soundTimerDictionary[Sound.PlayerMove] = 0f;
            initialized = true;
        }
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
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = new Vector3(0,0,0);
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

    public static void PlayUISound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            if(oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                oneShotAudioSource.outputAudioMixerGroup = masterGroup;
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
                /*
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
                */
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

  
}
