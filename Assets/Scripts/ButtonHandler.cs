using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    public void OnResetHighscoreButtonClicked()
    {
        // PlayerPrefs.DeleteAll();
        // PlayerPrefs.SetInt("highscore", 0);
        ScoreManagerTest.Instance.HighScore = 0;
        UpdateHighScoreText();
    }
    
    void UpdateHighScoreText()
    {
        highScoreText.text = $"High Score : {ScoreManagerTest.Instance.HighScore}";
    }
}
