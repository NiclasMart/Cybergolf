using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

    public static CameraController instance { get; private set; }

    public CinemachineFreeLook ballCam;
    public CinemachineVirtualCamera specCam;

    private void Start() 
    {
        instance = this;
    }

    public void SwitchToSpecView()
    {
        ballCam.Priority = 0;
        specCam.Priority = 100;
    }

    public void SwitchToBallView()
    {
        ballCam.Priority = 100;
        specCam.Priority = 0;
    }

    //sets update Methode of camera
    public void SetCameraUpdateSettings(CinemachineBrain.UpdateMethod method)
    {
        CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
        brain.m_UpdateMethod = method;

        
        if (method == CinemachineBrain.UpdateMethod.LateUpdate) {
            brain.m_IgnoreTimeScale = true;
        }
        else {
            //camera can move even if time scale is 0
            brain.m_IgnoreTimeScale = false;
        }
    }

    //locks camera while ball hit in Y direction
    public void LockCamera()    
    {
        ballCam.m_YAxis.m_InputAxisName = "";
        ballCam.m_YAxis.m_InputAxisValue = 0;
    }

    //delocks camera after hit
    public void DeLockCamera() 
    {
        ballCam.m_YAxis.m_InputAxisName = "Mouse Y";
    }
}
