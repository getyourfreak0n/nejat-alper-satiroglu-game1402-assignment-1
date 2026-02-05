using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;       // Horizontal movement speed
    [SerializeField] private float jumpForce = 15f;       // Force applied when jumping
    [SerializeField] private InputManager inputManager;   // Input manager reference

    private float _horizontalInput;       // Stores horizontal input (-1 to 1)
    private Rigidbody2D _playerRb;        // Rigidbody2D component reference
    private bool _isOnGround;             // True if player is touching the ground

    [Header("Ground Check Settings")]
    [SerializeField] private LayerMask groundLayer;       // Layer considered as ground
    [SerializeField] private Vector2 startPointOffset = new Vector2(0f, -0.5f); // Raycast offset
    [SerializeField] private float groundCheckDistance = 0.3f; // Raycast length

    [Header("Coyote Time Settings")]
    [SerializeField] private float coyoteTime = 0.2f;     // Time allowed to jump after leaving ground
    private float coyoteTimeCounter;                      // Countdown for coyote time

    void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>(); // Get Rigidbody2D
    }

    void OnEnable()
    {
        // Subscribe to input events
        inputManager.OnJump += HandleJumpInput;
        inputManager.OnMove += HandleMoveInput;
    }

    void OnDisable()
    {
        // Unsubscribe from input events
        inputManager.OnJump -= HandleJumpInput;
        inputManager.OnMove -= HandleMoveInput;
    }

    // Called when jump input is pressed
    void HandleJumpInput()
    {
        if (_playerRb == null) return;

        // Check if player is grounded or still within coyote time
        if (_isOnGround || coyoteTimeCounter > 0f)
        {
            _playerRb.linearVelocity = new Vector2(_playerRb.linearVelocity.x, jumpForce); // Apply vertical velocity
            coyoteTimeCounter = 0f; // Reset coyote timer after jump
        }
    }

    // Called when horizontal input changes
    void HandleMoveInput(float value)
    {
        _horizontalInput = value;
    }

    void FixedUpdate()
    {
        GroundCheck();     // Check if player is grounded
        HandleMovement();  // Apply horizontal movement
    }

    // Checks if the player is on the ground
    void GroundCheck()
    {
        Vector2 origin = (Vector2)transform.position + startPointOffset;

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        _isOnGround = hit.collider != null; // True if ray hits ground

        // Reset or decrease coyote time counter
        if (_isOnGround)
            coyoteTimeCounter = coyoteTime;   // Reset when touching ground
        else
            coyoteTimeCounter -= Time.fixedDeltaTime; // Countdown in air

        // Draw ray in editor for debugging
        Debug.DrawRay(origin, Vector2.down * groundCheckDistance,
            _isOnGround ? Color.green : Color.red);
    }

    // Applies horizontal movement
    void HandleMovement()
    {
        if (_playerRb == null) return;

        _playerRb.linearVelocity = new Vector2(
            _horizontalInput * moveSpeed,   // X velocity
            _playerRb.linearVelocity.y            // Keep current Y velocity
        );
    }
}
