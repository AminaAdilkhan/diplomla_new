using UnityEngine;

public class TutorController : MonoBehaviour
{
    [SerializeField] private SubtitleUI subtitles;
    [SerializeField] private TTSManager ttsManager;

    public void PlayIntro(string text)
    {
        ShowAndSpeak(text);
    }

    public void Speak(string text)
    {
        ShowAndSpeak(text);
    }

    private void ShowAndSpeak(string text)
    {
        if (subtitles != null)
            subtitles.Show(text);
        else
            Debug.LogError("SubtitleUI is not assigned!");

        if (ttsManager != null)
            ttsManager.Speak(text);
        else
            Debug.LogError("TTSManager is not assigned!");
    }
}
