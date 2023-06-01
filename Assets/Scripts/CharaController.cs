using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody))]
public class CharaController : MonoBehaviour
{
    [Header("BasicSettings")] public float Speed = 2f;
    [HideInInspector] public PlayerInput playerInput;

    private const string TalkPrompt = "交谈";
    private const string CollectionPrompt = "拾取";
    private const string PortalPrompt = "进入";
    private const string InvadePrompt = "入侵";

    private Vector2 _moveDir;
    private Rigidbody _rigid;
    private Animator _anim;
    private SpriteRenderer _sR;
    private GameObject _npc;
    private bool _isOnGround;
    private bool _isAllowToChat;
    private bool _isAllowToEnterPlane;
    private bool _isAllowToUsePortal;
    private bool _isAllowToTake;
    private GameObject _collectionObject;
    private int _targetPlaneIndex;
    private Vector3 _targetPortalPosition;


    public static Action<Vector3, Vector3, DialogStorage> InvokeChat;
    public static Action<string> ShowPrompt;
    public static Action ClosePrompt;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _sR = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        // transform.position = GameManager.Instance.playerPosition;
    }

    private void FixedUpdate()
    {
        //moving
        if (_isOnGround)
        {
            //Animation processing
            _anim.SetFloat("WLAR", Mathf.Abs(_moveDir.x));
            _anim.SetFloat("WFAB", _moveDir.y);

            if (_moveDir.x > 0.01) _sR.flipX = true;
            if (_moveDir.x < -0.01) _sR.flipX = false;

            _rigid.velocity = new Vector3(_moveDir.x * Speed, _rigid.velocity.y, _moveDir.y * Speed);
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
            _isAllowToChat = true;
            _npc = other.gameObject;
            ShowPrompt?.Invoke(TalkPrompt);
        }
        else if (other.CompareTag("EnterPlaneTrigger"))
        {
            _isAllowToEnterPlane = true;
            _targetPlaneIndex = other.GetComponent<EnterPlaneSettings>().TargetPlaneIndex;
            ShowPrompt?.Invoke(InvadePrompt);
        }
        else if (other.CompareTag("Collections"))
        {
            _isAllowToTake = true;
            _collectionObject = other.transform.parent.gameObject;
            ShowPrompt?.Invoke(CollectionPrompt);
        }
        else if (other.CompareTag("Portal"))
        {
            _isAllowToUsePortal = true;
            _targetPortalPosition = other.transform.parent.GetComponent<PortalSettings>().GetTargetPosition(other.gameObject);
            ShowPrompt?.Invoke(PortalPrompt);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isAllowToChat = false;
        _isAllowToEnterPlane = false;
        _isAllowToTake = false;
        _isAllowToUsePortal = false;
        ClosePrompt?.Invoke();
    }

    //character move callback func
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDir = context.ReadValue<Vector2>();
        _moveDir = _moveDir.normalized;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        if (_isAllowToChat)
        {
            ClosePrompt?.Invoke();
            playerInput.SwitchCurrentActionMap("Dialog");

            //stop animation when chatting
            _rigid.velocity = Vector3.zero;
            _anim.SetFloat("WLAR", 0);
            _anim.SetFloat("WFAB", 0);

            //deal with dialog
            var dsd = GameManager.Instance.dialogStorageDictionary;
            Vector3 dp = _npc.transform.GetChild(0).position;
            if (dsd.TryGetValue(_npc.name,out var value))
                InvokeChat?.Invoke(transform.GetChild(0).position, dp, value);
            else
                Debug.LogWarning("Haven't add this to dictionary!!!");
        }
        else if (_isAllowToEnterPlane)
        {
            ClosePrompt?.Invoke();
            Debug.Log("Plane");
            GameManager.Instance.EnterPlane(_targetPlaneIndex);
        }
        else if (_isAllowToUsePortal)
        {
            ClosePrompt?.Invoke();
            transform.position = _targetPortalPosition;
        }
        else if (_isAllowToTake)
        {
            ClosePrompt?.Invoke();
            Destroy(_collectionObject);
        }
    }

    private void OnDestroy()
    {
        if (InvokeChat != null)
        {
            var ic = InvokeChat.GetInvocationList();
            foreach (var a in ic)
            {
                InvokeChat -= a as Action<Vector3, Vector3, DialogStorage>;
            }

            var sp = ShowPrompt.GetInvocationList();
            foreach (var a in sp)
            {
                ShowPrompt -= a as Action<string>;
            }

            var cp = ClosePrompt.GetInvocationList();
            foreach (var a in cp)
            {
                ClosePrompt -= a as Action;
            }

            Debug.Log("委托清除完成");
        }
        else
        {
            Debug.Log("委托为空");
        }
    }
}