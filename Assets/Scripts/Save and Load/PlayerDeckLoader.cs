using DeckBuilder;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerDeckLoader : MonoBehaviour
{
    public GameObject playerDeck;
    [SerializeField] GameObject cardPrefab;
    JsonLoad jsonLoad;

    private void OnEnable()
    {
        jsonLoad = new JsonLoad();
        // Gets ID for which deck will be loaded.
        int playerDeckId = PlayerDeckSingleton.instance.GetPlayerDeckId();

        // Checks if JSON file exists in the persistent data path.
        if (!jsonLoad.CheckIfJsonSaveExists(playerDeckId))
        {
            CreateBasicPlayerDeck();
            return;
        }

        CreatePlayerDeckFromJson(playerDeckId);
    }

    // Creates a List of cards based on the JSON file content.
    // Sends the List to the player's DeckManager.
    private void CreatePlayerDeckFromJson(int deckIndex)
    {
        CardData[] cardData = jsonLoad.LoadCardsData(deckIndex);

        List<GameObject> cards = new();

        // Instantiates new card objects and initializes card info based on given data.
        foreach (var item in cardData)
        {
            GameObject card = cardPrefab;
            card.GetComponent<InitCardPrefab>().SetElement(item.element);
            cards.Add(Instantiate(card, playerDeck.transform));
        }

        playerDeck.GetComponent<DeckManager>().SetDeckCards(cards);
        Debug.Log($"Player deck {deckIndex} loaded from Json file");
    }

    // Creates a List of basic cards. (hard coded)
    // Sends the List to the player's DeckManager.
    private void CreateBasicPlayerDeck()
    {
        List<GameObject> cards = new();
        int currentElement = 0;

        for (int i = 0; i < 10; i++)
        {
            GameObject card = cardPrefab;
            card.GetComponent<InitCardPrefab>().SetElement(currentElement);
            cards.Add(Instantiate(card, playerDeck.transform));

            currentElement++;
            if (currentElement > 2)
                currentElement = 0;
        }
        
        playerDeck.GetComponent<DeckManager>().SetDeckCards(cards);
        Debug.Log("Basic player deck loaded");
    }

}