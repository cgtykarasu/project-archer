using UnityEngine;

public class BarController : MonoBehaviour
{
    [SerializeField] SpriteRenderer barSprite;
    
    Vector2 startSize;
    bool gameOver = false; // Oyunun bitip bitmediğini belirten bir bayrak

    
    void Start()
    {
        startSize = barSprite.size;
    }
    
    int currentPower = 0;
    int maxPower = 1000;
    
    void Update()
    {
        if (gameOver)
        {
            return;
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                currentPower += 3;
                UpdateBar(currentPower, maxPower);
            }
        
            if (Input.GetKeyUp(KeyCode.Space))
            {
                currentPower = 0;
                UpdateBar(currentPower, maxPower);
            }
        }
    }
    
    void UpdateBar(int currentValue, int maxValue)
    {
        currentValue = Mathf.Clamp(currentValue, 0, maxValue);
        
        // barSprite.size = new Vector2(startSize.x * (currentValue / (float)maxValue), startSize.y);
        float barSize = startSize.x / maxValue * currentValue;
        barSprite.size = new Vector2(barSize, startSize.y);
        
        if (currentValue <= 0)
        {
            currentValue = 0;
        }
        else if (currentValue >= maxValue)
        {
            // currentValue = maxValue;
            return; // Do not allow the bar to exceed the maximum value
        }
    }
    
    // Oyunun bitip bitmediğini belirlemek için bir metot
    public void SetGameOver(bool isGameOver)
    {
        gameOver = isGameOver;
    }
}
