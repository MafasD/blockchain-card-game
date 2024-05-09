using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsParent;
    private bool settingsOn = false;

    public void OnClickToggleSettings()
    {
        settingsOn = !settingsOn;
        settingsParent.SetActive(settingsOn);
    }
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Update is called once per frame
    public void ExitGame()
    {
        Application.Quit();
    }

}
