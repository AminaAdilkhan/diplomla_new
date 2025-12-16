using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ReportManager : MonoBehaviour
{
    public TMP_Text reportText;

    void Start()
    {
        if (!ProgressManager.HasSavedProgress())
        {
            reportText.text = "No data available";
            return;
        }

        var (course, module, lesson) = ProgressManager.LoadProgress();

        reportText.text =
            $"            REPORT  \n" +
            $"Course: {course}\n" +
            $"Module: {module}\n" +
            $"Lesson: {lesson}\n" +
            $"Status: Completed";
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
