using UnityEngine;

public class MovingPlatformGrabber : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Grabbable"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Grabbable"))
        {
            other.transform.SetParent (null);
        }
    }


}
