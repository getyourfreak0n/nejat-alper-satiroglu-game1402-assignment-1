using UnityEngine;

public class FallCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering is the player
        if (other.CompareTag("Grabbable"))
        {
            Destroy(other.gameObject); // Destroy the player
            // Optionally: Trigger respawn, lose life, or play animation
        }
    }
}
