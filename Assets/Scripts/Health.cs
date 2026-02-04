using UnityEngine;

public class Health : MonoBehaviour, ICollectible
{
    public void OnCollect()
    {
        Debug.Log("Health Collected");
        Destroy(gameObject);

    }
}
