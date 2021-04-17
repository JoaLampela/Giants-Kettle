using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAbilityManager : MonoBehaviour
{
    public GameObject rightHandGameObject;
    public GameObject leftHandGameObject;



    bool castingAbility = false;
    readonly private float minTimeBetweenAbilityCasts = 0.1f;

    public IAbility ability1;
    public IAbility ability2;
    public IAbility ability3;
    public IAbility ability4;


    public GameObject sting;
    public GameObject heavySting;
    public GameObject spinAttack;
    public GameObject bigProjectile;
    public GameObject nonProjectile;
    public GameObject shieldSlam;
    public GameObject shieldToss;
    public GameObject tripleShot;
    public GameObject powerShot;
    public GameObject dash;
    public GameObject basicAttackOneHandedSword;
    public GameObject basicAttackTwoHandedSword;

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
                    if (ability3 != null)
                    {
                        ability3.TryCast();
                    }
                    break;
                case 4:
                    if (ability4 != null)
                    {
                        ability4.TryCast();
                    }
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
        Debug.Log("Ability Set: " + slot);

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
        }
    }

    public void RemoveAbility(int slot)
    {
        Debug.Log("Ability Removed: " + slot);

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
        }
    }
}
