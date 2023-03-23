using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharaController : Singleton<CharaController>
{
    public MyCameraManager myCameraManager;
    [Header("BasicSettings")]
    public float speed = 2f;
    public float downDistance = 9f;
    Vector2 _moveDir;
    Rigidbody _rigid;

    protected override void Awake()
    {
        base.Awake();
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //moving
        _rigid.velocity = new Vector3(_moveDir.x * speed, _rigid.velocity.y, _moveDir.y * speed);

    }

    //character enter buildings callback func
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PortalSame"))
        {
            int targetCameraIndex=other.GetComponent<PortalSameInfo>().targetCameraIndex;
            myCameraManager.SetActiveCamera(targetCameraIndex);

            transform.position -= new Vector3(0, downDistance, 0);
        }

        if (other.CompareTag("PortalDiffer"))
        {
            PortalDifferInfo portalDifferInfo = other.GetComponent<PortalDifferInfo>();
            ChangeScene(portalDifferInfo.differInfo.targetSceneIndex, portalDifferInfo.differInfo.targetDestination);
        }
    }
    void ChangeScene(int targetSceneIndex, Vector3 targetDestination)
    {
        StartCoroutine(MyGameManager.Instance.LoadScene(targetSceneIndex, targetDestination));
    }

    //character move callback func
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDir = context.ReadValue<Vector2>();
        _moveDir = _moveDir.normalized;
    }
}
