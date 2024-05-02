using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckController : MonoBehaviour
{
    public DeckManager playerDeckManager;
    public List<DeckManager> enemyDeckManager;
    public Transform playerHand;
    public Transform enemyHand;
    public Button drawButton;

    private DeckManager chosenEnemyDeck;

    private void Start()
    {
        //Sets player's and enemy's hand references
        playerDeckManager.hand = playerHand;
        //Chooses a random enemy deck
        int randomIndex = Random.Range(0, enemyDeckManager.Count);
        // Set enemy's hand reference
        chosenEnemyDeck = enemyDeckManager[randomIndex];
        chosenEnemyDeck.hand = enemyHand;
        //Checks the initial state of the draw button
        CheckDrawButtonState();
    }
    //Method to draw cards for both player and enemy
    public void DrawCards()
    {
        playerDeckManager.DrawCard(5); //Draws 5 cards for the player
        chosenEnemyDeck.DrawCard(5); //Draws 5 cards from the randomly chosen enemy deck
        //Checks the state of the draw button after drawing cards
        CheckDrawButtonState();
    }
    //Method to check the state of the draw button
    public void CheckDrawButtonState()
    {
        //Checks if deck is not empty and if hand is empty
        bool playerDeckNotEmpty = playerDeckManager.deck.Count > 0 && playerHand.childCount == 0;
        //If all conditions are met, draw button becomes interactable
        drawButton.interactable = playerDeckNotEmpty;
    }
}