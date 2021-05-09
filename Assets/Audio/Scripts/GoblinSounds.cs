using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSounds : MonoBehaviour
{
    private EntityEvents events;
    private void Awake()
    {
        events = gameObject.GetComponent<EntityEvents>();
        Subscribe();
    }
    private void OnDisable()
    {
        Unsubscribe();
    }
    private void PlayDeathSound(GameObject spaghetti, GameObject bolognese)
    {
        SoundManager.PlaySound(SoundManager.Sound.GoblinDie, transform.position);
    }

    private void Hitmarker(Damage pasta)
    {
        SoundManager.PlaySound(SoundManager.Sound.NormalHit, transform.position);
    }

    private void Subscribe()
    {
        events.OnDie += PlayDeathSound;
        events.OnHitThis += Hitmarker;
    }
    private void Unsubscribe()
    {
        //events.OnDie -= PlayDeathSound;
    }
}
