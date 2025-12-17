using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField questionInput;
    [SerializeField] private Button askButton;
    [SerializeField] private QAController qaController;

    private void Start()
    {
        askButton.onClick.AddListener(OnAskClicked);
    }

    private async void OnAskClicked()
    {
        if (string.IsNullOrWhiteSpace(questionInput.text))
        {
            Debug.Log("Question is empty");
            return;
        }

        await qaController.AskQuestion(questionInput.text);
        questionInput.text = "";
    }
}
