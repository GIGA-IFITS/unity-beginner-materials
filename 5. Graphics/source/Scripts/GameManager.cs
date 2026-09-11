using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    [Min(0)] public float gameOverPresentationDelay = 0f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        finalScoreText.text = "Final Score: " + ScoreManager.Instance.GetScore();
        Time.timeScale = 0f; // pause seluruh gameplay
        if (gameOverPresentationDelay > 0f) StartCoroutine(ShowGameOverAfterImpact());
        else gameOverPanel.SetActive(true);
    }

    IEnumerator ShowGameOverAfterImpact()
    {
        // Pause happens immediately; only the opaque panel waits so the explosion is visible.
        yield return new WaitForSecondsRealtime(gameOverPresentationDelay);
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
