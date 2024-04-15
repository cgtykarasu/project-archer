using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testGameOver : MonoBehaviour
{
    public GameObject gameOverScreen; // Bitiş ekranı GameObject'i
    public TextMeshProUGUI scoreText; // Score'ı gösterecek TextMeshProUGUI bileşeni
    public TextMeshProUGUI highScoreText; // High Score'ı gösterecek TextMeshProUGUI bileşeni
    BarController barController;
    
    // Verilen süre sonunda oyunu sonlandırma işlemi
    public float gameDuration = 10f; // Oyun süresi (saniye cinsinden)

    void Start()
    {
        // Oyun süresi sonunda GameOver metodunu çağır.
        Invoke("GameOver", gameDuration);
        barController = FindObjectOfType<BarController>();
    }

    void GameOver()
    {
        EventManager.TriggerGameOver();
        // Oyunun durmasını sağla (opsiyonel)
        Time.timeScale = 0f;

        // Bitiş ekranını göster
        gameOverScreen.SetActive(true);
        barController.SetGameOver(true);
        
        ScoreManager.Instance.HideScoreUI(); // Skor UI'sini gizle

        // Skoru göster
        if (ScoreManager.Instance != null)
        {
            scoreText.text = ScoreManager.Instance.GetScoreText();
            highScoreText.text = ScoreManager.Instance.GetHighScoreText();
        }
        else
        {
            Debug.LogWarning("ScoreManager.Instance is null!");
        }
    }
    
    public void TryAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        Time.timeScale = 1f;
    }

}
