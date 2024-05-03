using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Class for saving cards to json format to the persistent data path.
public class JsonSave
{
    readonly string dataPath;

    public JsonSave()
    {
        dataPath = Application.persistentDataPath + "/";
    }

    public void SaveAllCardData(int deckIndex, CardData[] cardData)
    {
        string filePath = dataPath + $"Deck{deckIndex}.json";
        string saveJson = JsonHelper.ToJson(cardData, true);

        File.WriteAllText(filePath, saveJson);

    }

}
