using UnityEngine;

public class AbilityActivateAfterDelay : MonoBehaviour
{
    private float _currentLifetime = 0f;
    private AbilityEvents _events;
    [SerializeField] private float _delay = 0f;
    private bool _activated = false;

    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
    }

    private void Update()
    {
        if(!_activated && _delay <= _currentLifetime)
        {
            _activated = true;
            _events.Activate();
        }
        _currentLifetime += Time.deltaTime;
    }
}
