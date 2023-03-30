using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


[RequireComponent(typeof(Rigidbody))]
public class CharaController : MonoBehaviour
{
    [Header("BasicSettings")]
    public float speed = 2f;

    Vector2 _moveDir;
    Rigidbody _rigid;
    bool _isOnGround;

    Vector3 _npcDialogPoint;
    public static bool isAllowChat;
    public static Action<Vector3,Vector3> InvokeChat;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //moving
        if (_isOnGround && !GameManager.Instance.isChatting)
            _rigid.velocity = new Vector3(_moveDir.x * speed, _rigid.velocity.y, _moveDir.y * speed);
    }

    private void OnCollisionStay(Collision collision)
    {
        _isOnGround = true;
    }
    private void OnCollisionExit(Collision collisionInfo)
    {
        _isOnGround = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            isAllowChat = true;
            _npcDialogPoint=other.transform.GetChild(0).position;
            Debug.Log(isAllowChat.ToString());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            isAllowChat = false;
            Debug.Log(isAllowChat.ToString());
        }
    }

    //character move callback func
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDir = context.ReadValue<Vector2>();
        _moveDir = _moveDir.normalized;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(isAllowChat&&!GameManager.Instance.isChatting&&context.started)
        {
            InvokeChat?.Invoke(transform.GetChild(0).position,_npcDialogPoint);
        }
    }
}

