using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteSound : MonoBehaviour
{
    void Start()
    {
        SoundManager.PlaySound(SoundManager.Sound.CriticalHit, transform.position);
    }
}
