using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeView : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera TPV_Cam;
    [SerializeField] CinemachineVirtualCamera FPV_Cam;
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] Transform playerBody;

    private float xRotation = 0f;

    private void OnEnable()
    {
        CameraSwitcher.Register(FPV_Cam);
        CameraSwitcher.Register(TPV_Cam);

        CameraSwitcher.SwitchCam(FPV_Cam);
    }

    private void OnDisable()
    {
        CameraSwitcher.Unregister(TPV_Cam);
        CameraSwitcher.Unregister(FPV_Cam);
    }

    void Update()
    {
        HandleCameraSwitch();
        HandleMouseLook();
    }

    private void HandleCameraSwitch()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            if (CameraSwitcher.inFPV)
                CameraSwitcher.SwitchCam(TPV_Cam);
            else
                CameraSwitcher.SwitchCam(FPV_Cam);
        }
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the character horizontally
        playerBody.Rotate(Vector3.up * mouseX);

        // Calculate vertical rotation for the camera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp to prevent over-rotation

        // Apply vertical rotation to the camera
        if (CameraSwitcher.inFPV)
        {
            FPV_Cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            FPV_Cam.transform.rotation = Quaternion.Euler(xRotation, playerBody.eulerAngles.y, 0f);
        }
    }
}
