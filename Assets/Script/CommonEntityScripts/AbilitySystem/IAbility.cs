using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    public enum Hand
    {
        right,
        left,
        indeterminate
    }
    public void SetSlot(int slot);
    public void TryCast();

    public Item GetWeapon();

    public Hand GetHand();

    //should return valuation for how good this spell is in this situation capping at 100
    public int GetCastValue();
}
