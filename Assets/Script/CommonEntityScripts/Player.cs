using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    EntityEvents events;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<EntityAbilityManager>().SetAbility(1, GetComponent<AbilityFireBall>());
        GetComponent<EntityAbilityManager>().SetAbility(2, GetComponent<AbilityGroundSlam>());
        events = GetComponent<EntityEvents>();
        events.NewBuff("Buff", "bonusHealth", 10);
        events.NewBuff("Thorns", "bonusSpirit", 10, 15);
        events.NewBuff("Dragon's Blood", "bonusHealth", 20, 5);
        events.NewBuff("Giant's Vitality", "bonusHealth", 5, 20);
        events.NewBuff("TurboRegen", "bonusHealthRegen", 200, 2);
        events.HitThis(new Damage(gameObject, 30, 30));
        events.NewBuff("EnergyBoost", "bonusEnergy", 30, 30);
        events.NewBuff("SpiritRegebBoost", "bonusSpiritRegen", 600, 2);
        events.NewBuff("Blood Lust", "baseRageDepletion", 500, 5);
        events.RecoverRage(100);
        events.DeteriorateEnergy(70);
        events.DeteriorateSpirit(50);
        
    }

}
