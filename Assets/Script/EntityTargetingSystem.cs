using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTargetingSystem : MonoBehaviour
{
    [SerializeField] float aggroRange = 10; 
    private GameObject target;
    private EntityEvents events;
    private GameEventManager gameEventManager;
    private Dictionary<GameObject, int> aggroTable;
    private EntityStats stats;

    private void Awake()
    {
        gameEventManager = GameObject.Find("Game Manager").GetComponent<GameEventManager>();
        events = GetComponent<EntityEvents>();
        stats = GetComponent<EntityStats>();
    }

    private void Start()
    {
        Subscribe();
        AggroTableSet();
    }

    private void Update()
    {
        if (stats.team == 1)
        {
            foreach (GameObject entity in gameEventManager.allies)
            {
                if (aggroTable.ContainsKey(entity) && aggroRange >= Vector2.Distance(entity.transform.position, transform.position) && !stats.isInvisible)
                {
                    if (aggroTable[entity] < 1) aggroTable[entity] = 1;
                }
            }
        }
        if (stats.team == 2)
        {
            foreach (GameObject entity in gameEventManager.enemies)
            {
                if (aggroTable.ContainsKey(entity) && aggroRange >= Vector2.Distance(entity.transform.position, transform.position) && !stats.isInvisible)
                {
                    if (aggroTable[entity] < 1) aggroTable[entity] = 1;
                }
            }
        }
    }

    private void Subscribe()
    {
        events.OnHitThis += TakeDamage;
        events.OnIncreaseAggro += IncreaseAggro;
        events.OnDecreaseAggro += DecreaseAggro;
        gameEventManager.OnSetAggro += SetAggro;
        gameEventManager.OnAllEntitiesAdd += AggroTableAdd;
        gameEventManager.OnAllEntitiesRemove += AggroTableRemove;
    }
    private void Unsubscribe()
    {
        events.OnHitThis -= TakeDamage;
        events.OnIncreaseAggro -= IncreaseAggro;
        events.OnDecreaseAggro -= DecreaseAggro;
        gameEventManager.OnSetAggro -= SetAggro;
        gameEventManager.OnAllEntitiesAdd -= AggroTableAdd;
        gameEventManager.OnAllEntitiesRemove -= AggroTableRemove;

    }
    private void AggroTableSet()
    {
        foreach (GameObject entity in gameEventManager.allEntities)
        {
            aggroTable.Add(entity, 0);
        }
    }
    private void AggroTableAdd(GameObject entity)
    {
        aggroTable.Add(entity, 0);
    }
    private void AggroTableRemove(GameObject entity)
    {
        aggroTable.Remove(entity);
        if (target = entity)
        {
            target = null;
            NewTarget();
        }
    }

    private void TakeDamage(Damage damage)
    {
        IncreaseAggro(damage.source, (damage.physicalDamage + damage.spiritDamage));
    }

    private void IncreaseAggro(GameObject entity, int amount)
    {
        if (aggroTable.ContainsKey(entity)) aggroTable[entity] += amount;
        if (target == null || !aggroTable.ContainsKey(target)) target = entity;
        else
        {
            if (aggroTable[target] < aggroTable[entity]) target = entity;
        }
    }

    private void DecreaseAggro(GameObject entity, int amount)
    {
        if (aggroTable.ContainsKey(entity))
        {
            aggroTable[entity] -= amount;
            if (aggroTable[entity] < 0) aggroTable[entity] = 0;
            NewTarget();
        }
    }

    private void SetAggro(GameObject entity, int amount)
    {
        if (aggroTable.ContainsKey(entity))
        {
            aggroTable[entity] = amount;
            NewTarget();
        }
    }

    private void NewTarget()
    {
        target = null;
        int tempTopScore = 0;
        if(stats.team == 0) 
        {
            target = null;
        }
        if (stats.team == 1)
        {
            foreach (GameObject entity in gameEventManager.allies)
            {
                if(aggroTable.ContainsKey(entity))
                {
                    if(aggroTable[entity] > tempTopScore)
                    {
                        tempTopScore = aggroTable[entity];
                        target = entity;
                    }
                }
            }
        }
        if (stats.team == 2)
        {
            foreach (GameObject entity in gameEventManager.enemies)
            {
                if (aggroTable.ContainsKey(entity))
                {
                    if (aggroTable[entity] > tempTopScore)
                    {
                        tempTopScore = aggroTable[entity];
                        target = entity;
                    }
                }
            }
        }
    }
    private void OnDisable()
    {
        Unsubscribe();
    }
}