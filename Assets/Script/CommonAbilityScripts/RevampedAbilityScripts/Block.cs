using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IAbility
{
    public int GetCastValue()
    {
        throw new System.NotImplementedException();
    }

    public void SetSlot(int slot)
    {
        throw new System.NotImplementedException();
    }

    public void TryCast()
    {
        throw new System.NotImplementedException();
    }

    public Item GetWeapon()
    {
        return null;
    }

    public IAbility.Hand GetHand()
    {
        return IAbility.Hand.left;
    }
}
