using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAbilityToCastAI : MonoBehaviour
{
    EntityAbilityManager entityAbilityManager;
    int abilitySlotWithHighestCastValye;
    int castValueLimit = 50;
    private void Awake()
    {
        entityAbilityManager = GetComponent<EntityAbilityManager>();
    }
    public void UpdateCastValues()
    {
        int abilityCastValue;

        int bestCastValue = 0;
        if (entityAbilityManager.ability1 != null)
        {
            abilityCastValue = entityAbilityManager.ability1.GetCastValue();
            if (abilityCastValue > bestCastValue)
            {
                abilitySlotWithHighestCastValye = 1;
                bestCastValue = abilityCastValue;
            }
        }
        if (entityAbilityManager.ability2 != null)
        {
            abilityCastValue = entityAbilityManager.ability2.GetCastValue();
            if (abilityCastValue > bestCastValue)
            {
                abilitySlotWithHighestCastValye = 2;
                bestCastValue = abilityCastValue;
            }
        }
        if (entityAbilityManager.ability3 != null)
        {
            abilityCastValue = entityAbilityManager.ability3.GetCastValue();
            if (abilityCastValue > bestCastValue)
            {
                abilitySlotWithHighestCastValye = 3;
                bestCastValue = abilityCastValue;
            }
        }
        if (bestCastValue >= castValueLimit)
        {
            entityAbilityManager.CastAbility(abilitySlotWithHighestCastValye);
        }
    }
}
