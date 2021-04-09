using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IRuneScript
{
    public void SetProjectile(GameObject projectile);
    public void SetAbilityEvents(AbilityEvents abilityEvents);
    public void SetEntity(GameObject entity);

    public void SubscribeAbility();

    public void UnsubscribeAbility();

    public void SubscribeEntity();

    public void UnsubscribeEntity();
}

[Serializable]
public class IRuneScriptContainer : IUnifiedContainer<IRuneScript> { }
