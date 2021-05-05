using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOnStayActivate : MonoBehaviour
{
    private AbilityEvents _events;
    [SerializeField] private TriggerTargetTeam team;
    private enum TriggerTargetTeam { Caster, Enemy, Neutral} 


    private void Awake()
    {
        _events = GetComponent<AbilityEvents>();
    }

    private void Activate()
    {
        _events.Activate();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        

        if(team == TriggerTargetTeam.Caster)
        {
            if(collision.GetComponent<EntityStats>())
            {
                if(_events._abilityCastSource.GetComponent<EntityStats>().team == collision.GetComponent<EntityStats>().team) Activate();
            }
        }
        if (team == TriggerTargetTeam.Enemy)
        {
            if (collision.GetComponent<EntityStats>())
            {
                if (_events._abilityCastSource.GetComponent<EntityStats>().team != collision.GetComponent<EntityStats>().team) Activate();
            }
        }
        if (team == TriggerTargetTeam.Neutral)
        {
            if (collision.GetComponent<EntityStats>())
            {
                Activate();
            }
        }
    }


}
