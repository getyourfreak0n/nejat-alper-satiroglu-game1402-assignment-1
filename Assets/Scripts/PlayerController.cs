using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private InputManager inputManager;

    private float _horizontalInput;
    private Rigidbody2D _playerRb;

    void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        inputManager.OnJump += HandleJumpInput;      
    }

    void OnDisable()
    {
        inputManager.OnJump -= HandleJumpInput;
        inputManager.OnMove -= HandleMoveInput;
    }

    // Jump input callback
    void HandleJumpInput()
    {
        if (_playerRb != null)
            _playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // Move input callback
    void HandleMoveInput(float value)
    {
        _horizontalInput = value;
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (_playerRb == null) return;

        _playerRb.linearVelocity = new Vector2(_horizontalInput * moveSpeed, _playerRb.linearVelocity.y);
    }
}