using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public int mouseSensitivity;
    public Transform playerBody;
    private float pitch = 0;
    private float yaw = 0;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerBody = transform.parent;
    }

    void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);

            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -75, 75);
            transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        }

    }
}
