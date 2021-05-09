using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitArrow : MonoBehaviour
{
    GameEventManager gameEventManager;
    public float showTimeAfterLevelEnter;
    private bool showing;
    private bool inCombat;
    // Start is called before the first frame update
    private void Awake()
    {
        gameEventManager = GameObject.Find("Game Manager").GetComponent<GameEventManager>();
    }

    void Start()
    {
        Subscribe();
        showing = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        StartCoroutine(ShowInTime());

    }
    private void OnDisable()
    {
        Unsubscribe();
    }

    // Update is called once per frame
    void Update()
    {
        if (showing)
        {
            transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector2 difference = GameObject.FindGameObjectWithTag("Exit").transform.position - transform.position;
            if (difference.magnitude < 10)
                GetComponentInChildren<SpriteRenderer>().enabled = false;
            else if (!inCombat)
                GetComponentInChildren<SpriteRenderer>().enabled = true;

            difference.Normalize();
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        }
    }

    private void ExitLevel()
    {
        showing = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        StopAllCoroutines();
        StartCoroutine(ShowInTime());
    }
    private void StartCombat()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        inCombat = true;

    }
    private void EndCombat()
    {
        if (showing)
            GetComponentInChildren<SpriteRenderer>().enabled = true;
        inCombat = false;

    }

    IEnumerator ShowInTime()
    {
        yield return new WaitForSeconds(showTimeAfterLevelEnter);
        if (gameEventManager.combatOn == false)
            GetComponentInChildren<SpriteRenderer>().enabled = true;
        showing = true;
    }

    private void Subscribe()
    {
        gameEventManager.OnExitLevel += ExitLevel;
        gameEventManager.OnCombatStart += StartCombat;
        gameEventManager.OnCombatEnd += EndCombat;
    }

    public void Unsubscribe()
    {
        gameEventManager.OnExitLevel -= ExitLevel;
        gameEventManager.OnCombatStart -= StartCombat;
        gameEventManager.OnCombatEnd -= EndCombat;
    }
}
