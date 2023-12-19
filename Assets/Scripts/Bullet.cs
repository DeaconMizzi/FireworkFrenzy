using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            // If the player collides with a border, set the velocity to zero
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            // If the player collides with a border, set the velocity to zero
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
