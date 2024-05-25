using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField]
    private float floatAmplitude = 0.5f; // Amplitude of the sine wave
    [SerializeField]
    private float floatFrequency = 1f;   // Frequency of the sine wave

    private float initialY;
    private Camera mainCamera;

    void Start()
    {
        initialY = transform.position.y;
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Apply sine wave to the vertical position
        Vector3 position = transform.position;
        position.y = initialY + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = position;

        // Destroy the obstacle if it moves off-screen
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        if (screenPoint.x < -0.1f || screenPoint.x > 1.1f || screenPoint.y < -0.1f || screenPoint.y > 1.1f)
        {
            Debug.Log("Obstacle destroyed at: " + transform.position);
            Destroy(gameObject);
        }
    }
}
