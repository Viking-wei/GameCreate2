using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player Input")]
public class PlayerInput_2D : ScriptableObject, InputActions.IGameplayActions
{
    public event UnityAction<Vector2>onMove=delegate { };
    public event UnityAction onStopMove = delegate { };
    public event UnityAction onFire = delegate { };
    public event UnityAction onStopFire = delegate { };

    InputActions inputActions;
    void OnEnable()
    {
        inputActions=new InputActions();
        inputActions.Gameplay.SetCallbacks(this);
    }
    void  OnDisable()
    {
        DisableAllInputs();
    }

    public void DisableAllInputs()
    {
        inputActions.Gameplay.Disable();
    }
    public void EnableGameplayInput()
    {
        inputActions.Gameplay.Enable();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
       if(context.performed)
        {
            onMove.Invoke(context.ReadValue<Vector2>());
        }
        if (context.canceled)
        {
            onStopMove.Invoke();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onFire.Invoke();
        }
        if (context.canceled)
        {
            onStopFire.Invoke();
        }
    }
}
