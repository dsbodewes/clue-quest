using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    public Text questionText;  
    public Button[] answerButtons; // This is an array of buttons that will display the answers

    private void Start()
    {
        DisplayQuestion();


    }

    void DisplayQuestion()
    {
        // Get the current question from the GameManager using the singleton instance
        var currentQuestion = GameManager.Instance.GetCurrentQuestion();

        if (currentQuestion == null)
        {
            Debug.LogWarning("No current question to display.");
            return;
        }

        Debug.Log("Displaying Question: " + currentQuestion.questionText);

        questionText.text = currentQuestion.questionText; // Update the UI

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < currentQuestion.answers.Length)
            {
                answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answers[i];
            }
            else
            {
                Debug.LogWarning("Not enough answers for button " + i);
            }

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
