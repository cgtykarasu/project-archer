using System.Runtime.InteropServices;
using UnityEngine;

public class WebInterface : MonoBehaviour
{
    [DllImport("__Internal")]
    static extern void UpdateScoreOnWeb(int score);

    void OnEnable() {
        EventManager.OnScoreChanged += HandleScoreChanged;
    }

    void OnDisable() {
        EventManager.OnScoreChanged -= HandleScoreChanged;
    }

    void HandleScoreChanged(int newScore) {
        UpdateScoreOnWeb(newScore);
    }
}