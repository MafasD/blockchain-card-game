using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CardController
{
    bool isPlayersTurn = true; // Flag checking if it's players turn
    private void Awake()
    {
        if (animationManager == null && showAnimations)
            animationManager = GetComponent<PlayerAnimationManager>();
    }

    public override void MyTurn() // Player's turn starts
    {
        isPlayersTurn = true;
    }

    public override void EndOfMyTurn() // Player's turn ends.
    {
        isPlayersTurn = false;
    }

    public override void AddCardsToDiscardPile() // Functionality for adding cards to discard pile.
    {
        if (showAnimations)
        {
            isAnimationRunning = true;
            animationManager.SendCardToDiscardPile();
        }
        else
            myField.GetComponent<FieldHandler>().RemoveCards();
    }

    public bool CheckIfPlayersTurn() // Getter for player's turn flag (used in DragDropV2.cs)
    {
        if (isAnimationRunning)
            return false;

        return isPlayersTurn;
    }

}