using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Управление главным меню: Start / Continue / Report / Quit.
/// Подвешивается на пустой GameObject (MenuManager) в сцене MainMenu.
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header("UI")]
    public Button startButton;
    public Button continueButton;
    public Button reportButton;
    public Button quitButton;
    public TMP_Text infoText; // опционально, подсказка

    [Header("Scene names")]
    public string lessonSceneName = "Lesson";
    public string reportSceneName = "Report";

    void Start()
    {
        // Подпишем обработчики
        if (startButton) startButton.onClick.AddListener(StartNewCourse);
        if (continueButton) continueButton.onClick.AddListener(Continue);
        if (reportButton) reportButton.onClick.AddListener(OpenReport);
        if (quitButton) quitButton.onClick.AddListener(QuitApp);

        // Включаем/выключаем Continue в зависимости от наличия сохранения
        bool has = ProgressManager.HasSavedProgress();
        if (continueButton) continueButton.interactable = has;

        if (infoText != null)
        {
            infoText.text = has ? "Continue available" : "No saved progress";
        }
    }

    public void StartNewCourse()
    {
        // Если нужно — очистить старый прогресс
        ProgressManager.ClearProgress();

        // Для MVP просто загружаем сцену Lesson
        SceneManager.LoadScene(lessonSceneName);
    }

    public void Continue()
    {
        if (!ProgressManager.HasSavedProgress())
        {
            Debug.LogWarning("No saved progress to continue.");
            return;
        }

        // Загружаем сцену урока — в уроке LessonManager может подхватить прогресс и восстановить состояние
        SceneManager.LoadScene(lessonSceneName);
    }

    public void OpenReport()
    {
        SceneManager.LoadScene(reportSceneName);
    }

    public void QuitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
