using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScore : MonoBehaviour
{
    public int pointValue = 10; // Her bir hedefin puan değeri

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            ScoreManagerTest.Instance.AddScore(pointValue); // Score değişkenine puan değerini ekleyin
            Destroy(gameObject); // Nesneyi yok edin
        }
    }
}
