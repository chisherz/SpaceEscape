using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Array to hold the obstacle prefabs
    public float spawnInterval = 2f;
    public float minY = -4f;
    public float maxY = 4f;
    public float minObstacleSpeed = 1.5f;
    public float maxObstacleSpeed = 3f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            // Determine whether to spawn from the left or right
            bool spawnFromLeft = Random.Range(0, 2) == 0;

            // Randomize vertical position
            float randomY = Random.Range(minY, maxY);

            // Set spawn position
            Vector3 spawnPosition;
            if (spawnFromLeft)
            {
                spawnPosition = mainCamera.ScreenToWorldPoint(new Vector3(0, Random.Range(0, Screen.height), mainCamera.nearClipPlane));
                spawnPosition.x -= 1; // Spawn just off-screen to the left
            }
            else
            {
                spawnPosition = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Random.Range(0, Screen.height), mainCamera.nearClipPlane));
                spawnPosition.x += 1; // Spawn just off-screen to the right
            }
            spawnPosition.y = randomY; // Set the random vertical position

            // Randomly select an obstacle prefab
            int randomIndex = Random.Range(0, obstaclePrefabs.Length);
            GameObject selectedPrefab = obstaclePrefabs[randomIndex];

            // Create a random rotation
            Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

            // Instantiate the selected obstacle prefab with the random rotation
            GameObject obstacle = Instantiate(selectedPrefab, spawnPosition, randomRotation);
            Debug.Log("Obstacle spawned at: " + spawnPosition);

            // Add horizontal and vertical velocity to the obstacle with some randomness
            Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>();
            float obstacleSpeed = Random.Range(minObstacleSpeed, maxObstacleSpeed);
            float randomAngle = Random.Range(-30f, 30f); // Random angle for trajectory
            Vector2 direction = new Vector2(spawnFromLeft ? 1 : -1, Mathf.Tan(randomAngle * Mathf.Deg2Rad)).normalized;
            rb.velocity = direction * obstacleSpeed;

            // Add "floaty" movement with vertical sine wave
            obstacle.AddComponent<ObstacleMovement>();

            // Wait for the next spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
