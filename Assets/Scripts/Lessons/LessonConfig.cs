using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu(menuName = "VR Meta University/Lesson Config")]
public class LessonConfig : ScriptableObject
{
    public string lessonId;
    public string courseId;
    public string title;
    public string introText;
    public string[] assetAddresses;

    public async Task LoadAssetsAsync()
    {
        // Здесь подключаются Addressables
        await Task.Delay(300); // имитация загрузки
    }
}
