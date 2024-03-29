using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 1f;  // Defines how long before the bullet is destroyed
    public float rotation = 0f;
    public float speed = 1f;

    private Vector2 spawnPoint;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > bulletLife) Destroy(gameObject);
        timer += Time.deltaTime;
        transform.position = Movement(timer);
    }

    private Vector2 Movement(float timer)
    {
        // Moves right according to the bullet's rotation
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            // If the bullet collides with a border, set the velocity to zero
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            ShieldController playerController = collision.gameObject.GetComponent<ShieldController>();

            // Check if the player has a shield active
            if (playerController != null && playerController.shieldActive)
            {
                // Optionally, you can add some visual or audio feedback for the bullet hitting the shield
                Destroy(gameObject); // Destroy the bullet
            }
            else
            {
                // If the player does not have a shield active, destroy the player
                Destroy(collision.gameObject);
            }
        }
    }
}