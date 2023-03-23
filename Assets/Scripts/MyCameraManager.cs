using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MyCameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera[] cinemachineVirtualCameras;
    Transform targetCharacter;

    void Start()
    {
        targetCharacter = CharaController.Instance.transform;
        if (targetCharacter == null)
            Debug.LogWarning("no player there!");

        InitCamera();
    }

    void InitCamera()
    {
        CharaController.Instance.myCameraManager = this;

        if (cinemachineVirtualCameras.Length == 0)
        {
            Debug.LogWarning("have no main virtual camera!");
        }
        else
        {
            CinemachineVirtualCamera mainCamera = cinemachineVirtualCameras[0];
            mainCamera.Follow =targetCharacter;
            mainCamera.LookAt=targetCharacter;
        }
    }
    public void SetActiveCamera(int cameraIndex)
    {
        int priorityNum = cinemachineVirtualCameras.Length;

        if(cameraIndex>priorityNum-1)
        Debug.LogWarning("haven't set the target camera!");
        
        foreach (CinemachineVirtualCamera cine in cinemachineVirtualCameras)
        {
            cine.Priority = priorityNum;
        }
        cinemachineVirtualCameras[cameraIndex].Priority = priorityNum + 1;
    }
}
