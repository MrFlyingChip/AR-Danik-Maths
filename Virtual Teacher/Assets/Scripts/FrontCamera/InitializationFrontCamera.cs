using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class InitializationFrontCamera : MonoBehaviour {

    private bool changedCamera;

    // Update is called once per frame
    void Update()
    {
        if (!changedCamera)
        {
            ChangeCamera();
        }
    }

    public void ChangeCamera()
    {
        CameraDevice.Instance.Stop();
        CameraDevice.Instance.Deinit();
        CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_FRONT);
        CameraDevice.Instance.SelectVideoMode(CameraDevice.CameraDeviceMode.MODE_OPTIMIZE_QUALITY);
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        CameraDevice.Instance.Start();
        changedCamera = true;
    }


}
