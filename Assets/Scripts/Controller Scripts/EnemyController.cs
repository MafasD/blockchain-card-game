using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CardController
{
    bool botTurn = false; // Flag for checking when bot/enemy starts
    [SerializeField] GameObject MyHandCards;
    SimpleBot Bot;

    private void Awake()
    {
        Bot = new SimpleBot(MyHandCards); // Creates a bot
    }

    void Update()
    {
        if (botTurn) // Bot gameplay starts
        {
            StartCoroutine(BotPlayStart());
            botTurn = false;
        }
    }

    public override void MyTurn()
    {
        botTurn = true;
    }

    public override void EndOfMyTurn()
    {

    }

    public override void AddCardsToDiscardPile()
    {
        MyField.GetComponent<FieldHandler>().RemoveCards();
    }

    IEnumerator BotPlayStart() // Currently chooses random card from deck
    {
        float waitTime = 1f;
        yield return new WaitForSeconds(waitTime); // Waits a little bit before continuing

        GameObject pickACard = Bot.ChooseRandomCard();
        
        //Sets the card's scale to 1.2x, because for some reason the OpponentHand affects the scaling.
        pickACard.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        MyField.GetComponent<FieldHandler>().AddCard(pickACard); // Adds card manually to the field

    }

}


// A simple bot that can choose random card from the hand
public class SimpleBot
{
    readonly GameObject MyHandCards; // Parent for the bot's hand cards

    public SimpleBot(GameObject myHandCards) // Constructor which needs hand cards object (parent)
    {
        MyHandCards = myHandCards;
    }

    public GameObject ChooseRandomCard() // Chooses a card from the hand by random number
    {
        int rand = Random.Range(0, MyHandCards.transform.childCount);
        return MyHandCards.transform.GetChild(rand).gameObject;
    }
}