using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private int lives = 3;

    public GameObject quizCanvas;

    public GameObject finalScoreCanvas;
    public UnityEngine.UI.Text finalScoreText;
    public UnityEngine.UI.Text livesText;

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

    private void Start()
    {
        UpdateLivesUI();
    }

    public Question GetCurrentQuestion()
    {
        if (questions == null || questions.Count == 0)
        {
            Debug.LogWarning("No questions available.");
            return null; 
        }

        if (currentQuestion < 0 || currentQuestion >= questions.Count)
        {
            Debug.LogWarning($"Invalid currentQuestion index: {currentQuestion}. Total questions: {questions.Count}");
            return null; 
        }

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
            LoseLife();
        }

        NextQuestion();
    }

    private void LoseLife()
    {
        lives--;
        Debug.Log("Wrong Answer! Lives remaining: " + lives);

        UpdateLivesUI();

        if (lives <= 0)
        {
            //Debug.Log("Lives <= 0: Ending game");
            EndGame();
            return;
        }
    }

    private void NextQuestion()
    {
        if (lives <= 0) return;

        currentQuestion++;
        Debug.Log("Next question: " + currentQuestion);

        if (currentQuestion < questions.Count)
        {
            Debug.Log("Next Question: " + questions[currentQuestion].questionText);
        }
        else
        {
            EndGame();
        }
    }

    public void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over! Score: " + score);

        if (finalScoreCanvas != null)
        {
            quizCanvas.SetActive(false);
        }

        if (finalScoreCanvas != null)
        {
            finalScoreCanvas.SetActive(true);
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + score + "/" + questions.Count;
        }
    }

    public void ReturnToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void ResetGame()
    {
        currentQuestion = 0;
        score = 0;
        lives = 3;

        //Debug.Log("Game Reset: Lives = " + lives + ", Score = " + score + ", CurrentQuestion = " + currentQuestion);
        UpdateLivesUI();
    }
}