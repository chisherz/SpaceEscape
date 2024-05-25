using UnityEngine;
using TMPro;

public class ScoringManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private bool isGameOver = false;

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        if (!isGameOver)
        {
            // Increment score based on survival time
            score += Mathf.FloorToInt(Time.timeSinceLevelLoad) - score;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void StopScoring()
    {
        isGameOver = true;
    }

    public void ResetScore()
    {
    score = 0;
    UpdateScoreText();
    }

    // public void AddObstacleAvoidanceScore()
    // {
    //     if (!isGameOver)
    //     {
    //         score += 50; // Increment score for avoiding an obstacle (adjust as needed)
    //         UpdateScoreText();
    //         Debug.Log("Score updated via Obstacle Avoidance: " + score);
    //     }
    // }
}
