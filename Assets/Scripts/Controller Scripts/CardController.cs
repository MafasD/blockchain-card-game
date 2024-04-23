using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract class that is inherited by PlayerController and EnemyController

public abstract class CardController : MonoBehaviour
{
    public GameObject MyField; // My field parent object where cards are dropped; My hand cards parent object

    public abstract void MyTurn(); // Called when turn starts

    public abstract void EndOfMyTurn(); // Called when turn starts (player)

    public abstract void AddCardsToDiscardPile(); // Remove card from the field -> move it to the discard pile

    public void RevealCardFromField() // Reveals card from the field to the all players
    {
        MyField.GetComponent<FieldHandler>().RevealCards();
    }
    public Card GetMyCard() // Get card information as a Card object (class)
    {
        return MyField.GetComponent<FieldHandler>().GetCard();
    }

}
