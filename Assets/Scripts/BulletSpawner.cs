using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnRate = 2f; // Adjust spawn rate as needed
    public float spawnWidth = 5f; // Adjust spawn width within the screen
    public float bulletSpeed = 5f; // Adjust bullet speed
    public float yOffset = 1f; // Offset from the top edge

    void Start()
    {
        StartCoroutine(SpawnBullets());
    }

    IEnumerator SpawnBullets()
    {
        while (true)
        {
            // Calculate a random position along the top edge
            Vector2 spawnPosition = new Vector2(
                Random.Range(-spawnWidth / 2f, spawnWidth / 2f),
                transform.position.y + yOffset
            );

            // Instantiate a bullet prefab at the calculated position
            GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

            // Apply downward force to the bullet to make it move downwards
            newBullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * bulletSpeed;

            // Wait for the specified spawn rate before spawning the next bullet
            yield return new WaitForSeconds(spawnRate);
        }
    }

}
