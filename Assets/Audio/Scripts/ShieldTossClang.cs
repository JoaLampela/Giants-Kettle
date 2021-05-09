using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTossClang : MonoBehaviour
{
    void Start()
    {
        SoundManager.PlaySound(SoundManager.Sound.Clang, transform.position);
    }
}