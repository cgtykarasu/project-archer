using UnityEngine;

public class BowScript : MonoBehaviour
{
    Vector2 direction;
    bool canShoot = true;

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
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 bowPos = new Vector2(transform.position.x, transform.position.y);
        direction = bowPos - mousePos;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float clampAngle = Mathf.Clamp(angle, -25, 80);
        transform.rotation = Quaternion.AngleAxis(clampAngle, Vector3.forward);
    }
}
