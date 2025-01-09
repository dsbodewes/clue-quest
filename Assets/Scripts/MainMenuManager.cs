using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject creditsCanvas;

    public void StartGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGame();
        }

        Debug.Log("Loading ClueScene...");
        SceneManager.LoadScene("ClueScene");
    }

    // ClueScene to Questions Scene
    public void StartQuestions()
    {
        Debug.Log("Loading Questions scene...");
        SceneManager.LoadScene("Questions");
    }

    public void ShowCredits()
    {
        mainMenu.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    public void BackToMenu()
    {
        mainMenu.SetActive(true);
        creditsCanvas.SetActive(false);
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
