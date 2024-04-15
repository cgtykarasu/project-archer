using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    TextMeshProUGUI highScoreText;

    void Awake()
    {
        scoreText = FindObjectOfType<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        EventManager.OnScoreChanged += UpdateScoreDisplay;
        
        UpdateScoreDisplay(ScoreManager.Instance.Score);
    }

    void OnDisable()
    {
        EventManager.OnScoreChanged -= UpdateScoreDisplay;
    }

    void UpdateScoreDisplay(int newScore)
    {
        scoreText.text = "Score : " + newScore.ToString();
    }
}