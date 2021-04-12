using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffClass
{
    public EntityStats.BuffType _id;
    public int _value;

    public BuffClass(EntityStats.BuffType id, int value)
    {
        _id = id;
        _value = value;
    }
}
