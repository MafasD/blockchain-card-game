using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas DeckSelectCanvas;

    private void Awake()
    {
        DeckSelectCanvas.enabled = false;
    }


    public void StartGame()
    {
        DeckSelectCanvas.enabled = true; // Opens a little popup UI for the deck selection.
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenDeckBuilder()
    {
        SceneManager.LoadScene("DeckBuilderScene");
    }
}
