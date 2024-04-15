using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testDailyPlayManager : MonoBehaviour
{
    // // Oyuncunun bir sonraki oyun oynamasına kadar beklemesi gereken süre (saniye cinsinden)
    // public float dailyPlayCooldown = 86400; // Bir günün saniye cinsinden değeri (24 saat * 60 dakika * 60 saniye)
    //
    // // Oyuncunun son oyun oynama tarihi
    // private DateTime lastPlayDate;
    //
    // // Oyun başladığında çağrılan metod
    // void Start()
    // {
    //     Debug.Log("START!");
    //     
    //     // Son oyun oynama tarihini yükle
    //     lastPlayDate = LoadLastPlayDate();
    //
    //     // Eğer oyuncu oyunu daha önce hiç oynamadıysa, ilk oyun tarihini kaydet
    //     if (lastPlayDate == DateTime.MinValue)
    //     {
    //         SaveLastPlayDate(DateTime.Now);
    //     }
    // }
    //
    // // Oyuncunun oyun oynama butonuna basıldığında çağrılan metod
    // public void PlayGame()
    // {
    //     Debug.Log("Oyun oynama butonuna basıldı!");
    //     
    //     // Son oyun oynama tarihini yükle
    //     lastPlayDate = LoadLastPlayDate();
    //
    //     // Geçen süreyi hesapla
    //     TimeSpan timeSinceLastPlay = DateTime.Now - lastPlayDate;
    //
    //     // Eğer geçen süre, günlük cooldown'dan büyükse, oyuna izin ver
    //     if (timeSinceLastPlay.TotalSeconds >= dailyPlayCooldown)
    //     {
    //         // Oyunu başlat
    //         StartGame();
    //
    //         // Son oyun oynama tarihini güncelle
    //         SaveLastPlayDate(DateTime.Now);
    //     }
    //     else
    //     {
    //         // Oyuncuya hata mesajı göster (Günlük oyun hakkınızı kullanmak için daha beklemeniz gerekiyor)
    //         Debug.Log("Günlük oyun hakkınızı kullanmak için daha beklemeniz gerekiyor.");
    //     }
    // }
    //
    // // Oyunu başlat
    // void StartGame()
    // {
    //     Debug.Log("Oyun başladı!");
    //     // Oyunun başlatılmasıyla ilgili gerekli işlemleri yapabilirsiniz.
    // }
    //
    // // Son oyun oynama tarihini kaydet
    // void SaveLastPlayDate(DateTime date)
    // {
    //     PlayerPrefs.SetString("LastPlayDate", date.ToString());
    // }
    //
    // // Kaydedilmiş son oyun oynama tarihini yükle
    // DateTime LoadLastPlayDate()
    // {
    //     if (PlayerPrefs.HasKey("LastPlayDate"))
    //     {
    //         string dateString = PlayerPrefs.GetString("LastPlayDate");
    //         return DateTime.Parse(dateString);
    //     }
    //     else
    //     {
    //         return DateTime.MinValue;
    //     }
    // }
    
    private const string LastPlayDateKey = "LastPlayDate";
    Scene currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Günlük oynama durumunu kontrol et
    public void CheckDailyPlay()
    {
        // Son oynama tarihini kontrol et
        DateTime lastPlayDate = GetLastPlayDate();

        // Bugün oynanmış mı kontrol et
        if (lastPlayDate.Date != DateTime.Today)
        {
            // Bugün oynanmamışsa, oyuna izin ver
            Debug.Log("Bugün oyun oynanmamış. Oyuna izin verildi.");
            PlayGame();
            // Burada oyuna izin verme işlemini gerçekleştir
            // Örneğin, başlayan bir oyunu başlatma işlemi yapabilirsiniz.
        }
        else
        {
            // Bugün zaten oynanmışsa, bir sonraki oynama tarihini hesapla
            DateTime nextPlayDate = lastPlayDate.AddDays(1);
            TimeSpan remainingTime = nextPlayDate - DateTime.Now;

            Debug.Log("Bugün oyun zaten oynandı. Bir sonraki oynama tarihi: " + nextPlayDate);
            Debug.Log("Kalan süre: " + remainingTime.Hours + " saat, " + remainingTime.Minutes + " dakika, " + remainingTime.Seconds + " saniye");
            // Burada arayüzü güncelleyebilir ve oyuncuya bir sonraki oyun için ne kadar süre beklemesi gerektiğini gösterebilirsiniz.
        }
    }

    // Son oynama tarihini getir
    private DateTime GetLastPlayDate()
    {
        string dateString = PlayerPrefs.GetString(LastPlayDateKey);
        long ticks;
        if (long.TryParse(dateString, out ticks))
        {
            return new DateTime(ticks);
        }
        else
        {
            Debug.LogWarning("PlayerPrefs'ten alınan tarih değeri geçersiz: " + dateString);
            // Varsayılan olarak bugünkü tarihi dön
            return DateTime.Today;
        }
    }

    // Son oynama tarihini kaydet
    private void SaveLastPlayDate(DateTime date)
    {
        PlayerPrefs.SetString(LastPlayDateKey, date.Ticks.ToString());
    }

    // Oyuncunun oyunu oynamasını sağla
    public void PlayGame()
    {
        // Oyun oynandığında çağrılacak metot
        SaveLastPlayDate(DateTime.Now);
        SceneManager.LoadScene(currentScene.buildIndex+1);
        // Burada oyun başlatma veya diğer işlemleri gerçekleştirebilirsiniz.
    }
}
