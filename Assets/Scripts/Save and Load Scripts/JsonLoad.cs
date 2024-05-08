using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Class for loading cards from the json file located in the persistent data path.
public class JsonLoad
{
    // Root where the file is located in the disk
    string dataPath;

    // Constructor that sets the data path.
    public JsonLoad()
    {
        dataPath = Application.persistentDataPath + "/";
    }

    // Loads player's card data based on the deck index number.
    // Returns an array of class.
    public CardData[] LoadCardsData(int deckIndex)
    {
        string file = $"Deck{deckIndex}.json";

        // Checks if file do not exists
        if (!CheckIfJsonSaveExists(deckIndex))
            return null;

        // Reads the data as a text/string
        string data = File.ReadAllText(dataPath + file);
        // Create JSON array that holds the card data.
        CardData[] jsonData = JsonHelper.FromJson<CardData>(data);

        return jsonData;
    }

    public bool CheckIfJsonSaveExists(int deckIndex)
    {
        string file = $"Deck{deckIndex}.json";

        if (File.Exists(dataPath + file))
            return true;

        return false;
    }
}
