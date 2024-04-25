using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckBuilderMenu : MonoBehaviour
{
    public void OnClickReturn()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
