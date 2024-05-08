using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CardController
{
    [SerializeField] GameObject myHandCards;
    SimpleBot bot;

    private void Awake()
    {
        if(animationManager == null && showAnimations)
            animationManager = GetComponent<EnemyAnimationManager>();

        bot = new SimpleBot(myHandCards); // Creates a bot
    }

    public override void MyTurn()
    {
        //botTurn = true; // Activate the bot's turn -> Controlled in Update method.
        StartCoroutine(BotPlayStart());
    }

    public override void EndOfMyTurn() { }

    public override void AddCardsToDiscardPile()
    {
        if (showAnimations) // If animations are enabled.
        {
            isAnimationRunning = true;
            animationManager.SendCardToDiscardPile(); // Set the send card to discard pile animation.
        }  
        else
            MyField.GetComponent<FieldHandler>().RemoveCards(); // Removes all the cards in the field. (No animations)
    }

    IEnumerator BotPlayStart() // Currently chooses random card from deck
    {
        float waitTime = 1f;
        yield return new WaitForSeconds(waitTime); // Waits a little bit before continuing

        GameObject pickACard = bot.ChooseRandomCard();
        
        //Sets the card's scale to 1.2x, because for some reason the OpponentHand affects the scaling.
        pickACard.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

        if (showAnimations) // If animations are enabled.
        {
            isAnimationRunning = true;
            animationManager.SendCardToMyField(pickACard); // Set the draw card animation for the chosen card.
        }
            
        else
            MyField.GetComponent<FieldHandler>().AddCard(pickACard); // Adds card manually to the field. (No animations)

    }

}


// A simple bot that can choose random card from the hand
public class SimpleBot
{
    readonly GameObject myHandCards; // Parent for the bot's hand cards

    public SimpleBot(GameObject myHandCards) // Constructor which needs hand cards object (parent)
    {
        this.myHandCards = myHandCards;
    }

    public GameObject ChooseRandomCard() // Chooses a card from the hand by random number
    {
        int rand = Random.Range(0, myHandCards.transform.childCount);
        return myHandCards.transform.GetChild(rand).gameObject;
    }
}