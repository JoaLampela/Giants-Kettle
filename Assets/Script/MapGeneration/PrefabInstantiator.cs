using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiator : MonoBehaviour
{
    public void Paint(GameObject prefab, Vector3 worldPosition,  Transform targetTrans)
    {
        Instantiate(prefab, worldPosition, Quaternion.identity, targetTrans);
    }
}
