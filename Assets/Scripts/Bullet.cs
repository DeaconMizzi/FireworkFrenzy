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
        if (timer > bulletLife)
        {
            Destroy(gameObject);
            return;
        }

        timer += Time.deltaTime;
        transform.position = Movement(timer);
    }

    private Vector2 Movement(float timer)
    {
        // Moves in the bullet's "up" direction (rotate the bullet prefab when spawning)
        Vector2 direction = transform.up;
        return spawnPoint + direction * speed * timer;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            ShieldController playerController = collision.gameObject.GetComponent<ShieldController>();

            if (playerController != null && playerController.shieldActive)
            {
                Destroy(gameObject); // Bullet blocked by shield
            }
            else
            {
                Destroy(collision.gameObject); // Player is hit
            }
        }
    }
}
