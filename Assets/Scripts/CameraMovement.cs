using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float CameraSpeed = 10;
    public float RotationSpeed = 10;
    public float DesiredGrabbedDemonHeights = 3f;

    public Camera Camera;

    private DemonScript Demon;

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

        if (Input.GetMouseButton(1)) {
            this.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * RotationSpeed, Space.World);
        }

        if (Input.GetMouseButtonDown(0) && Demon == null) {
            var p0 = MouseRayStart();

            var didHit = Physics.Raycast(p0, Camera.transform.forward * 10, out var hitInfo);
            if (didHit) {
                var demon =  hitInfo.collider.GetComponent<DemonColliderScript>();
                if (demon != null) {
                    Demon = demon.Demon;
                    Demon.StartBeHeld();
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            if (Demon != null) {
                Demon.StopBeHeld();
                Demon = null;
            }
        }

        SetDemonPosition();
    }

    void SetDemonPosition() {
        if (Demon == null) return;

        var p0 = MouseRayStart();
        var v0 = Camera.transform.forward;
        var d = (DesiredGrabbedDemonHeights - p0.y) / v0.y;

        Demon.DemonBody.transform.position =  p0 + d * v0;
    }

    Vector3 MouseRayStart()
    {
        var clickXCoeff =  (2 * (Input.mousePosition.x / Camera.pixelWidth) - 1) * Camera.orthographicSize * Camera.aspect; 
        var clickYCoeff =  (2 * (Input.mousePosition.y / Camera.pixelHeight) - 1) * Camera.orthographicSize; 
        var rayOffset = Camera.transform.right * clickXCoeff + Camera.transform.up * clickYCoeff;
        return Camera.transform.position + rayOffset;
    }
}
