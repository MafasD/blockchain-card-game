using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

// This script is attached inside the player Scroll View's "Content" object.
// "Content" object needs to have 3 childs where the player's cards are spawned based on element id values.

namespace DeckBuilder
{
    // This class handles player's deck card objects.
    public class DeckContentHandler : DeckUIHandler
    {
        public DeckBuilderController deckBuilderController;
        GameObject[] elementParents; // Array of parent objects where cards are spawned by the element id; 0=water; 1=fire; 2=leaf;
        public GameObject simpleCardPrefab; // Card object that will be spawned to the elementParents.
        Vector2 cardScale = new Vector2(0.6f, 0.6f); // hotfix -> card's size changes when changing parent object.
        int[] cardCounts;
        int totalCardCount;

        private void Awake()
        {
            // Initializes array of game objects.
            elementParents = new GameObject[transform.childCount];
            // Sets the array of elements.
            for (int i = 0; i < elementParents.Length; i++)
            {
                elementParents[i] = transform.GetChild(i).gameObject;

            }
            cardCounts = new int[elementParents.Length];
        }

        // Spawns a new card to the player's deck

        public void AddCard(GameObject givenCard, int elementID)
        {
            GameObject card = Instantiate(givenCard);

            card.transform.SetParent(transform.GetChild(elementID));
            card.GetComponent<SimpleDragDrop>().SetIsPlayerDeckCard(true);
            card.transform.localScale = cardScale;

            //deckBuilderController.CardAdded(elementID);
            cardCounts[elementID]++;
            totalCardCount++;

            UpdateCardCountByElementID(elementID, cardCounts[elementID]);
            UpdateTotalCardCount(totalCardCount);
        }

        public int GetChildCountByIndex(int value)
        {
            return elementParents[value].transform.childCount;
        }

        // Get count of all the element childs
        public int GetTotalChildCount()
        {
            int count = 0;

            for (int i = 0; i < elementParents.Length; i++)
            {
                count += elementParents[i].transform.childCount;
            }

            return count;
        }

        // Deletes all the existing card objects from the player's deck.
        // Spawns new card objects to the player's deck.
        public void CreateAllCards(CardData[] cardData)
        {
            DeleteAllCards();

            if(cardData == null)
            {
                for (int i = 0; i < elementParents.Length; i++)
                {
                    UpdateCardCountByElementID(i, cardCounts[i]);
                    totalCardCount += cardCounts[i];
                    
                }

                UpdateTotalCardCount(totalCardCount);
                return;
            }

            // Instantiates new card objects and initializes card info based on given data.
            foreach (var item in cardData)
            {
                GameObject card = Instantiate(simpleCardPrefab);
                card.GetComponent<InitCardPrefab>().SetElement(item.element);
                card.GetComponent<SimpleDragDrop>().SetIsPlayerDeckCard(true);

                card.transform.SetParent(transform.GetChild(item.element));
                card.transform.localScale = cardScale;

                cardCounts[item.element]++;
            }


            for (int i = 0; i < elementParents.Length; i++)
            {

                UpdateCardCountByElementID(i, cardCounts[i]);
                totalCardCount += cardCounts[i];
            }

            UpdateTotalCardCount(totalCardCount);

        }

        // Converts all the child object to a list of CardData's
        public CardData[] GetAllCardData()
        {
            List<CardData> dataList = new();
            int cardCount = 0;
            for (int i = 0; i < elementParents.Length; i++)
            {
                foreach (Transform card in elementParents[i].transform)
                {
                    int elementID = card.GetComponent<InitCardPrefab>().GetElementID();

                    CardData cardData = new()
                    {
                        cardID = cardCount,
                        element = elementID,
                        isNFT = false, // Not implemented yet
                        sprite = null, // Not implemented yet
                    };
                    dataList.Add(cardData);
                    cardCount++;
                }
            }

            return dataList.ToArray();
        }

        // Deletes the given game object.
        // Gives command to update the card count in UI based on element id number.
        public void DeleteCard(GameObject cardObj)
        {
            int elementID = cardObj.GetComponent<InitCardPrefab>().GetElementID();
            Destroy(cardObj);
            // Current child count of the current element.
            cardCounts[elementID]--;
            totalCardCount--;
            UpdateCardCountByElementID(elementID, cardCounts[elementID]);
            UpdateTotalCardCount(totalCardCount);
            ShowCardWasDeletedInfo();
        }

        // Destoys all the childs that elementParents have.
        public void DeleteAllCards()
        {
            for (int i = 0; i < elementParents.Length; i++)
            {
                foreach (Transform child in elementParents[i].transform)
                {
                    Destroy(child.gameObject);
                }
                // Reset card counts
                cardCounts[i] = 0;
            }
            totalCardCount = 0;
        }
    }

}