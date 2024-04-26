using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public GameObject PlayerController, EnemyController; // Card controllers for bridging methods between player- and enemy fields
    GameObject CurrentController; // Determines which controller is currently in use (player or enemy/bot)
    public TMP_Text testInfoTMP; // Shows information on the screen (for testing!)
    int turnCounter, roundCount; // Keeps track who is playing now (0=player, 1=enemy/bot, 2=round end)
    bool bothCardsRevealed = false; // Flag for checking up if both players cards are revealed

    readonly CompareCards compareCards = new(); // Object containing the functions for comparing player and enemy cards (elemnets)

    public Image playerHealthBar;
    public Image opponentHealthBar;
    public float damagePercentage = 0.1f; //0.1f = 10% damage. This can be adjusted in the MainController's inspector window

    void Awake()
    {
        CurrentController = PlayerController; // Player starts the game
        CurrentController.GetComponent<CardController>().MyTurn();
        testInfoTMP.text = "";
    }

    void Update()
    {
        if(bothCardsRevealed) // When both cards are visible -> compare those cards
        {
            bothCardsRevealed = false;
            CompareCards();
        }
    }

    public void NextTurn() // When card is placed on the field -> this method is called manually from the "FieldHandler" script
    {
        if (turnCounter > 1)
            turnCounter = 0;

        else
            turnCounter++;

        CurrentController.GetComponent<CardController>().EndOfMyTurn(); // Inform to the current controller when turn ends

        if (turnCounter == 0) // Player's turn next
            CurrentController = PlayerController;

        else if (turnCounter == 1) // Enemy's turn next
            CurrentController = EnemyController;

        else // Both players are ready -> reveal cards
        { 
            roundCount++;
            RevealCards();
            return;
        }

        CurrentController.GetComponent<CardController>().MyTurn(); // Start turn method for the current controller
    }

    void RevealCards() // Set field cards visible
    {
        PlayerController.GetComponent<CardController>().RevealCardFromField();
        EnemyController.GetComponent<CardController>().RevealCardFromField();
        bothCardsRevealed = true;
    }

    void CompareCards() // Compares both cards and determines who wins
    {
        Card playerCard = PlayerController.GetComponent<CardController>().GetMyCard();
        Card enemyCard = EnemyController.GetComponent<CardController>().GetComponent<CardController>().GetMyCard();
        int results = compareCards.Compare(playerCard, enemyCard);
        
        if(results == 0) // Draw
        {
            testInfoTMP.text = $"Round {roundCount}: Draw";

        }
        else if(results == 1) // Player wins
        {
            testInfoTMP.text = $"Round {roundCount}: You win!";

            DamageOpponent(); //Player wins = opponent takes damage
        }
        else // Enemy wins
        {
            testInfoTMP.text = $"Round {roundCount}: You lose";

            DamagePlayer(); //Player loses = player takes damage
        }

        StartCoroutine(IEStartNewRound());
    }
    //Method for damaging player
    void DamagePlayer()
    {
        //Subtracts the damage from the healthbar fill amount
        playerHealthBar.fillAmount -= damagePercentage;
        //Checks if game should end based on current health
        CheckEndGame(playerHealthBar);
    }
    //Method for damaging opponent
    void DamageOpponent()
    {
        opponentHealthBar.fillAmount -= damagePercentage;
        CheckEndGame(opponentHealthBar);
    }
    //Method for match end
    void CheckEndGame(Image healthBar)
    {
        if (healthBar.fillAmount <= 0.01) //If either healthbar value is lower than or equal to 0.01, match ends and player will be directed to a match end scene.
        {
            if (healthBar == playerHealthBar)
            {
                SceneManager.LoadScene("LoseScene");
            }
            else if (healthBar == opponentHealthBar)
            {
                SceneManager.LoadScene("WinScene");
            }
        }
    }


    IEnumerator IEStartNewRound() // Coroutine for waiting a little before sending cards to discard pile
    {
        yield return new WaitForSeconds(3f);
        PlayerController.GetComponent<CardController>().AddCardsToDiscardPile();
        EnemyController.GetComponent<CardController>().AddCardsToDiscardPile();
        testInfoTMP.text = "";
        NextTurn();
    }

}


public class CompareCards // Compares two cards based on the element id's
{
    public int Compare(Card playerCard, Card enemyCard)
    {
        int result = CompareElements(playerCard.elementID, enemyCard.elementID);
        return result; // 0 = draw, 1 = player, 2 = enemy
    }

    private int CompareElements(int playerElement, int enemyElement)
    {
        // Draw
        if (playerElement == enemyElement)
            return 0;

        // Player wins
        else if (playerElement == 0 && enemyElement == 1 ||   // water vs fire
            playerElement == 1 && enemyElement == 2 ||  // fire vs leaf
            playerElement == 2 && enemyElement == 0)    // leaf vs water
            return 1;

        // enemy wins
        else
            return 2;
    }
}