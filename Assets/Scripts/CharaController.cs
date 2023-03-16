using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharaController : MonoBehaviour
{
    [Header("BasicSettings")]
    public float speed=2f;
    Vector2 _moveDir;
    Rigidbody _rigid;

    void Awake()
    {
        _rigid=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //moving
        _rigid.velocity=new Vector3(_moveDir.x*speed,_rigid.velocity.y,_moveDir.y*speed);
        
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDir=context.ReadValue<Vector2>();
        _moveDir=_moveDir.normalized;
    }
    void OnTriggerEnter(Collider other)
    {
        int mid=MyGameManager.Instance.cinemachineVirtualCamera1.Priority;
        MyGameManager.Instance.cinemachineVirtualCamera1.Priority=MyGameManager.Instance.cinemachineVirtualCamera2.Priority;
        MyGameManager.Instance.cinemachineVirtualCamera2.Priority=mid;
    }
}
