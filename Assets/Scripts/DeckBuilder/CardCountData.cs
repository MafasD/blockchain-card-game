using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeckBuilder
{
    // Hardcoded values for minimum and maximum values for the player's deck
    public class DeckCountsData
    {
        readonly int minCardCount = 12; // Minimum value of cards in player's deck
        readonly int maxCardCount = 30; // Maximum value of cards in player's deck

        public int GetMinCardsCount()
        { 
            return minCardCount; 
        }

        public int GetMaxCardsCount()
        { 
            return maxCardCount; 
        }
    }
}