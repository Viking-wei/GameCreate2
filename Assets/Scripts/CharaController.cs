using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.SceneManagement;

public class CharaController : MonoBehaviour
{
    [Header("BasicSettings")] 
    public float speed = 2f;
    public Transform birthPosition;
    
    [HideInInspector] 
    public PlayerInput playerInput;
    private const float Gravity = 9.8f;

    private const string TalkPrompt = "交谈";
    private const string CollectionPrompt = "拾取";
    private const string PortalPrompt = "进入";
    private const string InvadePrompt = "入侵";

    private Vector2 _moveDir;
    private bool _isGrounded;
    private CharacterController _controller;
    private Animator _anim;
    private SpriteRenderer _sR;
    private GameObject _npc;
    private bool _isAllowToChat;
    private bool _isAllowToEnterPlane;
    private bool _isAllowToUsePortal;
    private bool _isAllowToTake;
    private GameObject _collectionObject;
    private HackerPropertyController _hackerPropertyController;
    private GameObject _triggeredGameObject;
    private GameManager _gameManagerInstance;


    public static Action<Vector3, Vector3, DialogStorage> InvokeChat;
    public static Action<string> ShowPrompt;
    public static Action ClosePrompt;


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
        _sR = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        transform.position=GameManager.Instance.playerPosition==Vector3.zero?
            birthPosition.position:
            GameManager.Instance.playerPosition;
        _gameManagerInstance= GameManager.Instance;
    }

    private void FixedUpdate()
    {
        //Animation processing
        _anim.SetFloat("WLAR", Mathf.Abs(_moveDir.x));
        _anim.SetFloat("WFAB", _moveDir.y);

        if (_moveDir.x > 0.01) _sR.flipX = true;
        if (_moveDir.x < -0.01) _sR.flipX = false;
        
        _isGrounded=Physics.Raycast(transform.position,Vector3.down,_controller.height*0.5f+0.1f);

        if (!_isGrounded)
            _controller.Move(Gravity*Time.fixedDeltaTime*Vector3.down);
        var trueDir=new Vector3(_moveDir.x,0,_moveDir.y);
        _controller.Move(trueDir * (speed * Time.fixedDeltaTime));
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
            _hackerPropertyController = other.transform.parent.GetComponent<HackerPropertyController>();
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
            _triggeredGameObject = other.transform.parent.gameObject;
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
            switch (_hackerPropertyController.hackerProperty)
            {
                case HackerProperty.None:
                    Debug.Log("None hacker");
                    break;  
                case HackerProperty.Unlocked:
                    _hackerPropertyController.UnLockedAllTriggeredPortals();
                    break;
                case HackerProperty.End:
                    Debug.Log("Game End");
                    break;
                default:
                    Debug.Log("noting need to deal with");
                    break;
            }
            GameManager.Instance.EnterPlane(_hackerPropertyController.targetPlaneIndex);
        }
        else if (_isAllowToUsePortal)
        {
            ClosePrompt?.Invoke();
            PortalSettings portalSettings= _triggeredGameObject.GetComponentInParent<PortalSettings>();
            if(portalSettings.TryGetTargetPosition(_triggeredGameObject,out var targetPosition))
                transform.position = targetPosition;
            else
                _gameManagerInstance.SendMessageToUI("上锁的传送门!");
                
        }
        else if (_isAllowToTake)
        {
            _collectionObject.SetActive(false);
            ClosePrompt?.Invoke();
            _gameManagerInstance.SendMessageToUI("收集到一个文件");
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