using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEfectManager : MonoBehaviour
{
    EntityStats stats;
    private bool bubbleOn = false;
    [SerializeField] private GameObject effect;
    GameObject currentBubble;
    private void Awake()
    {
        stats = GetComponent<EntityStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stats.currentShield > 0 && !bubbleOn) {
            bubbleOn = true;
            currentBubble = Instantiate(effect, transform.position, transform.rotation, transform);
        }
        else if(stats.currentShield <= 0)
        {
            bubbleOn = false;
            Destroy(currentBubble);
        }
    }
}
