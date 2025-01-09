using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject creditsCanvas;

    public void StartGame()
    {
        Debug.Log("Loading ClueScene...");
        SceneManager.LoadScene("ClueScene");
    }

    // ClueScene to Questions Scene
    public void StartQuestions()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGame();
        }

        Debug.Log("Loading Questions scene...");
        SceneManager.LoadScene("Questions");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
