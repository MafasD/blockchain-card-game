using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public CardController playerController, enemyController; // Card controllers for bridging methods between player- and enemy fields
    CardController currentController; // Determines which controller is currently in use (player or enemy/bot)
    public TMP_Text testInfoTMP; // Shows information on the screen (for testing!)
    int turnCounter, roundCount; // Keeps track who is playing now (0=player, 1=enemy/bot, 2=round end)
    bool bothCardsRevealed = false; // Flag for checking up if both players cards are revealed

    readonly CompareCards compareCards = new(); // Object containing the functions for comparing player and enemy cards (elemnets)

    public Image playerHealthBar;
    public Image opponentHealthBar;
    public float damagePercentage = 0.1f; //0.1f = 10% damage. This can be adjusted in the MainController's inspector window

    public int maxRoundCount = 30; //Max round count for the match

    void Awake()
    {
        currentController = playerController; // Player starts the game
        currentController.MyTurn(); // Set the first turn.
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

        currentController.EndOfMyTurn(); // Inform to the current controller when turn ends

        if (turnCounter == 0) // Player's turn next
            currentController = playerController;

        else if (turnCounter == 1) // Enemy's turn next
            currentController = enemyController;

        else // Both players are ready -> reveal cards
        { 
            roundCount++;
            RevealCards();
            return;
        }

        currentController.MyTurn(); // Start turn method for the current controller
    }

    void RevealCards() // Set field cards visible
    {
        playerController.RevealCardFromField();
        enemyController.RevealCardFromField();
        bothCardsRevealed = true;
    }

    void CompareCards() // Compares both cards and determines who wins
    {
        Card playerCard = playerController.GetMyCard();
        Card enemyCard = enemyController.GetMyCard();
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

        if (roundCount >= maxRoundCount) //If max roundCount reached, match ends
        {
            Invoke("EndMatch", 2f); //Calls EndMatch() after 2 second delay
            return;
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
    //Match end method for max round reached. Based on remaining health, match can end in a win, loss or draw
    void EndMatch()
    {
        if (playerHealthBar.fillAmount > opponentHealthBar.fillAmount)
        {
            SceneManager.LoadScene("WinScene");
        }
        else if (playerHealthBar.fillAmount < opponentHealthBar.fillAmount)
        {
            SceneManager.LoadScene("LoseScene");
        }
        else
        {
            SceneManager.LoadScene("DrawScene");
        }
    }

    IEnumerator IEStartNewRound() // Coroutine for waiting a little before sending cards to discard pile
    {
        yield return new WaitForSeconds(2f); // How many seconds to wait before sending cards to discard piles.
        playerController.AddCardsToDiscardPile();
        enemyController.AddCardsToDiscardPile();
        testInfoTMP.text = "";
        NextTurn(); // Next turn can be set.
    }

    public void OnClickReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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