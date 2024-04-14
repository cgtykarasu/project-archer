using System;
using TMPro;
using UnityEngine;

// public class ScoreManager : MonoBehaviour
// {
//     public static ScoreManager Instance { get; private set; }
//
//     public event Action<int> OnScoreChanged;
//
//     public TextMeshProUGUI scoreText;
//     public TextMeshProUGUI highScoreText;
//
//     int score;
//     int highScore;
//
//     public int HighScore
//     {
//         get => highScore;
//         set => highScore = value;
//     }
//
//     void Awake()
//     {
//         // scoreText = FindObjectOfType<TextMeshProUGUI>();
//         highScore = PlayerPrefs.GetInt("highScore", 0);
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }
//
//     public int Score
//     {
//         get { return score; }
//         private set
//         {
//             score = value;
//             OnScoreChanged?.Invoke(score);
//         }
//     }
//
//     void Start()
//     {
//         UpdateScoreText();
//         UpdateHighScoreText();
//     }
//
//     public int GetScore => score;
//
//     void UpdateScoreText()
//     {
//         scoreText.text = $"Score : {score}";
//     }
//
//     void UpdateHighScoreText()
//     {
//         highScoreText.text = $"High Score : {highScore}";
//     }
//
//     public void AddScore(int addedScore)
//     {
//         score += addedScore;
//         UpdateScoreText();
//
//         if (score > highScore)
//         {
//             highScore = score;
//             UpdateHighScoreText();
//             PlayerPrefs.SetInt("highScore", score);
//         }
//     }
//
//     void OnEnable()
//     {
//         Instance.OnScoreChanged += UpdateScoreDisplay;
//         UpdateScoreDisplay(Instance.Score);
//     }
//
//     void OnDisable()
//     {
//         Instance.OnScoreChanged -= UpdateScoreDisplay;
//     }
//
//     void UpdateScoreDisplay(int newScore)
//     {
//         scoreText.text = "Score : " + newScore;
//     }
// }

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public event Action<int> OnScoreChanged; // Puan değişikliğini bildirmek için olay.

    public TextMeshProUGUI scoreText;      // Puanı gösteren UI elementi.
    public TextMeshProUGUI highScoreText;  // Yüksek puanı gösteren UI elementi.

    int score;       // Aktif oyun puanı.
    int highScore;   // Yüksek puan.

    void Awake()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0); // PlayerPrefs'ten yüksek puanı yükler.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Nesneyi sahne değişikliklerinde yok etmemek için.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();       // Başlangıçta puanı güncelle.
        UpdateHighScoreText();   // Başlangıçta yüksek puanı güncelle.
    }

    public int Score
    {
        get { return score; }
        private set
        {
            score = value;
            OnScoreChanged?.Invoke(score); // Puan değişikliği olduğunda olayı tetikle.
            UpdateScoreText();             // UI'da puanı güncelle.
        }
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
    
    public int HighScore
    {
        get => highScore;
        set
        {
            highScore = value;
            UpdateHighScoreText();
            PlayerPrefs.SetInt("highScore", highScore);
        }
    }

    public void AddScore(int addedScore)
    {
        Score += addedScore; // Score property'sini kullanarak puanı arttır ve otomatik güncellemeleri tetikle.

        if (score > highScore)
        {
            HighScore = score; // HighScore property'sini kullanarak yüksek puanı güncelle.
        }
    }


    // Bu metotlara daha önce gerek duyulmadığından dolayı kaldırıldı.
    // OnEnable ve OnDisable metotlarını yalnızca skor değişikliklerini izlemek için kullanılabilir hale getirildi.
}
