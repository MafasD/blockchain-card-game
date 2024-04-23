using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CardController
{
    bool isPlayersTurn = true; // Flag checking if it's players turn
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
        MyField.GetComponent<FieldHandler>().RemoveCards();
    }

    public void SetIsPlayersTurn(bool isPlayersTurn)
    {
        this.isPlayersTurn = isPlayersTurn;
    }

    public bool CheckIfPlayersTurn()
    {
        return isPlayersTurn;
    }

}
