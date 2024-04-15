using UnityEngine;

public class HitScore : MonoBehaviour
{
    public int pointValue = 10; // Her bir hedefin puan değeri

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            ScoreManager.Instance.AddScore(pointValue); // Score değişkenine puan değerini ekleyin
            Destroy(gameObject); // Nesneyi yok edin
        }
    }
}
