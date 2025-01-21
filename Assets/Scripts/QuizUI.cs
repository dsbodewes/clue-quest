using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    public Text questionText;
    public Button[] answerButtons;

    public Image background1;
    public Image background2;

    public Sprite[] backgrounds1;
    public Sprite[] backgrounds2;


    private void Start()
    {
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        if (GameManager.Instance.questions == null || GameManager.Instance.questions.Count == 0)
        {
            return;
        }

        // Get the current question from the GameManager using the singleton instance
        var currentQuestion = GameManager.Instance.GetCurrentQuestion();

        if (currentQuestion == null)
        {
            return;
        }

        questionText.text = currentQuestion.questionText; // Update the UI

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < currentQuestion.answers.Length)
            {
                answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answers[i];
                answerButtons[i].gameObject.SetActive(true); // Ensure the button is visible
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false); // Hide unused buttons
            }

            int answerIndex = i; // Necessary to avoid closure issues
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(answerIndex));
        }

        UpdateBackgrounds(GameManager.Instance.GetCurrentQuestionIndex());

        void UpdateBackgrounds(int questionIndex)
        {
            if (backgrounds1 != null && backgrounds1.Length > questionIndex)
            {
                background1.sprite = backgrounds1[questionIndex];
            }

            if (backgrounds2 != null && backgrounds2.Length > questionIndex)
            {
                background2.sprite = backgrounds2[questionIndex];
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
}