using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour
{
    private Camera _mainCamera;
    void Start()
    {
        _mainCamera=Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation=_mainCamera.transform.rotation;
    }
}
