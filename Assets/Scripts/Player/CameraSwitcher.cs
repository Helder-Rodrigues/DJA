using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting.FullSerializer;

public static class CameraSwitcher 
{
    public static List<CinemachineVirtualCamera> cams = new List<CinemachineVirtualCamera>();
    public static CinemachineVirtualCamera ActvCam = null;
    public static bool inFPV = false; // "SwitchCam()" is called at start so the value is "true"

    public static void SwitchCam(CinemachineVirtualCamera cam)
    {
        cam.Priority = 10;
        ActvCam = cam;

        foreach (CinemachineVirtualCamera c in cams)
            if(c != cam && c.Priority != 0)
                c.Priority = 0;

        inFPV = !inFPV;
    }

    public static void Register(CinemachineVirtualCamera cam) => cams.Add(cam);

    public static void Unregister(CinemachineVirtualCamera cam) => cams.Remove(cam);
}
