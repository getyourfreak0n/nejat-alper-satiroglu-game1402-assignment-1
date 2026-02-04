using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    public void OnCollect()
    {
        Debug.Log("Coin collected");
        Destroy(gameObject);

    }

}