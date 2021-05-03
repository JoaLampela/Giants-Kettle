using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiator : MonoBehaviour
{
    public void Paint(GameObject brushTarget, Vector3 worldPosition, GameObject prefab)
    {
        Instantiate(prefab, worldPosition, Quaternion.identity);
    }
}
