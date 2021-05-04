using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHomeToClosestEnemy : MonoBehaviour
{
    GameEventManager gameEventManager;
    AbilityEvents events;
    [SerializeField] private GameObject target;

    [SerializeField] private float orbitSpeed;

    public GameObject source;

    [SerializeField] private float triggerDistance;
    private bool targetFound = false;
    private float speed = 10;
    public Vector2 sourcePos;
    Rigidbody2D rb;


    private void Start()
    {
        //source = events._abilityCastSource;
    }
    private void Awake()
    {
        gameEventManager = GameObject.Find("Game Manager").GetComponent<GameEventManager>();
        events = GetComponent<AbilityEvents>();
    }
    private void LateUpdate()
    {

        if (target == null)
        {

            if (source != null && !targetFound)
            {
                transform.RotateAround(source.transform.position, Vector3.forward, orbitSpeed * Time.deltaTime);
                sourcePos = source.transform.position;
            }
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
            if (!gameObject.GetComponent<Rigidbody2D>())
            {
                rb = gameObject.AddComponent<Rigidbody2D>();
                gameObject.GetComponent<CircleCollider2D>().enabled = true;
                rb.gravityScale = 0;
                rb.freezeRotation = true;
                
            }
            
            gameObject.GetComponent<Rigidbody2D>().velocity = ((Vector2)target.transform.position - (Vector2)transform.position).normalized * speed;
        }

        
    }
}
