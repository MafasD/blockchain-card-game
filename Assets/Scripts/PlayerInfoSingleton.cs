using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Keeps track which player deck is selected.
// Saved json file will be loaded based on the playerDeckId value.

public class PlayerInfoSingleton : MonoBehaviour
{
    public static PlayerInfoSingleton instance;
    [SerializeField] int playerDeckId;

    private void Awake()
    {
        // If this instance do not exist -> make it singleton.
        // Else destroy this object on awake.

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this);
    }

    // Sets the player's deck by index/id that can be loaded on the GameScene.
    public void SetPlayerDeckID(int id)
    {
        instance.playerDeckId = id;
    }

    // Used to get the current player's deck by index/id.
    public int GetPlayerDeckId()
    {
        return instance.playerDeckId;
    }
}
