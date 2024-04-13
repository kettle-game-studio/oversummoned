using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float CameraSpeed = 10;
    public float RotationSpeed = 10;

    void FixedUpdate()
    {
        transform.Translate(
            new Vector3(
                Input.GetAxis("Horizontal") * Time.deltaTime * CameraSpeed,
                0,
                Input.GetAxis("Vertical") * Time.deltaTime * CameraSpeed
            ),
            Space.Self
        );

        if (Input.GetMouseButton(0)) {
            this.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * RotationSpeed, Space.World);
        }
    }
}
