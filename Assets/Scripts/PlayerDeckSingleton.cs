using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Keeps track what player deck is selected.
// Saved json file will be loaded based on the playerDeckId value.

public class PlayerDeckSingleton : MonoBehaviour
{
    public static PlayerDeckSingleton instance;
    public int playerDeckId;

    private void Awake()
    {
        // If instance do not exist set this object singleton
        // Else destroy this object

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this);
    }

    public void SetPlayerDeckID(int id)
    {
        instance.playerDeckId = id;
    }

    public int GetPlayerDeckId()
    {
        return instance.playerDeckId;
    }
}
