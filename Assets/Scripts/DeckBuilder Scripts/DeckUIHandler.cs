using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DeckBuilder {
    public abstract class DeckUIHandler : MonoBehaviour
    {
        [SerializeField] TMP_Text[] elementCounts;
        [SerializeField] TMP_Text totalCount;
        [SerializeField] TMP_Text screenInfo;
        [SerializeField] DeckContentHandler playerDeckHandler;
        [SerializeField] DrawText drawText;
        readonly DeckCountsData deckCountsData = new();

        private void Awake()
        {
            screenInfo.text = string.Empty;
        }

        // Updates the TMP text value based on element's ID.
        // Element id's: 0 = water; 1 = ire; 2 = leaf;
        public void UpdateCardCountByElementID(int elementID, int count)
        {
            string txt;
            if (elementID == 0)
                txt = $"Water: {count}";

            else if (elementID == 1)
                txt = $"Fire: {count}";

            else
                txt = $"Leaf: {count}";

            elementCounts[elementID].text = txt;
        }

        // Sets the card count of all the cards (text)
        public void UpdateTotalCardCount(int count)
        {
            if (count < deckCountsData.GetMinCardsCount())
                totalCount.color = Color.red;
            else
                totalCount.color = Color.green;

            totalCount.text = $"{count}/{deckCountsData.GetMaxCardsCount()}";
        }

        public void ResetAllCardCounts()
        {
            for (int i = 0; i < elementCounts.Length; i++)
            {
                UpdateCardCountByElementID(i, 0);
            }

            UpdateTotalCardCount(0);
        }

        public void ShowCardWasDeletedInfo()
        {
            drawText.ShowInfoOnScreen("Card deleted from the deck", 2f);
        }

    }
}
