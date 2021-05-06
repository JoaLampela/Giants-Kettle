using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySpellExplosionSound : MonoBehaviour
{
    void Start()
    {
        SoundManager.PlaySound(SoundManager.Sound.SpellExplode, transform.position);
    }


}
