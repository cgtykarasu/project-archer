using System;

public abstract class EventManager
{
    public static event Action ArrowShot;
    public static event Action GameOver;
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnHighScoreChanged;

    public static void TriggerScoreChanged(int score)
    {
        OnScoreChanged?.Invoke(score);
    }

    public static void TriggerHighScoreChanged(int highScore)
    {
        OnHighScoreChanged?.Invoke(highScore);
    }

    public static void TriggerArrowShot()
    {
        ArrowShot?.Invoke();
    }

    public static void TriggerGameOver()
    {
        GameOver?.Invoke();
    }
}