using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHoamToClosestEnemy : MonoBehaviour
{
    GameEventManager gameEventManager;
    AbilityEvents events;
    [SerializeField] private GameObject target;
    [SerializeField] private float projectileSpeed;

    [SerializeField] private float orbitSpeed;


    [SerializeField] private float triggerDistance;
    private bool targetFound = false;

    private void Awake()
    {
        gameEventManager = GameObject.Find("Game Manager").GetComponent<GameEventManager>();
        events = GetComponent<AbilityEvents>();
    }
    private void LateUpdate()
    {

        if (target == null)
        {
            GameObject source = events._abilityCastSource;
            if (source != null && !targetFound) transform.RotateAround(source.transform.position, Vector3.forward, orbitSpeed * Time.deltaTime);
            if (events._abilityCastSource.GetComponent<EntityStats>().team == 2)
            {
                foreach (GameObject enemy in gameEventManager.enemies)
                {
                    if (Vector2.Distance(transform.position, enemy.transform.position) <= triggerDistance)
                    {
                        gameObject.transform.parent = null;
                        target = enemy;
                        targetFound = true;
                    }
                }
            }
            if (events._abilityCastSource.GetComponent<EntityStats>().team == 1)
            {
                foreach (GameObject enemy in gameEventManager.allies)
                {
                    if (Vector2.Distance(transform.position, enemy.transform.position) <= triggerDistance)
                    {
                        gameObject.transform.parent = null;
                        target = enemy;
                    }
                }
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = ((Vector2)target.transform.position - (Vector2)transform.position).normalized * projectileSpeed;

        }

        
    }
}
