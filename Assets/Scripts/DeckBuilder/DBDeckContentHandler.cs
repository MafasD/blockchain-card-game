using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

namespace DeckBuilder
{
    public class DeckContentHandler : MonoBehaviour
    {
        public CardStatsController statsController;
        public GameObject simpleCardPrefab;
        Vector2 cardScale = new Vector2(0.6f, 0.6f);
        [SerializeField] GameObject[] childs;

        private void Awake()
        {
            childs = new GameObject[transform.childCount];

            for (int i = 0; i < childs.Length; i++)
            {
                childs[i] = transform.GetChild(i).gameObject;

            }


        }

        // Spawns a new card to the player's deck
        public void AddCard(GameObject givenCard, int elementID, bool isAddedByHand)
        {
            GameObject card;

            if (isAddedByHand)
                card = Instantiate(givenCard);
            else
                card = givenCard;

            card.transform.SetParent(transform.GetChild(elementID));
            card.GetComponent<SimpleDragDrop>().SetIsDeckCard(true);
            //card.transform.localScale = givenCard.transform.localScale; // Keeps card size the same
            card.transform.localScale = cardScale; // 
            int count = childs[elementID].transform.childCount;
            statsController.UpdateCardCount(elementID, count);

        }

        // Get count of all the element childs
        public int GetChildCount()
        {
            int count = 0;

            for (int i = 0; i < childs.Length; i++)
            {
                count += childs[i].transform.childCount;
            }

            return count;
        }

        public void CreateAllCards(CardData[] cardData)
        {
            DeleteAllCards();

            foreach (var item in cardData)
            {
                Debug.Log(item.cardID + ":" + item.element);
                GameObject card = Instantiate(simpleCardPrefab);
                card.GetComponent<InitCardPrefab>().SetElement(item.element);
                AddCard(card, item.element, false);
            }
        }

        // Converts all the child object to a list of CardData's
        public CardData[] GetAllCardData()
        {
            List<CardData> dataList = new();
            int cardCount = 0;
            for (int i = 0; i < childs.Length; i++)
            {
                foreach (Transform card in childs[i].transform)
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

        // Deletes the card game object completely
        public void DeleteCard(int elementID)
        {
            int count = childs[elementID].transform.childCount;
            statsController.UpdateCardCount(elementID, count);
        }

        public void DeleteAllCards()
        {
            for (int i = 0; i < childs.Length; i++)
            {
                foreach (Transform child in childs[i].transform)
                {
                    Destroy(child.gameObject);
                }
            }
            int[] elementCount = { 0, 0, 0 };
            statsController.SetAllCardCounts(elementCount, 0);
        }
    }

}