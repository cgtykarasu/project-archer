using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManagerTest : MonoBehaviour
{
    public static ScoreManagerTest Instance { get; private set; }

    public event Action<int> OnScoreChanged;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    int score;
    int highScore;

    public int HighScore
    {
        get => highScore;
        set => highScore = value;
    }

    void Awake()
    {
        // scoreText = FindObjectOfType<TextMeshProUGUI>();

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

    public int Score
    {
        get { return score; }
        private set
        {
            score = value;
            OnScoreChanged?.Invoke(score);
        }
    }

    void Start()
    {
        UpdateScoreText();
        UpdateHighScoreText();
    }

    public int GetScore => score;

    void UpdateScoreText()
    {
        scoreText.text = $"Score : {score}";
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = $"High Score : {highScore}";
    }

    // void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    public void AddScore(int addedScore)
    {
        score += addedScore;
        UpdateScoreText();

        if (score > highScore)
        {
            highScore = score;
            UpdateHighScoreText();
            PlayerPrefs.SetInt("highScore", score);
        }
    }

    void OnEnable()
    {
        ScoreManagerTest.Instance.OnScoreChanged += UpdateScoreDisplay;

        UpdateScoreDisplay(ScoreManagerTest.Instance.Score);
    }

    void OnDisable()
    {
        ScoreManagerTest.Instance.OnScoreChanged -= UpdateScoreDisplay;
    }

    void UpdateScoreDisplay(int newScore)
    {
        scoreText.text = "Score : " + newScore.ToString();
    }
}