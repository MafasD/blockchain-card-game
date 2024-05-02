using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadJsonFile
{
    List<CardData> cards;
    string dataPath;

    public LoadJsonFile()
    {
        dataPath = Application.persistentDataPath + "/";
    }

    public CardData[] LoadCardsData(int deckIndex)
    {
        string file = $"Deck{deckIndex}.json";

        if (!File.Exists(dataPath + file))
        {
            Debug.Log($"{file} was not loaded - File does not exist");
            return null;
        }

        string data = File.ReadAllText(dataPath + file);
        CardData[] jsonData = JsonHelper.FromJson<CardData>(data);
        Debug.Log(jsonData.Length);

        //string saveJson = JsonHelper.FromJson(data);

        return jsonData;
    }

    private CardData LoadCardData()
    {


        return new CardData();
    }
}
