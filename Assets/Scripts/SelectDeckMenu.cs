using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// Dropdown script for the MainMenu scene.
// Attached to the Unity Dropdown that communicates with with the "SelectDeckByValue" method.

public class SelectDeckMenu : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    PlayerInfoSingleton singleton;

    private void Awake()
    {
        singleton = GameObject.FindWithTag("PlayerInfoSingleton").GetComponent<PlayerInfoSingleton>();
    }

    // Set deck index/value based on the dropdown value.
    public void SelectDeckByValue()
    {
        int value = dropdown.value - 1; // Gets dropdown value and drops it by 1 (don't count the first dropdown item).

        if (value < 0 && value > 4)
            value = 0;

        singleton.SetPlayerDeckID(value); // Sets singleton value that determines which player deck file (JSON) is loaded.
        SceneManager.LoadScene("GameScene"); // Load the gameplay scene.
    }

}
