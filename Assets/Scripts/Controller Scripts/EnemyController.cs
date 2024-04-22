using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CardController
{
    bool botTurn = false;

    void Update()
    {
        if (botTurn)
        {
            StartCoroutine(BotPlayStart());
            botTurn = false;
        }
    }


    public override void AddCardToField(GameObject card, bool addToMyField)
    {
        if(addToMyField)
            MyField.GetComponent<FieldHandler>().AddCard(card);
    }

    public override void MyTurn()
    {
        botTurn = true;
    }

    public override void AddCardsToDiscardPile()
    {
        MyField.GetComponent<FieldHandler>().RemoveCards();
    }

    IEnumerator BotPlayStart() // Currently chooses random card from deck
    {
        yield return new WaitForSeconds(1f);
        int index = Random.Range(0, MyHandCards.transform.childCount);
        GameObject myCard = MyHandCards.transform.GetChild(index).gameObject;
        MyField.GetComponent<FieldHandler>().AddCard(myCard);

    }

    public override GameObject GetMyCard()
    {
        return MyField.GetComponent<FieldHandler>().GetCard();
    }
}
