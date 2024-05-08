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

    public override void MyTurn()
    {
        isPlayersTurn = true;
    }

    public override void EndOfMyTurn()
    {
        isPlayersTurn = false;
    }

    public override void AddCardsToDiscardPile()
    {
        if(showAnimations)
            animationManager.SendCardToDiscardPile();
        else
            MyField.GetComponent<FieldHandler>().RemoveCards();
    }

    public void SetIsPlayersTurn(bool isPlayersTurn)
    {
        this.isPlayersTurn = isPlayersTurn;
    }

    public bool CheckIfPlayersTurn() // Getter for player's turn flag (used in DragDropV2.cs)
    {
        return isPlayersTurn;
    }

}
