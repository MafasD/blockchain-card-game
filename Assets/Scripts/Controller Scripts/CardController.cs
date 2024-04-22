using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardController : MonoBehaviour
{
    public GameObject MyField, MyHandCards;

    public abstract void AddCardToField(GameObject card); // Adds a card to the field from your hand

    public abstract void MyTurn();

    public abstract Card GetMyCard();

    public abstract void AddCardsToDiscardPile();

    public void RevealCardFromField()
    {
        MyField.GetComponent<FieldHandler>().RevealCards();
    }

}
