using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public GameObject source;
    public int _damage;
    public int _trueDamage;

    public Damage(GameObject newSource, int damage = 0, int trueDamage = 0)
    {
        source = newSource;
        _damage = damage;
        _trueDamage = trueDamage;
    }
}
