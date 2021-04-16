
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAbilityCoolDownManager : MonoBehaviour
{
    public static GameAbilityCoolDownManager Instance { get; private set; }

    public List<Item> weaponsOnCooldown;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            weaponsOnCooldown = new List<Item>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        foreach(Item weapon in weaponsOnCooldown)
        {
            if (weapon.currentCooldownAbility1 > 0)
            {
                weapon.currentCooldownAbility1 -= Time.deltaTime;
            }
            else weapon.currentCooldownAbility1 = 0;

            if (weapon.currentCooldownAbility2 > 0)
            {
                weapon.currentCooldownAbility2 -= Time.deltaTime;
                //Debug.Log(weapon.currentCooldownAbility2 + " " + weapon);
            }
            else weapon.currentCooldownAbility2 = 0;
        }
    }
}
