using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KIRBYMOVETEST : MonoBehaviour
{
    EntityTargetingSystem targetingSystem;

    // Start is called before the first frame update
    void Start()
    {
        targetingSystem = GetComponent<EntityTargetingSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetingSystem.target != null)
        {
            GetComponent<Rigidbody2D>().velocity = (targetingSystem.target.transform.position - transform.position).normalized * 10;
        }
    }
}
