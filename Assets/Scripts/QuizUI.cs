using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    public Text questionText;  // This is the text component that will display the question
    public Button[] answerButtons; // This is an array of buttons that will display the answers

    private void Start()
    {
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        // Get the current question from the GameManager using the singleton instance
        var currentQuestion = GameManager.Instance.GetCurrentQuestion(); 

        // Update the UI
        questionText.text = currentQuestion.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answers[i];
            int answerIndex = i; // Necessary to avoid closure issues
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(answerIndex));

        }
    }

    void OnAnswerSelected(int selectedAnswer)
    {
        // Pass the player's choice to the GameManager
        GameManager.Instance.SubmitAnswer(selectedAnswer);

        // Update the UI for the next question
        if (GameManager.Instance.GetCurrentQuestion() != null)
        {
            DisplayQuestion();
        }
    }
}
