using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CardController
{
    public override void AddCardToField(GameObject card)
    {

    }

    public override void MyTurn()
    {

    }

    public override void AddCardsToDiscardPile()
    {

        MyField.GetComponent<FieldHandler>().RemoveCards();
        
    }

    public override Card GetMyCard()
    {
        return MyField.GetComponent<FieldHandler>().GetCard();
    }
}
