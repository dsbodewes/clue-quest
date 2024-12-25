using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // This is a singleton instance of the GameManager

    [System.Serializable] // This attribute allows the class to be serialized
    public class Question
    {
        public string questionText;
        public string[] answers;
        public int correctAnswer;
    }

    public List<Question> questions = new List<Question>();
    private int currentQuestion = 0;
    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call 
    public Question GetCurrentQuestion()
    {
        return questions[currentQuestion];
    }

    public void SubmitAnswer(int selectedAnswer)
    {
        if (selectedAnswer == questions[currentQuestion].correctAnswer)
        {
            score++;
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Wrong Answer");
        }

        NextQuestion();
    }

    private void NextQuestion()
    {
        currentQuestion++;

        if (currentQuestion < questions.Count)
        {
            Debug.Log("Next Question: " + questions[currentQuestion].questionText);
        }
        else
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over! Score: " + score);
    }
}