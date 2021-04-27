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
    public enum Hand
    {
        right,
        left,
        dual,
        indeterminate
    }
    public void SetUpPermanentEffects();
    public void SetDuplicateCountWeapon(int value);
    public void IncrementDuplicateCountWeapon(int amount, IRuneScript.Hand hand);

    public void DecrementDuplicateCountWeapon(int amount, IRuneScript.Hand hand);

    public void IncrementDuplicateCountArmor(int amount);

    public void DecrementDuplicateCountArmor(int amount);
    public int GetDuplicateCountWeapon();

    public int GetDuplicateCountWeaponRight();

    public int GetDuplicateCountWeaponLeft();

    public void RemoveRune();

    public int GetDuplicateCountArmor();
    public void SetWeaponType(WeaponType weaponType);
    public void SetEntity(GameObject entity);

    public void SetContainerItem(Item item, IRuneScript.Hand hand);
}

[Serializable]
public class IRuneScriptContainer : IUnifiedContainer<IRuneScript> { }
