using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public bool shieldActive = false;
    public float shieldDuration = 5f;
    private int collectedCoins = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject); // Remove the collected coin
            collectedCoins++;

            if (collectedCoins >= 10)
            {
                ActivateShield();
            }
        }
        else if (other.CompareTag("Bullet") && !shieldActive)
        {
            Destroy(gameObject); // Destroy the player on collision with a bullet
        }
    }

    void Update()
    {
        if (shieldActive)
        {
            shieldDuration -= Time.deltaTime;
            if (shieldDuration <= 0)
            {
                DeactivateShield();
            }
        }
    }

    void ActivateShield()
    {
        shieldActive = true;
        // Add visual or audio feedback for the shield activation if needed
    }

    void DeactivateShield()
    {
        shieldActive = false;
        collectedCoins = 0; // Reset collected coins after shield deactivates
        shieldDuration = 5f; // Reset shield duration
    }
}