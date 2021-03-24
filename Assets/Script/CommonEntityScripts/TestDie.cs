using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDie : MonoBehaviour
{
    EntityEvents events;
    GameEventManager gameManager;
    float counterX = 0;
    float counterY = 0;
    bool squishing = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (squishing)
        {
            counterX += Time.deltaTime * 10;
            counterY += Time.deltaTime * 10;
            float scaleX = Mathf.Lerp(1, 2, counterX);
            float scaleY = Mathf.Lerp(1, 0.3f, counterY);
            gameObject.transform.localScale = new Vector3(scaleX, scaleY, 1);
        }

    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    IEnumerator DieLater()
    {
        squishing = true;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
