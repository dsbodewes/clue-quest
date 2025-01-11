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
        DontDestroyOnLoad(gameObject);
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
        else
        {
            Debug.LogWarning("Lives UI Text is not assigned.");
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

        // Remove if causing issues
        //ResetGame();
    }

    public void ResetGame()
    {
        questions.Clear();

        currentQuestion = 0;
        score = 0;
        lives = 3;

        Debug.Log($"Game Reset: Current Question = {currentQuestion}");

        questions.Add(new Question
        {
            questionText = "Yuh or Nuh?",
            answers = new string[] { "Yuh", "Nuh"},
            correctAnswer = 0
        });

        questions.Add(new Question
        {
            questionText = "Blue or Red?",
            answers = new string[] { "Blue", "Green" },
            correctAnswer = 0
        });

        questions.Add(new Question
        {
            questionText = "9 + 10?",
            answers = new string[] { "19", "21" },
            correctAnswer = 1
        });

        questions.Add(new Question
        {
            questionText = "Fortnite?",
            answers = new string[] { "Yes", "No" },
            correctAnswer = 0
        });

        questions.Add(new Question
        {
            questionText = "A or B?",
            answers = new string[] { "A", "B" },
            correctAnswer = 1
        });


        Debug.Log("Game Reset: Lives = " + lives + ", Score = " + score + ", CurrentQuestion = " + currentQuestion);
        Debug.Log("Questions reloaded. Total questions: " + questions.Count);
        ReassignReferences();
        UpdateLivesUI();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        ReassignReferences();
    }

    public void ReassignReferences()
    {
        if (livesText == null)
        {
            livesText = GameObject.Find("LivesText")?.GetComponent<UnityEngine.UI.Text>();
        }

        if (quizCanvas == null)
        {
            quizCanvas = GameObject.Find("QuizCanvas");
        }

        if (finalScoreCanvas == null)
        {
            finalScoreCanvas = GameObject.Find("FinalCanvas");
        }

        if (finalScoreText == null && finalScoreCanvas != null)
        {
            finalScoreText = finalScoreCanvas.GetComponentInChildren<UnityEngine.UI.Text>();
            Debug.Log("FinalScoreText reassigned.");
        }

        Debug.Log("Reassigned GameManager references.");
        UpdateLivesUI();
    }

    public int GetCurrentQuestionIndex()
    {
        return currentQuestion;
    }
}