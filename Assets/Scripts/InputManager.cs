using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions _playerInputActions; // Generated input actions from the Input System

    // Events that other scripts (like PlayerController) can subscribe to
    public System.Action OnJump;         // Triggered when jump button is pressed
    public System.Action<float> OnMove;  // Triggered when horizontal input changes

    void Awake()
    {
        // Create a new instance of the PlayerInputActions asset
        _playerInputActions = new PlayerInputActions();

        // Enable input so it starts listening
        _playerInputActions.Enable();
    }

    void OnEnable()
    {
        // Subscribe to Jump input event
        _playerInputActions.Player.Jump.performed += OnJumpPressed;

        // Horizontal input could also be subscribed here, but it's currently commented out
        //_playerInputActions.Player.Horizontal.performed += OnMovement;
    }

    void OnDisable()
    {
        // Unsubscribe from Jump event to prevent memory leaks or errors
        _playerInputActions.Player.Jump.performed -= OnJumpPressed;

        // If Horizontal subscription is used, unsubscribe here
        //_playerInputActions.Player.Horizontal.performed -= OnMovement;

        // Disable input when this object is disabled
        _playerInputActions.Disable();
    }

    // Called when Jump input is pressed
    void OnJumpPressed(InputAction.CallbackContext context)
    {
        // Invoke the event so any subscriber (PlayerController) reacts
        OnJump?.Invoke();
    }

    // Handles horizontal movement input
    void OnMovement()
    {
        // Read horizontal value (e.g., -1 for left, +1 for right) and invoke event
        OnMove?.Invoke(_playerInputActions.Player.Horizontal.ReadValue<float>());
    }

    void Update()
    {
        // Continuously read horizontal input every frame
        // This is used instead of the commented-out performed subscription
        OnMovement();
    }
}
