using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;

    public float distanceFromTarget = 5f;
    public float rotationSpeed = 4f;
    public bool enableCamRotation = true;

    private void Update()
    {

        EnableCameraRotation();
        transform.position = target.position - transform.forward * distanceFromTarget;


    }

    private void EnableCameraRotation()
    {
        if (Input.GetMouseButton(0) && enableCamRotation)
        {
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");

            transform.Rotate(Vector3.up, h * 10, Space.World);
            transform.Rotate(transform.right, v * 10, Space.World);
        }
        if (target == null)
        {
            return;
        }
    }

}
