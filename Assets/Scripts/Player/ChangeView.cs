using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeView : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera TPV_Cam;
    [SerializeField] CinemachineVirtualCamera FPV_Cam;

    private void OnEnable()
    {
        CameraSwitcher.Register(TPV_Cam);
        CameraSwitcher.Register(FPV_Cam);
    }

    private void OnDisable()
    {
        CameraSwitcher.Unregister(TPV_Cam);
        CameraSwitcher.Unregister(FPV_Cam);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            if (CameraSwitcher.inFPV)
                CameraSwitcher.SwitchCam(TPV_Cam);
            else
                CameraSwitcher.SwitchCam(FPV_Cam);
        }
    }
}
