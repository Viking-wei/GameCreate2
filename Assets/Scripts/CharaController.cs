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

    private Vector2 _moveDir;
    private Rigidbody _rigid;
    private Animator _anim;
    private SpriteRenderer _sR;
    private bool _isOnGround;

    private GameObject _npc;
    public static bool isAllowChat;
    public static Action<Vector3, Vector3, DialogStorage> InvokeChat;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _sR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        //moving
        if (_isOnGround && !GameManager.Instance.isChatting)
        {
            //Animation processing
            _anim.SetFloat("WLAR", Mathf.Abs(_moveDir.x));
            _anim.SetFloat("WFAB", _moveDir.y);

            if (_moveDir.x > 0.01) _sR.flipX = true;
            if (_moveDir.x < -0.01) _sR.flipX = false;

            _rigid.velocity = new Vector3(_moveDir.x * speed, _rigid.velocity.y, _moveDir.y * speed);

        }
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

            _npc = other.gameObject;
            Debug.Log(isAllowChat.ToString() + " " + _npc.name);
        }
        else if(other.CompareTag("EnterPlaneTrigger"))
        {
            //TODO:
        }
        else if(other.CompareTag("Collections"))
        {
            //TODO:
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            isAllowChat = false;
            Debug.Log(isAllowChat.ToString());
        }
        //TODO:
    }

    //character move callback func
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDir = context.ReadValue<Vector2>();
        _moveDir = _moveDir.normalized;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started && isAllowChat && !GameManager.Instance.isChatting)
        {
            //stop animation when chatting
            _rigid.velocity=Vector3.zero;
            _anim.SetFloat("WLAR", 0);
            _anim.SetFloat("WFAB", 0);

            var dsd = GameManager.Instance.dialogStorageDictionary;

            Vector3 dp = _npc.transform.GetChild(0).position;

            if (dsd.ContainsKey(_npc.name))
                InvokeChat?.Invoke(transform.GetChild(0).position, dp, dsd[_npc.name]);
            else
                Debug.LogWarning("Haven't add this to dictionary!!!");
        }
    }
}

