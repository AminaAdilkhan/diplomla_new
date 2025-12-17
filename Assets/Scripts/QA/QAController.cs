using System.Threading.Tasks;
using UnityEngine;

public class QAController : MonoBehaviour
{
    [SerializeField] private TutorController tutor;

    public async Task AskQuestion(string question)
    {
        if (tutor == null)
        {
            Debug.LogError("TutorController is not assigned in QAController");
            return;
        }

        // имитация "AI думает"
        await Task.Delay(1000);

        string fakeAnswer =
            "That is a great question. Electric charge is a fundamental property of matter that causes it to experience a force when placed in an electromagnetic field.";

        tutor.Speak(fakeAnswer);
    }
}
