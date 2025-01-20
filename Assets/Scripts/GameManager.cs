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
        if (finalScoreCanvas != null)
        {
            DontDestroyOnLoad(finalScoreCanvas);
        }

        UpdateLivesUI();
    }

    public Question GetCurrentQuestion()
    {
        if (questions == null || questions.Count == 0)
        {
            return null; 
        }

        if (currentQuestion < 0 || currentQuestion >= questions.Count)
        {
            return null; 
        }

        return questions[currentQuestion];
    }

    public void SubmitAnswer(int selectedAnswer)
    {
        if (selectedAnswer == questions[currentQuestion].correctAnswer)
        {
            score++;
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

        UpdateLivesUI();

        if (lives <= 0)
        {
            EndGame();
            return;
        }
    }

    private void NextQuestion()
    {
        if (lives <= 0) return;

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

    public void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }

    private void EndGame()
    {
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
        questions.Clear();

        currentQuestion = 0;
        score = 0;
        lives = 3;

        questions.Add(new Question
        {
            questionText = "Was it a rainy night or a rainy day?",
            answers = new string[] { "Rainy night", "Rainy day"},
            correctAnswer = 0
        });

        questions.Add(new Question
        {
            questionText = "Does the house have two windows on the side with the left window broken, or is the right window broken?",
            answers = new string[] { "Left window", "Right window" },
            correctAnswer = 0
        });

        questions.Add(new Question
        {
            questionText = "Is there a blue piece of cloth stuck to one of the points of the broken glass, or is it a red piece of cloth?",
            answers = new string[] { "Blue cloth", "Red cloth" },
            correctAnswer = 0
        });

        questions.Add(new Question
        {
            questionText = "Are there footprints on the kitchen floor, or are there bloodstains on the kitchen floor?",
            answers = new string[] { "Footprints", "Bloodstains" },
            correctAnswer = 0
        });

        questions.Add(new Question
        {
            questionText = "Is the murdered woman lying in her bed, or is she lying next to her bed?",
            answers = new string[] { "In her bed", "Next to her bed" },
            correctAnswer = 1
        });

        questions.Add(new Question
        {
            questionText = "Is the bed and ground free of blood, or are the bed and ground covered in blood?",
            answers = new string[] { "Free of blood", "Covered in blood" },
            correctAnswer = 1
        });

        questions.Add(new Question
        {
            questionText = "IS the note with 'Remember Me ;)' located in her hand or on the nightstand?",
            answers = new string[] { "In hand", "Nightstand" },
            correctAnswer = 0
        });

        questions.Add(new Question
        {
            questionText = "Was the murder weapon a kitchen knife or a pocket knife?",
            answers = new string[] { "Kitchen Knife", "Pocket Knife" },
            correctAnswer = 1
        });

        questions.Add(new Question
        {
            questionText = "Was the pocket knife cleaned?",
            answers = new string[] { "Yes", "No" },
            correctAnswer = 0
        });

        questions.Add(new Question
        {
            questionText = "Did he leave through the door or the broken window?",
            answers = new string[] { "Door", "Broken window" },
            correctAnswer = 0
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
        }

        UpdateLivesUI();
    }

    public int GetCurrentQuestionIndex()
    {
        return currentQuestion;
    }
}