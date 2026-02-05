using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target;           // The player Transform the camera will follow
    public float smoothSpeed = 0.125f; // How quickly the camera interpolates to the target position
    public Vector3 offset;             // Offset from the player (usually above and behind, e.g., (0, 2, -10))

    void LateUpdate()
    {
        // Safety check: do nothing if target is not assigned
        if (target == null) return;

        // Calculate the desired camera position based on player's position + offset
        // Keep camera's Z position constant (important for 2D)
        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z) + offset;

        // Smoothly interpolate from current position to desired position
        // Lerp ensures smooth camera movement instead of snapping instantly
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;
    }
}
