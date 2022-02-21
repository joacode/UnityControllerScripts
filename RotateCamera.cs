using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private float rotationSpeed = 500.0f;

    public GameObject player;

    private Vector3 pos = new Vector3(0.0f, 3f, -3f);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // rotate camera around focal point
        float mouseAxis = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseAxis * rotationSpeed * Time.deltaTime);

        // move focal point with player

        transform.position = player.transform.position;
    }
}
