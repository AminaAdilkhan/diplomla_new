using UnityEngine;

/// <summary>
/// Единый менеджер прогресса (MVP).
/// Используется всеми сценами и контроллерами.
/// </summary>
public static class ProgressManager
{
    private const string KEY_COURSE = "PM_LastCourse";
    private const string KEY_MODULE = "PM_LastModule";
    private const string KEY_LESSON = "PM_LastLesson";

    private const string KEY_TIME = "PM_TimeSpent";
    private const string KEY_QUESTIONS = "PM_Questions";
    private const string KEY_MISTAKES = "PM_Mistakes";

    // ===== TRACKING (для QAController и LessonManager) =====

    public static void TrackTime(float seconds)
    {
        float current = PlayerPrefs.GetFloat(KEY_TIME, 0f);
        PlayerPrefs.SetFloat(KEY_TIME, current + seconds);
    }

    public static void TrackQuestion(bool correct)
    {
        int q = PlayerPrefs.GetInt(KEY_QUESTIONS, 0) + 1;
        PlayerPrefs.SetInt(KEY_QUESTIONS, q);

        if (!correct)
        {
            int m = PlayerPrefs.GetInt(KEY_MISTAKES, 0) + 1;
            PlayerPrefs.SetInt(KEY_MISTAKES, m);
        }
    }

    // ===== SAVE (вызывается при завершении урока) =====

    public static void Save()
    {
        // В MVP ничего дополнительно не делаем
        PlayerPrefs.Save();
    }

    public static void SaveProgress(int courseId, int moduleId, int lessonId)
    {
        PlayerPrefs.SetInt(KEY_COURSE, courseId);
        PlayerPrefs.SetInt(KEY_MODULE, moduleId);
        PlayerPrefs.SetInt(KEY_LESSON, lessonId);
        PlayerPrefs.Save();
    }

    // ===== LOAD (для ReportManager) =====

    public static bool HasSavedProgress()
    {
        return PlayerPrefs.HasKey(KEY_LESSON);
    }

    public static (int courseId, int moduleId, int lessonId) LoadProgress()
    {
        int c = PlayerPrefs.GetInt(KEY_COURSE, 0);
        int m = PlayerPrefs.GetInt(KEY_MODULE, 0);
        int l = PlayerPrefs.GetInt(KEY_LESSON, 0);
        return (c, m, l);
    }

    // ===== OPTIONAL =====

    public static void ClearProgress()
    {
        PlayerPrefs.DeleteKey(KEY_COURSE);
        PlayerPrefs.DeleteKey(KEY_MODULE);
        PlayerPrefs.DeleteKey(KEY_LESSON);
        PlayerPrefs.DeleteKey(KEY_TIME);
        PlayerPrefs.DeleteKey(KEY_QUESTIONS);
        PlayerPrefs.DeleteKey(KEY_MISTAKES);
        PlayerPrefs.Save();
    }
}
