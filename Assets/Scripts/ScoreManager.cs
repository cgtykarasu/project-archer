using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI scoreText; // Score'ı gösterecek UI bileşeni
    [SerializeField] TextMeshProUGUI highScoreText; // High Score'ı gösterecek UI bileşeni

    int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            EventManager.TriggerScoreChanged(score);
            UpdateScoreText();
        }
    }
    
    int highScore;
    public int HighScore
    {
        get => highScore;
        set
        {
            highScore = value;
            EventManager.TriggerHighScoreChanged(highScore);
            UpdateHighScoreText();
            PlayerPrefs.SetInt("highScore", highScore);
        }
    }

    void Awake()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
        UpdateHighScoreText();
    }
    
    void UpdateScoreText()
    {
        if (Score < 1000)
        {
            scoreText.text = $"Score: {Score}MB";
        }
        else
        {
            float displayScore = Score / 1000f;
            scoreText.text = $"Score: {displayScore}GB";
        }
    }

    void UpdateHighScoreText()
    {
        if (HighScore < 1000)
        {
            highScoreText.text = $"High Score: {highScore}MB";
        }
        else
        {
            float displayHighScore = HighScore / 1000f;
            highScoreText.text = $"High Score: {displayHighScore}GB";
        }
    }

    public void AddScore(int addedScore)
    {
        Score += addedScore;

        if (score > highScore)
        {
            HighScore = score;
        }
    }
    
    public string GetScoreText()
    {
        return scoreText.text;
    }

    public string GetHighScoreText()
    {
        return highScoreText.text;
    }
    
    public void HideScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(false);
        }
        if (highScoreText != null)
        {
            highScoreText.gameObject.SetActive(false);
        }
    }
}
