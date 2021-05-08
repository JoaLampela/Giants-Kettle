using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSounds : MonoBehaviour
{
    private EntityEvents events;
    private void Start()
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
        SoundManager.PlaySound(SoundManager.Sound.SkeletonDie,transform.position);
    }

    private void Subscribe()
    {
        events.OnDie += PlayDeathSound;
    }
    private void Unsubscribe()
    {
        events.OnDie -= PlayDeathSound;
    }
}
