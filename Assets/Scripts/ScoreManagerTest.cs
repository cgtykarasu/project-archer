using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManagerTest : MonoBehaviour
{
    public static ScoreManagerTest Instance { get; private set; }

    public event Action<int> OnScoreChanged;

    int score;

    public int Score
    {
        get { return score; }
        private set
        {
            score = value;
            OnScoreChanged?.Invoke(score);
        }
    }

    void Awake()
    {
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

    public void AddScore(int amount)
    {
        Score += amount;
    }
}