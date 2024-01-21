using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }
    public enum MovementType { Free, LeftRight }

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;
    [SerializeField] private float changePositionTime = 5f;
    private Vector2 newPosition;

    private GameObject spawnedBullet;
    private float timer = 0f;
    private float changePositionTimer = 0f;
    private bool isMoving = false;

    public MovementType movementType = MovementType.Free;

    // Start is called before the first frame update
    void Start()
    {
        SetNewPosition();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        changePositionTimer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);

        if (changePositionTimer >= changePositionTime && !isMoving)
        {
            isMoving = true;
            SetNewPosition();
            StartCoroutine(MoveSpawner());
        }

        if (timer >= firingRate)
        {
            Fire();
            timer = 0;
        }
    }

    private IEnumerator MoveSpawner()
    {
        float movementTime = 1.0f; // Set the time for the spawner to move to the new position
        float elapsedTime = 0f;
        Vector2 startingPos = transform.position;

        while (elapsedTime < movementTime)
        {
            transform.position = Vector2.Lerp(startingPos, newPosition, (elapsedTime / movementTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = newPosition; // Ensure the final position is exact
        isMoving = false;
        changePositionTimer = 0f;
    }

    private void SetNewPosition()
    {
        Vector2 currentPosition = transform.position;

        if (movementType == MovementType.LeftRight)
        {
            // Use relative positioning based on screen width
            float screenWidthWorld = CalculateScreenWidthWorld();
            float randomX = Random.Range(-screenWidthWorld / 2f, screenWidthWorld / 2f);
            newPosition = new Vector2(currentPosition.x + randomX, currentPosition.y);

            // Clamp the new position to stay within camera bounds
            float halfSpawnerWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
            newPosition.x = Mathf.Clamp(newPosition.x, -screenWidthWorld / 2f + halfSpawnerWidth, screenWidthWorld / 2f - halfSpawnerWidth);
        }
        else
        {
            // Use relative positioning based on screen dimensions
            float screenWidthWorld = CalculateScreenWidthWorld();
            float screenHeightWorld = CalculateScreenHeightWorld();
            newPosition = new Vector2(Random.Range(-screenWidthWorld / 2f, screenWidthWorld / 2f),
                                       Random.Range(-screenHeightWorld / 2f, screenHeightWorld / 2f));
        }
    }

    private void Fire()
    {
        if (bullet)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<Bullet>().speed = speed;
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }

    private float CalculateScreenWidthWorld()
    {
        // Calculate screen width in world coordinates
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Vector2 zeroPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);
        float screenWidthWorld = Mathf.Abs(screenBounds.x - zeroPoint.x);
        return screenWidthWorld;
    }

    private float CalculateScreenHeightWorld()
    {
        // Calculate screen height in world coordinates
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Vector2 zeroPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);
        float screenHeightWorld = Mathf.Abs(screenBounds.y - zeroPoint.y);
        return screenHeightWorld;
    }
}