using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LessonManager : MonoBehaviour
{
    [Header("UI")]
    public Button finishButton;

    [Header("Scene names")]
    public string reportSceneName = "Report";

    void Start()
    {
        if (finishButton != null)
            finishButton.onClick.AddListener(FinishLesson);
    }

    public void FinishLesson()
    {
        // MVP-прогресс (заглушка)
        ProgressManager.SaveProgress(
            courseId: 1,
            moduleId: 1,
            lessonId: 1
        );

        SceneManager.LoadScene(reportSceneName);
    }
}
