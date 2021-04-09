using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRuneScript2 : MonoBehaviour, IRuneScript
{
    private GameObject _projectile;
    private AbilityEvents _events;
    private GameObject _entity;
    private EntityEvents _entityEvents;
    public void SetAbilityEvents(AbilityEvents events)
    {
        _events = events;
    }

    public void SetEntity(GameObject entity)
    {
        _entity = entity;
        _entityEvents = entity.GetComponent<EntityEvents>();
    }

    public void SetProjectile(GameObject projectile)
    {
        _projectile = projectile;
    }

    public void SubscribeAbility()
    {
        throw new System.NotImplementedException();
    }

    public void SubscribeEntity()
    {
        throw new System.NotImplementedException();
    }

    public void UnsubscribeAbility()
    {
        throw new System.NotImplementedException();
    }

    public void UnsubscribeEntity()
    {
        throw new System.NotImplementedException();
    }
}
