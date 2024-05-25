using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float thrust = 5f;
    public float gravity = 2.5f;
    public float nearMissDistance = 5f; // Define the near miss distance threshold
    public AudioSource thrustSound;
    public AudioSource collisionSound;
    public AudioSource gameOverSound;
    private ScoringManager scoringManager;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoringManager = FindObjectOfType<ScoringManager>();
        thrustSound = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, thrust);
             if (!thrustSound.isPlaying)
            {
            thrustSound.Play();
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - gravity * Time.deltaTime);
            if (thrustSound.isPlaying)
            {
            thrustSound.Stop();
            }
        }
        // CheckNearMiss();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collision detected with obstacle. Game Over!");
            collisionSound.Play();
            gameOverSound.Play();
            FindObjectOfType<ScoringManager>().StopScoring();
            FindObjectOfType<GameOverManager>().GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle avoided");
            // scoringManager.AddObstacleAvoidanceScore();
        }
    }

    // void CheckNearMiss()
    // {
    // GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
    // foreach (GameObject obstacle in obstacles)
    // {
    //     float distance = Vector2.Distance(transform.position, obstacle.transform.position);
    //     if (distance < nearMissDistance)
    //     {
    //         Debug.Log("Near miss with obstacle"); // Add debug log
    //         scoringManager.AddObstacleAvoidanceScore();
    //     }
    // }
}

