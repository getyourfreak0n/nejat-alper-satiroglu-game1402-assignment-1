using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private InputManager inputManager;

    private float _horizontalInput;
    private Rigidbody2D _playerRb;
    private bool isOnGround;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 startPointOffset = new Vector2(0f, -0.5f);
    [SerializeField] private float groundCheckDistance = 0.3f;

    void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        inputManager.OnJump += HandleJumpInput;
        inputManager.OnMove += HandleMoveInput;
    }

    void OnDisable()
    {
        inputManager.OnJump -= HandleJumpInput;
        inputManager.OnMove -= HandleMoveInput;
    }

    void HandleJumpInput()
    {
        if (_playerRb == null) return;
        if (!isOnGround) return;

        _playerRb.AddForceY(jumpForce, ForceMode2D.Impulse);
    }

    void HandleMoveInput(float value)
    {
        _horizontalInput = value;
    }

    void FixedUpdate()
    {
        GroundCheck();
        HandleMovement();
    }

    void GroundCheck()
    {
        Vector2 origin = (Vector2)transform.position + startPointOffset;

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        isOnGround = hit.collider != null;

        Debug.DrawRay(origin, Vector2.down * groundCheckDistance,
            isOnGround ? Color.green : Color.red);
    }

    void HandleMovement()
    {
        if (_playerRb == null) return;

        _playerRb.linearVelocity = new Vector2(
            _horizontalInput * moveSpeed,
            _playerRb.linearVelocity.y
        );
    }
}