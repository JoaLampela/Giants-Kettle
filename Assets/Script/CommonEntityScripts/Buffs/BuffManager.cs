using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    private EntityEvents events;
    private EntityStats stats;


    //Contains one entry for each SourceId. 
    //If new buff/debuff with the same source id is called will the most efficient one be present
    private Dictionary<string, BuffClass> activeBuffs;

    private void Awake()
    {
        stats = GetComponent<EntityStats>();
        activeBuffs = new Dictionary<string, BuffClass>();
        events = GetComponent<EntityEvents>();
    }

    private void Start()
    {
        Subscribe();
    }
    private void Subscribe()
    {
        events.OnNewBuff += NewBuff;
    }
    private void Unsubscribe()
    {
        events.OnNewBuff -= NewBuff;
    }


    //Called from Buff-script when new buff is created or old one has been deleted
    public void UpdateActiveBuffs(string sourceId, BuffClass buff)
    {
        Debug.Log(gameObject + " Updating active buffs");
        //if buff with this sourceId is not present in dictionary, new entry is created with this sourceId
        if(!activeBuffs.ContainsKey(sourceId))
        {
            stats.UpdateBuff(buff._id, buff._value); //<- 2.
            activeBuffs.Add(sourceId, buff);
            
        }
        else
        {
            //if this sourceId is already present in the dictionary it will be compared with this new one and
            //bigger value is updated to the dictionary
            if (activeBuffs[sourceId]._value <= buff._value)
            {
                stats.UpdateBuff(buff._id, -activeBuffs[sourceId]._value);
                activeBuffs[sourceId] = buff;
                stats.UpdateBuff(buff._id, buff._value); //<- 1.
            }
        }
    }


    //called from Buff-script when duration of buff reaches 0.
    //removes the buff from the dictionary
    public void RemoveBuff(string sourceId, BuffClass buff)
    {
        if(activeBuffs.ContainsKey(sourceId))
        {
            if (activeBuffs[sourceId]._value == buff._value)
            {
                stats.UpdateBuff(buff._id, -activeBuffs[sourceId]._value);
                activeBuffs.Remove(sourceId);
            }
            
        }
        else
        {
            Debug.Log("NO BUFF TO REMOVE");
        }
    }


    //Creates and adds new Buff-script to this entity.
    private void NewBuff(string sourceId, EntityStats.BuffType id, int value, float duration)
    {
        Debug.Log("NEW buff buff manager");

        Buff buff = gameObject.AddComponent<Buff>();
        if (id == EntityStats.BuffType.Burning) buff._sourceId = "Burning";
        if (id == EntityStats.BuffType.Stunned) buff._sourceId = "Stunned";
        if (id == EntityStats.BuffType.Invulnurable) buff._sourceId = "Invulnurable";
        else buff._sourceId = sourceId;
        buff._value = value;
        
        buff._effectID = id;
        //if buff has duration this starts courutine that destroys 
        //the script component after set amount of time
        if(duration != -1)
        {
            StartCoroutine(buff.DestroyAfterTime(duration));
        }
    }
    private void OnDisable()
    {
        Unsubscribe();
    }
}
