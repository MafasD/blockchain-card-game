using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<GameObject> deck; //List of all cards in the deck
    public Transform hand; //Reference to the player's or opponent's hand
    public DeckController deckController; //Reference to the DeckController

    // This method is called when the deck is loaded by a script.
    public void SetDeckCards(List<GameObject> deck)
    {
        this.deck = deck;
    }

    //Method for drawing cards from decks
    public void DrawCard(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (deck.Count > 0)
            {
                //Selects a random card from the deck
                GameObject drawnCard = deck[Random.Range(0, deck.Count)];
                //Removes the drawn card from the deck
                deck.Remove(drawnCard);
                //Sets the parent of the drawn card to either player's or opponent's hand
                drawnCard.transform.SetParent(hand);
                //Activates the drawn card, since they are initially set to inactive
                drawnCard.SetActive(true);
            }
            else
            {
                Debug.Log("No cards left in the deck."); //This could be changed to a "pop-up" text
                break;
            }
        }
        //Updates draw button state after drawing cards
        deckController.CheckDrawButtonState(); 
    }
}