using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnJump;
    
    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        _playerInputActions.Player.Jump.performed += Jump_performed;
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        OnJump?.Invoke(this, EventArgs.Empty);
        Debug.Log("Jump");
    }

    private void OnDestroy()
    {
        _playerInputActions.Dispose();
    }


    public Vector2 GetMovementVectorNormalized()
    {
        return _playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
    }
}
