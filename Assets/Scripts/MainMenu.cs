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

    // Start is called before the first frame update
    public void StartGame()
    {
        DeckSelectCanvas.enabled = true;
    }

    // Update is called once per frame
    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenDeckBuilder()
    {
        SceneManager.LoadScene("DeckBuilderScene");
    }
}
