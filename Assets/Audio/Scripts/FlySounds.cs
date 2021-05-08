using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySounds : MonoBehaviour
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
        SoundManager.PlaySound(SoundManager.Sound.Splat, transform.position);
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
