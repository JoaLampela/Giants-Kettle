using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IRuneScript
{
    public enum WeaponType
    {
        OneHandedSword,
        TwoHandedSword,
        Shield,
        Bow,
        Staff
    }
    public void SetIsInArmorSlot();
    public void SetWeaponType(WeaponType weaponType);
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
