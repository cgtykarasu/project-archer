using UnityEngine;

public class BowScript : MonoBehaviour
{
    Vector2 direction;
    bool canShoot = true;
    
    public float minYPosition = -3f; // En düşük y eksen değeri
    public float maxYPosition = 3f; // En yüksek y eksen değeri

    void OnEnable()
    {
        EventManager.GameOver += DisableShooting;
    }

    void OnDisable()
    {
        EventManager.GameOver -= DisableShooting;
    }

    void DisableShooting()
    {
        canShoot = false;
    }

    void Update()
    {
        if (canShoot) {
            RotateBow();
        }

    }

    void RotateBow()
    {
//         Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//         Vector2 bowPos = new Vector2(transform.position.x, transform.position.y);
//         direction = bowPos - mousePos;
//         
//         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//         float clampAngle = Mathf.Clamp(angle, -50, 65);
//         // Quaternion.AngleAxis kullanarak x ekseni etrafında dönüş
//         Quaternion newRotation = Quaternion.AngleAxis(clampAngle, Vector3.left);
//
//         float currentYRotation = transform.rotation.eulerAngles.y;
//
// // Y ekseni dışındaki rotasyonları kullanarak yeni rotasyonu oluştur
//         Quaternion finalRotation = Quaternion.Euler(newRotation.eulerAngles.x, currentYRotation, newRotation.eulerAngles.z);
//
// // Objeyi yalnızca x ekseni etrafında döndür
//         transform.rotation = finalRotation;

        // Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Vector2 bowPos = new Vector2(transform.position.x, transform.position.y);
        // direction = bowPos - mousePos;
        //
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // float clampAngle = Mathf.Clamp(angle, -25, 80);
        // transform.rotation = Quaternion.AngleAxis(clampAngle, Vector3.forward);
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    
        // Mouse pozisyonunu kullanarak yeni y ekseni pozisyonunu hesapla
        float newYPosition = Mathf.Clamp(mousePos.y, minYPosition, maxYPosition);
    
        // Yeni y ekseni pozisyonunu objeye uygula
        transform.position = new Vector3(transform.position.x, -newYPosition, transform.position.z);
    }
}
