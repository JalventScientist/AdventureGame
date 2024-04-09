using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public bool CameraEnabled = true;

    public Transform orientation;
    public Transform camHolder;

    public Camera UICamera;

    float xRotation;
    float yRotation;
    private float mouseX;
    private float mouseY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
       if(CameraEnabled)
        {
            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
    

    private void FixedUpdate()
    {
        if (CameraEnabled)
        {
            mouseX = Input.GetAxisRaw("Mouse X") * sensX;
            mouseY = Input.GetAxisRaw("Mouse Y") * sensY;
        }
    }

    public void SetOrientation(float x, float y, float z)
    {
        camHolder.rotation = Quaternion.Euler(x,y,z);
        orientation.rotation = Quaternion.Euler(x,y,z);
    }

    public void DoFov(float endValue, float UIModifier)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
        UICamera.DOFieldOfView(endValue * UIModifier, 0.25f);
    }

    public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }
}
