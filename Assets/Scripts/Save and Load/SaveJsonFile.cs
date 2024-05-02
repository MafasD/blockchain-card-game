using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveJsonFile
{
    List<CardData> cardDatas;
    string dataPath;

    public SaveJsonFile()
    {
        dataPath = Application.persistentDataPath + "/";
    }

    public void SaveAllCardData(int deckIndex, CardData[] cardData)
    {
        string filePath = dataPath + $"Deck{deckIndex}.json";
        string saveJson = JsonHelper.ToJson(cardData, true);

        File.WriteAllText(filePath, saveJson);

        Debug.Log("Card data saved succesfully!");

    }

    private void SaveCard(CardData cardData, string path)
    {
        string savePlayerData = JsonUtility.ToJson(cardData);
        File.WriteAllText(dataPath, savePlayerData);
    }
}
