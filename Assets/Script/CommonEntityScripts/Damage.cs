using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public GameObject source;
    public int physicalDamage;
    public int spiritDamage;

    public Damage(GameObject newSource, int newPhysicalDamage = 0, int newSpiritDamage = 0)
    {
        source = newSource;
        physicalDamage = newPhysicalDamage;
        spiritDamage = newSpiritDamage;
    }
}
