using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class CameraSwitcher 
{
    private static List<CinemachineVirtualCamera> cams = new List<CinemachineVirtualCamera>();
    private static CinemachineVirtualCamera ActvCam = null;
    public static bool inFPV = true;

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
