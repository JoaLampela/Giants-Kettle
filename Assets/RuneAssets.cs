using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class RuneAssets: MonoBehaviour
{
    private static RuneAssets _i;

    public static RuneAssets i
    {
        get
        {
            if (_i == null) _i = (Instantiate(Resources.Load("RuneAssets")) as GameObject).GetComponent<RuneAssets>();
            return _i;
        }
    }

    public GameObject RuneOrbArmorProjectile;
    public GameObject RuneOrbWeaponProjectile;
}
