using TMPro;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    public void OnResetHighscoreButtonClicked()
    {
        // PlayerPrefs.DeleteAll();
        // PlayerPrefs.SetInt("highscore", 0);
        ScoreManager.Instance.HighScore = 0;
        UpdateHighScoreText();
    }
    
    void UpdateHighScoreText()
    {
        highScoreText.text = $"High Score : {ScoreManager.Instance.HighScore}";
    }
}
