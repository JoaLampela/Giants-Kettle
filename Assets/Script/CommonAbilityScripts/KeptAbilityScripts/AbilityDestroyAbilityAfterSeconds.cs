using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDestroyAbilityAfterSeconds : MonoBehaviour
{
    public float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyInSeconds(destroyTime));
    }

    // Update is called once per frame
    IEnumerator DestroyInSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
