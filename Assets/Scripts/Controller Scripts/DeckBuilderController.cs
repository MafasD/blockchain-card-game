using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckBuilderController : MonoBehaviour
{
    public TMP_Text[] elementsTMPs;
    public TMP_Text allCount;
    public DeckContentHandler contentHandler;
    readonly int minCardCount = 12; // Minimum value of cards in player's deck
    readonly int maxCardCount = 30; // Maximum value of cards in player's deck

    void Awake()
    {
        // Updates all element count values to 0 (text)
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
        SetAllCardsCount(contentHandler.GetChildCount());
    }

    // Sets the card count of all the cards (text)
    public void SetAllCardsCount(int count)
    {
        if(count < minCardCount)
            allCount.color = Color.red;
        else
            allCount.color = Color.green;

        allCount.text = $"{count}/{maxCardCount}";
    }
}
