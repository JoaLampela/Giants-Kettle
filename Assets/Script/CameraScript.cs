using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothSpeed = 0.125f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 position = transform.position;

        position.x = playerTransform.position.x;
        position.y = playerTransform.position.y;

        //sets the player position (x & y) as the position of the camera
        transform.position = position;

    }
}
