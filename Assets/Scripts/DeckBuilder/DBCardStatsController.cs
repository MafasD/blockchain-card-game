using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DeckBuilder {
    public class CardStatsController : MonoBehaviour
    {
        public TMP_Text[] elementsTMPs;
        public TMP_Text totalCount;
        public DeckContentHandler contentHandler;
        readonly int minCardCount = 12; // Minimum value of cards in player's deck
        readonly int maxCardCount = 30; // Maximum value of cards in player's deck

        void Awake()
        {
            for (int i = 0; i < elementsTMPs.Length; i++)
            {
                UpdateCardCount(i, 0);
            }
        }

        // Updates the TMP text value based on element's ID.
        // Element id's: 0=water; 1=fire; 2=leaf;
        public void UpdateCardCount(int elementID, int count)
        {
            string txt;
            if (elementID == 0)
                txt = $"Water: {count}";

            else if (elementID == 1)
                txt = $"Fire: {count}";

            else
                txt = $"Leaf: {count}";

            elementsTMPs[elementID].text = txt;
            SetTotalCardsCount(contentHandler.GetChildCount());
        }

        // Sets the card count of all the cards (text)
        public void SetTotalCardsCount(int count)
        {
            if (count < minCardCount)
                totalCount.color = Color.red;
            else
                totalCount.color = Color.green;

            totalCount.text = $"{count}/{maxCardCount}";
        }

        public void SetAllCardCounts(int[] elementCounts, int totalCount)
        {
            for (int i = 0; i < elementCounts.Length; i++)
            {
                UpdateCardCount(i, elementCounts[i]);
            }

        }
    }
}
