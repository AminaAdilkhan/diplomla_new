using UnityEngine;

/// <summary>
/// Простой менеджер прогресса — хранит lastCourseId/lastModuleId/lastLessonId в PlayerPrefs.
/// В будущем можно заменить на JSON или отправку на сервер.
/// </summary>
public static class ProgressManager
{
    const string KEY_COURSE = "PM_LastCourse";
    const string KEY_MODULE = "PM_LastModule";
    const string KEY_LESSON = "PM_LastLesson";

    public static void SaveProgress(int courseId, int moduleId, int lessonId)
    {
        PlayerPrefs.SetInt(KEY_COURSE, courseId);
        PlayerPrefs.SetInt(KEY_MODULE, moduleId);
        PlayerPrefs.SetInt(KEY_LESSON, lessonId);
        PlayerPrefs.Save();
    }

    public static bool HasSavedProgress()
    {
        return PlayerPrefs.HasKey(KEY_LESSON); // простая проверка
    }

    public static (int courseId, int moduleId, int lessonId) LoadProgress()
    {
        int c = PlayerPrefs.GetInt(KEY_COURSE, 0);
        int m = PlayerPrefs.GetInt(KEY_MODULE, 0);
        int l = PlayerPrefs.GetInt(KEY_LESSON, 0);
        return (c, m, l);
    }

    public static void ClearProgress()
    {
        PlayerPrefs.DeleteKey(KEY_COURSE);
        PlayerPrefs.DeleteKey(KEY_MODULE);
        PlayerPrefs.DeleteKey(KEY_LESSON);
        PlayerPrefs.Save();
    }
}
