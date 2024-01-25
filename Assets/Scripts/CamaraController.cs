using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float angleX;
    public float angleY;
    public float radius = 1;
    private void Update()
    {
        angleX += Input.GetAxis("Mouse X");
        angleY = Mathf.Clamp(angleY += Input.GetAxis("Mouse Y"), -45, 0);
        radius = Mathf.Clamp(radius -= Input.mouseScrollDelta.y, 1, 10);
        if (angleX > 360)
        {
            angleX -= 360;
        }
        else if (angleX < 0)
        {
            angleX += 360;
        }
        Vector3 orbit = Vector3.forward * radius;
        orbit = Quaternion.Euler(angleY, angleX, 0) * orbit;
        transform.position = player.transform.position + orbit;
        transform.LookAt(player.transform.position);
    }
}
