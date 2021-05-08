using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSounds : MonoBehaviour
{
    void Start()
    {
        SoundManager.PlaySound(SoundManager.Sound.Zap, transform.position);
    }
}
