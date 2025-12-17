using UnityEngine;

public class TTSMessenger : MonoBehaviour
{
    [SerializeField] private TTSManager ttsManager;

    // Этот метод можно вызывать из кнопки
    public void Send(string text)
    {
        if (ttsManager != null)
        {
            ttsManager.Speak(text);
        }
        else
        {
            Debug.LogError("TTSManager is not assigned in TTSMessenger!");
        }
    }
}
