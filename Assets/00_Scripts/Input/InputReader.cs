using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputActions;

public class InputReader : MonoBehaviour ,IPlayerActions
{
    private PlayerInputActions _inputActions;

    public Action OnClickReset;

    public void Awake()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerInputActions();
            _inputActions.Player.SetCallbacks(this);
        }

        _inputActions.Enable();
    }
    public void OnReset(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            OnClickReset?.Invoke();
        }
    }

  
}
