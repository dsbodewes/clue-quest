using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject creditsCanvas;
    public GameObject settingsCanvas;

    public void StartGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGame();
        }

        SceneManager.LoadScene("ClueScene");
    }

    // ClueScene to Questions Scene
    public void StartQuestions()
    {
        SceneManager.LoadScene("Questions");
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        settingsCanvas.SetActive(true);
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
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}