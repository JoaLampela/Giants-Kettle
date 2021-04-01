using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAbilityManager : MonoBehaviour
{
    bool castingAbility = false;
    readonly private float minTimeBetweenAbilityCasts = 0.1f;

    public IAbility ability1;
    public IAbility ability2;
    public IAbility ability3;
    public IAbility ability4;
    public IAbility ability5;
    public IAbility ability6;
    public GameObject sting;

    public void CastAbility(int slot)
    {
        if(!castingAbility)
        {
            castingAbility = true;
            StartCoroutine(AbilityCoolDownMinimum());
            switch (slot)
            {
                case 1:
                    if(ability1 != null)
                    {
                        ability1.TryCast();
                    }
                    break;
                case 2:
                    if (ability2 != null)
                    {
                        ability2.TryCast();
                    }
                    break;
                case 3:
                    ability3.TryCast();
                    break;
                case 4:
                    ability4.TryCast();
                    break;
                case 5:
                    ability5.TryCast();
                    break;
                case 6:
                    ability6.TryCast();
                    break;
            }
        }
    }
    private IEnumerator AbilityCoolDownMinimum()
    {
        yield return new WaitForSeconds(minTimeBetweenAbilityCasts);
        castingAbility = false;
    }


    public void SetAbility(int slot, IAbility ability)
    {
        switch (slot) 
        {
            case 1:
                ability1 = ability;
                ability1.SetSlot(1);
                break;
            case 2:
                ability2 = ability;
                ability2.SetSlot(2);
                break;
            case 3:
                ability3 = ability;
                ability3.SetSlot(3);
                break;
            case 4:
                ability4 = ability;
                ability4.SetSlot(4);
                break;
            case 5:
                ability5 = ability;
                ability5.SetSlot(5);
                break;
            case 6:
                ability6 = ability;
                ability6.SetSlot(6);
                break;
        }
    }

    public void RemoveAbility(int slot)
    {
        switch (slot)
        {
            case 1:
                ability1 = null;
                break;
            case 2:
                ability2 = null;
                break;
            case 3:
                ability3 = null;
                break;
            case 4:
                ability4 = null;
                break;
            case 5:
                ability5 = null;
                break;
            case 6:
                ability6 = null;
                break;
        }
    }
}
