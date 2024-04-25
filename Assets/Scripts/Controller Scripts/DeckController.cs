using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckController : MonoBehaviour
{
    public DeckManager playerDeckManager; 
    public DeckManager enemyDeckManager;
    public Transform playerHand;
    public Transform enemyHand;
    public Button drawButton;

    private void Start()
    {
        //Sets player's and enemy's hand references
        playerDeckManager.hand = playerHand;
        enemyDeckManager.hand = enemyHand;
        //Checks the initial state of the draw button
        CheckDrawButtonState();
    }
    //Method to draw cards for both player and enemy
    public void DrawCards()
    {
        playerDeckManager.DrawCard(5); //Draws 5 cards for the player
        enemyDeckManager.DrawCard(5); //Draws 5 cards for the enemy
        //Checks the state of the draw button after drawing cards
        CheckDrawButtonState();
    }
    //Method to check the state of the draw button
    public void CheckDrawButtonState()
    {
        //Checks if decks are not empty and that hands have at least one card or less (It's actually 0, but the counting does not work properly for some reason).
        bool playerDeckNotEmpty = playerDeckManager.deck.Count > 0 && playerHand.childCount <= 1;
        bool enemyDeckNotEmpty = enemyDeckManager.deck.Count > 0 && enemyHand.childCount <= 1;
        //If all conditions are met, draw button becomes interactable.
        drawButton.interactable = playerDeckNotEmpty && enemyDeckNotEmpty;
    }
}