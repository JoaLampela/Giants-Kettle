using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRuneScript : MonoBehaviour, IRuneScript
{
    private GameObject _projectile;
    private AbilityEvents _events;
    private GameObject _entity;
    private EntityEvents _entityEvents;
    private WeaponType _weaponType;
    private bool isInArmor;

    //Always needed functions
    public enum WeaponType
    {
        OneHandedSword,
        TwoHandedSword,
        Shield,
        Bow,
        Staff
    }

    public void SetIsInArmorSlot()
    {
        isInArmor = true;
    }

    public void SetAbilityEvents(AbilityEvents events)
    {
        _events = events;
    }

    public void SetEntity(GameObject entity)
    {
        _entity = entity;
        _entityEvents = entity.GetComponent<EntityEvents>();
        SetUpPermanentEffects();
    }

    public void SetProjectile(GameObject projectile)
    {
        _projectile = projectile;
    }

    public void SetWeaponType(IRuneScript.WeaponType weaponType)
    {
        if (weaponType == IRuneScript.WeaponType.OneHandedSword) _weaponType = WeaponType.OneHandedSword;
        else if (weaponType == IRuneScript.WeaponType.TwoHandedSword) _weaponType = WeaponType.TwoHandedSword;
        else if (weaponType == IRuneScript.WeaponType.Shield) _weaponType = WeaponType.Shield;
        else if (weaponType == IRuneScript.WeaponType.Bow) _weaponType = WeaponType.Bow;
        else if (weaponType == IRuneScript.WeaponType.Staff) _weaponType = WeaponType.Staff;
    }

    public void OnEquip()
    {
        
    }

    public void OnUnequip()
    {

    }

    private void SetUpPermanentEffects()
    {
        if(isInArmor)
        {
            _entityEvents.NewBuff("TestRuneScriptBuff1", EntityStats.BuffType.Health, 100);
            _entityEvents.NewBuff("TestRuneScriptBuff2", EntityStats.BuffType.Speed, 100);
        }
        else
        {

        }

    }



    private void Test()
    {
        Debug.Log("IT WORKS!");
    }

    

    //Subs and Unsubs
    public void SubscribeAbility()
    {
        _events._onActivate += Test;
        _events._onDestroy += UnsubscribeAbility;
    }

    public void SubscribeEntity()
    {
        _entityEvents.OnCastAbility += Test;
    }

    public void UnsubscribeAbility()
    {
        _events._onActivate -= Test;
        _events._onDestroy -= UnsubscribeAbility;
    }

    public void UnsubscribeEntity()
    {
        _entityEvents.OnCastAbility -= Test;
    }

    public void IncrementDuplicateCountWeapon()
    {
        throw new System.NotImplementedException();
    }

    public void DecrementDuplicateCountWeapon()
    {
        throw new System.NotImplementedException();
    }

    public void IncrementDuplicateCountArmor()
    {
        throw new System.NotImplementedException();
    }

    public void DecrementDuplicateCountArmor()
    {
        throw new System.NotImplementedException();
    }

    public int GetDuplicateCountWeapon()
    {
        throw new System.NotImplementedException();
    }

    public void RemoveRune()
    {
        throw new System.NotImplementedException();
    }

    public int GetDuplicateCountArmor()
    {
        throw new System.NotImplementedException();
    }

    public void SetDuplicateCountWeapon(int value)
    {
        throw new System.NotImplementedException();
    }

    public void IncrementDuplicateCountWeapon(int amount)
    {
        throw new System.NotImplementedException();
    }

    public void DecrementDuplicateCountWeapon(int amount)
    {
        throw new System.NotImplementedException();
    }

    public void IncrementDuplicateCountArmor(int amount)
    {
        throw new System.NotImplementedException();
    }

    public void DecrementDuplicateCountArmor(int amount)
    {
        throw new System.NotImplementedException();
    }

    public bool GetIsDestroyed()
    {
        throw new System.NotImplementedException();
    }
}
