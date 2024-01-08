using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinSpawner coinSpawner;

    void Start()
    {
        coinSpawner = GameObject.Find("CoinManager").GetComponent<CoinSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            coinSpawner.IncrementCoinCounter();
            Destroy(gameObject);
        }
    }
}