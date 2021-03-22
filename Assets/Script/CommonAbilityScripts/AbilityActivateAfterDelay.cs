using UnityEngine;

public class AbilityActivateAfterDelay : MonoBehaviour
{
    private AbilityEvents _events;
    [SerializeField] private float _delay = 0f;
    private bool _activated = false;

    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
    }

    private void Update()
    {
        if(!_activated && _delay <= _events._timeActive)
        {
            _activated = true;
            _events.Activate();
        }
    }
}
