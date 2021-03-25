using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEffectLifetime : MonoBehaviour
{
    [SerializeField] private float _maxLifeTime;
    private float _lifeTime = 0f;

    void Update()
    {
        _lifeTime += Time.deltaTime;

        if(_lifeTime > _maxLifeTime)
        {
            Destroy(gameObject);
        }
    }
}
