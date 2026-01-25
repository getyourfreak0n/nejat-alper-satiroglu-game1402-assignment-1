using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    public System.Action OnJump;
    public System.Action<float> OnMove;
    
    void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }

    void OnEnable()
    {
        _playerInputActions.Player.Jump.performed += OnJumpPressed;
        //_playerInputActions.Player.Horizontal.performed += OnMovement;
    }

    void OnDisable()
    {
        _playerInputActions.Player.Jump.performed -= OnJumpPressed;
        //_playerInputActions.Player.Horizontal.performed -= OnMovement;
        _playerInputActions.Disable();
    }

    void OnJumpPressed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }

    void OnMovement()
    {
        OnMove?.Invoke(_playerInputActions.Player.Horizontal.ReadValue<float>());
    }

    void Update()
    {
        OnMovement();
    }
}
