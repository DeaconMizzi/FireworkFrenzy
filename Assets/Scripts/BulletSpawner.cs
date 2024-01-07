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
            Vector2 min = Camera.main.ScreenToWorldPoint(Vector2.zero); // Bottom-left corner of the screen in world coordinates
            Vector2 max = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)); // Top-right corner of the screen in world coordinates
            newPosition = new Vector2(Random.Range(min.x, max.x), currentPosition.y);     
        }
        else
        {
            Vector2 min = Camera.main.ScreenToWorldPoint(Vector2.zero); // Bottom-left corner of the screen in world coordinates
            Vector2 max = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)); // Top-right corner of the screen in world coordinates
            newPosition = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
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


}
