using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySpellCollisionSound : MonoBehaviour
{
    void Start()
    {
        SoundManager.PlaySound(SoundManager.Sound.SpellHit, transform.position);
    }

 
}
