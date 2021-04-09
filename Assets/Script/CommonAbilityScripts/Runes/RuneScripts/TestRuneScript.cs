using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRuneScript : MonoBehaviour, IRuneScript
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
        Debug.Log("Subscribed");
        _events._onActivate += Test;
        _events._onDestroy += UnsubscribeAbility;
    }

    public void SubscribeEntity()
    {
        Debug.Log("Subscribed to entity events");
        _entityEvents.OnCastAbility += Test;
    }

    public void UnsubscribeAbility()
    {
        _events._onActivate -= Test;
        _events._onDestroy -= UnsubscribeAbility;
    }

    public void UnsubscribeEntity()
    {
        Debug.Log("unsubscribed to entity events");
        _entityEvents.OnCastAbility -= Test;
    }

    private void Test()
    {
        Debug.Log("IT WORKS!");
    }
}
