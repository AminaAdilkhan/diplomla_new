using UnityEngine;
using System.Collections;

public class TTSManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    // Заглушка под реальный TTS (по ТЗ допускается mock)
    public void Speak(string text)
    {
        Debug.Log("[TTS] Speaking: " + text);

        // Здесь в реальном проекте:
        // 1. Отправка текста на TTS API
        // 2. Получение аудио
        // 3. audioSource.PlayOneShot(clip);

        // Пока — имитация
        StartCoroutine(FakeSpeech());
    }

    private IEnumerator FakeSpeech()
    {
        yield return new WaitForSeconds(2f);
    }
}
