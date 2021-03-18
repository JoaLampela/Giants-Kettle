using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float smoothSpeed = 5f;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //set the camera to the players position
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 position = transform.position;
        
        position.x = playerTransform.position.x;
        position.y = playerTransform.position.y;
        //smoothly transforms from the cameras position to the players position
        position = Vector3.Lerp(transform.position, position, smoothSpeed * Time.fixedDeltaTime);

        //sets the player position (x & y) as the position of the camera
        transform.position = position;
    }
}
