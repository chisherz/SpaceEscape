using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverCanvas; // Assign in Inspector
    private ScoringManager scoringManager;

    void Start()
    {
        gameOverCanvas.SetActive(false); // Ensure the GameOverCanvas is disabled initially
        scoringManager = FindObjectOfType<ScoringManager>();
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true); // Show Game Over UI
        Time.timeScale = 0; // Pause the game
        scoringManager.StopScoring();
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Unpause the game
        FindObjectOfType<ScoringManager>().ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1; // Unpause the game
        SceneManager.LoadScene("MainMenu"); // Replace with your main menu scene name
    }
}