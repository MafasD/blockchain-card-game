using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsParent;

    private void Awake()
    {
        settingsParent.SetActive(false); // Hide the settings at the start.
    }

    public void OpenSettingsWindow()
    {
        settingsParent.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingsParent.SetActive(false);
    }

    
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    
    public void ExitGame()
    {
        Application.Quit();
    }

}
