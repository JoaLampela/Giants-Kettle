using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTargetingSystem : MonoBehaviour, IAbilityTargetPosition
{
    [SerializeField] float aggroRange = 10;
    public GameObject target;
    private EntityEvents events;
    private GameEventManager gameEventManager;
    private Dictionary<GameObject, int> aggroTable = new Dictionary<GameObject, int>();
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
                if (aggroTable.ContainsKey(entity) && aggroRange >= Vector2.Distance(entity.transform.position, transform.position) && !entity.GetComponent<EntityStats>().isInvisible)
                {

                    if (aggroTable[entity] < 1)
                    {
                        aggroTable[entity] = 1;
                        NewTarget();
                    }
                }
            }
        }
        if (stats.team == 2)
        {
            foreach (GameObject entity in gameEventManager.enemies)
            {
                if (aggroTable.ContainsKey(entity) && aggroRange >= Vector2.Distance(entity.transform.position, transform.position) && !entity.GetComponent<EntityStats>().isInvisible)
                {
                    if (aggroTable[entity] < 1)
                    {
                        aggroTable[entity] = 1;
                        NewTarget();
                    }
                }
            }
        }
    }

    private void Subscribe()
    {
        gameEventManager.OnUpdateAggro += NewTarget;
        events.OnHitThis += TakeDamage;
        events.OnIncreaseAggro += IncreaseAggro;
        events.OnDecreaseAggro += DecreaseAggro;
        gameEventManager.OnSetAggro += SetAggro;
        gameEventManager.OnAllEntitiesAdd += AggroTableAdd;
        gameEventManager.OnAllEntitiesRemove += AggroTableRemove;
    }
    private void Unsubscribe()
    {
        gameEventManager.OnUpdateAggro -= NewTarget;
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
        if (damage.source.GetComponent<EntityStats>().team != 3)
        {
            Debug.Log("source = " + damage.source + " team = " + damage.source.GetComponent<EntityStats>().team);
            IncreaseAggro(damage.source, damage._damage);
        }

    }

    public void IncreaseAggro(GameObject entity, int amount)
    {
        Debug.Log("increase aggro");
        if (aggroTable.ContainsKey(entity)) aggroTable[entity] += amount;
        if (target == null || !aggroTable.ContainsKey(target)) target = entity;
        else
        {
            if (aggroTable[target] < aggroTable[entity]) target = entity;
        }
    }

    private void DecreaseAggro(GameObject entity, int amount)
    {
        Debug.Log("decrease aggro");
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
        float tempDistance = 1000f;
        target = null;
        int tempTopScore = 0;
        if (stats.team == 0)
        {
            target = null;
        }
        if (stats.team == 1)
        {
            foreach (GameObject entity in gameEventManager.allies)
            {
                if (aggroTable.ContainsKey(entity))
                {
                    if (aggroTable[entity] > tempTopScore)
                    {
                        float distance = Vector2.Distance(entity.transform.position, transform.position);
                        if (!entity.GetComponent<EntityStats>().isInvisible && aggroRange >= distance)
                        {
                            if (distance < tempDistance)
                            {
                                tempTopScore = aggroTable[entity];
                                target = entity;
                                tempDistance = distance;
                            }
                        }
                    }
                }
            }
        }
        if (stats.team == 2)
        {
            foreach (GameObject entity in gameEventManager.enemies)
            {
                float distance = Vector2.Distance(entity.transform.position, transform.position);
                if (aggroTable.ContainsKey(entity))
                {
                    if (distance < tempDistance)
                    {
                        tempTopScore = aggroTable[entity];
                        target = entity;
                        tempDistance = distance;
                    }
                }
            }
        }
    }
    private void OnDisable()
    {
        Unsubscribe();
    }

    public Vector2 GetTargetPosition()
    {
        return target.transform.position;
    }
}
