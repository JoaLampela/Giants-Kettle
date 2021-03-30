using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingItemIcon : MonoBehaviour
{
    public Vector3 offset;


    void Update()
    {
        MoveObject(); 
    }

    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = -2;
        transform.position = pos;
    }
}
