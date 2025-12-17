using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LessonManager : MonoBehaviour
{
    [Header("Lesson")]
    [SerializeField] private LessonConfig currentLesson;
    [SerializeField] private TutorController tutor;

    [Header("UI")]
    [SerializeField] private Button finishButton;
    [SerializeField] private string reportSceneName = "Report";

    private float lessonTimer;

    private void Start()
    {
        if (finishButton != null)
            finishButton.onClick.AddListener(FinishLesson);
    }

    private void Update()
    {
        lessonTimer += Time.deltaTime;
        ProgressManager.TrackTime(Time.deltaTime);
    }

    public async Task StartLesson()
    {
        await currentLesson.LoadAssetsAsync();
        tutor.PlayIntro(currentLesson.introText);
    }

    public void FinishLesson()
    {
        ProgressManager.Save();
        SceneManager.LoadScene(reportSceneName);
    }
}
