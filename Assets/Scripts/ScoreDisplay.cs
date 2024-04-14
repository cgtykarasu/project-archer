using System;
using System.Collections;
using System.Collections.Generic;
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
        ScoreManager.Instance.OnScoreChanged += UpdateScoreDisplay;
        
        UpdateScoreDisplay(ScoreManager.Instance.Score);
    }

    void OnDisable()
    {
        ScoreManager.Instance.OnScoreChanged -= UpdateScoreDisplay;
    }

    void UpdateScoreDisplay(int newScore)
    {
        scoreText.text = "Score : " + newScore.ToString();
    }
}


// public static ScoreManager instance;
// public TextMeshProUGUI scoreText;
// public TextMeshProUGUI highScoreText;
//
// private int _score;
// private int _highScore;
//
// private void Awake()
// {
//     instance = this;
//     _highScore = PlayerPrefs.GetInt("highScore", 0);
// }
//
// private void Start()
// {
//     UpdateScoreText();
//     UpdateHighScoreText();
// }
//
// public int GetScore => _score;
//
// public void AddScore(int score)
// {
//     _score += score;
//     UpdateScoreText();
//
//     if (_score > _highScore)
//     {
//         _highScore = _score;
//         UpdateHighScoreText();
//         PlayerPrefs.SetInt("highScore", _score);
//     }
// }
//
// private void UpdateScoreText()
// {
//     scoreText.text = $"{_score} POINTS";
// }
//
// private void UpdateHighScoreText()
// {
//     highScoreText.text = $"HIGH SCORE : {_highScore}";
// }