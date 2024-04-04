using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    void Awake()
    {
        scoreText = FindObjectOfType<TextMeshProUGUI>();
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
