using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[ExecuteAlways]
public class CameraDirector : MonoBehaviour
{
    [Required]public Transform target;
    public float distance;
    public float angle;
    private Vector3 _pos;
    private Vector3 _offSet;
    
    
    void LateUpdate()
    {
        //FIXME:remember move this to Start()
        _offSet=new Vector3(0,distance*Mathf.Sin(angle/180),-distance*Mathf.Cos(angle/57));

        transform.position=target.position+_offSet;
        transform.LookAt(target.transform);
    }
}
