using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectDeckMenu : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public PlayerDeckSingleton singleton;
    JsonLoad jsonLoad;

    private void Awake()
    {
        jsonLoad = new();
    }

    public void SelectDeckByValue()
    {
        int value = dropdown.value - 1;

        if (value < 0 && value > 4)
            value = 0;

        singleton.SetPlayerDeckID(value);
        Debug.Log(value);
        SceneManager.LoadScene("GameScene");
    }

}
