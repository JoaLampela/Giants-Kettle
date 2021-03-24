using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDie : MonoBehaviour
{
    EntityEvents events;
    GameEventManager gameManager;

    private void Awake()
    {
        events = GetComponent<EntityEvents>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        events.OnDie += Die;
    }

    private void Unsubscribe()
    {
        events.OnDie -= Die;
    }


    private void Die()
    {
        StartCoroutine(DieLater());
        gameObject.transform.localScale = new Vector3(1.7f, 0.3f, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    IEnumerator DieLater()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
