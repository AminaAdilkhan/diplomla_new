using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class AITutorController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource voiceSource;
    public AudioClip greetingClip;

    [Header("Lecture Video")]
    public VideoPlayer lectureVideo;

    private bool lectureStarted = false;

    private void Start()
    {
        if (lectureVideo != null)
            lectureVideo.Stop();
    }

    // 🔹 ЭТО ВЫЗЫВАЕТ КНОПКА
    public void StartLecture()
    {
        if (lectureStarted) return;

        lectureStarted = true;
        StartCoroutine(LectureSequence());
    }

    private IEnumerator LectureSequence()
    {
        // 1. Приветствие
        if (voiceSource != null && greetingClip != null)
        {
            voiceSource.clip = greetingClip;
            voiceSource.Play();
            yield return new WaitForSeconds(greetingClip.length);
        }

        // 2. Пауза
        yield return new WaitForSeconds(0.5f);

        // 3. Видео
        if (lectureVideo != null)
        {
            lectureVideo.Play();
        }
    }
}
