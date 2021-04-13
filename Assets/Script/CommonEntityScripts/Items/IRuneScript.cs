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

    public void SetDuplicateCountWeapon(int value);
    public void IncrementDuplicateCountWeapon();

    public void DecrementDuplicateCountWeapon();

    public void IncrementDuplicateCountArmor();

    public void DecrementDuplicateCountArmor();
    public int GetDuplicateCountWeapon();

    public void RemoveRune();

    public int GetDuplicateCountArmor();
    public void SetWeaponType(WeaponType weaponType);
    public void SetEntity(GameObject entity);
}

[Serializable]
public class IRuneScriptContainer : IUnifiedContainer<IRuneScript> { }
