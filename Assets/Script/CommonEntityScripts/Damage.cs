using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public GameObject source;
    public int physicalDamage;
    public int spiritDamage;

    public Damage(GameObject newSource, int newPhysicalDamage, int newSpiritDamage)
    {
        source = newSource;
        physicalDamage = newPhysicalDamage;
        spiritDamage = newSpiritDamage;
    }
}
