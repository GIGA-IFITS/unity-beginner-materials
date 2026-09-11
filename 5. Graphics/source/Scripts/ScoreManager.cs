using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;

    private int currentScore = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreText();
        if (scoreText.TryGetComponent<ScalePunch>(out var punch)) punch.Play();
    }

    public int GetScore()
    {
        return currentScore;
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore;
    }
}
