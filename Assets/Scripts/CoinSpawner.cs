using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Assign the coin prefab in the inspector
    public TMP_Text coinCounterText; // Reference to a UI Text component to display the coin count
    public float spawnInterval = 2.0f; // Interval between spawns
    private int coinCounter = 0;

    void Start()
    {
        StartCoroutine(SpawnCoin());
        UpdateCoinCounterText();
    }

    IEnumerator SpawnCoin()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4.5f, 4.5f));
            GameObject newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void IncrementCoinCounter()
    {
        coinCounter++;
        UpdateCoinCounterText();
    }

    void UpdateCoinCounterText()
    {
        coinCounterText.text = "Coins: " + coinCounter.ToString();
    }
}