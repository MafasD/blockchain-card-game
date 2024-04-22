using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public GameObject PlayerController, EnemyController;
    public Button nextRoundBtn;
    GameObject CurrentController;
    int turnCount, roundCount;
    bool startNewTurn = false;

    CompareCards compareCards = new();
    public static bool isPlayersTurn { get; set; }

    void Awake()
    {
        CurrentController = PlayerController;
        MainController.isPlayersTurn = true;
        nextRoundBtn.gameObject.SetActive(false);
    }

    void Update()
    {
        if(startNewTurn)
        {
            startNewTurn = false;
            CompareCards();         
        }
    }


    public void NextTurn()
    {
        turnCount++;

        if(turnCount == 0)
        {
            CurrentController = PlayerController;
        }
        else if (turnCount == 1)
        {
            CurrentController = EnemyController;
        }
        else
        { 
            turnCount = 0;
            roundCount++;
            RevealCards();
            return;
        }
        MainController.isPlayersTurn = turnCount == 0;
        CurrentController.GetComponent<CardController>().MyTurn();
    }

    public void RevealCards()
    {
        PlayerController.GetComponent<CardController>().RevealCardFromField();
        EnemyController.GetComponent<CardController>().RevealCardFromField();
        nextRoundBtn.gameObject.SetActive(true);
        startNewTurn = true;
    }

    void CompareCards()
    {
        Card playerCard = PlayerController.GetComponent<CardController>().GetMyCard();
        Card enemyCard = EnemyController.GetComponent<CardController>().GetComponent<CardController>().GetMyCard();
        int results = compareCards.Compare(playerCard, enemyCard);
        
        if(results == 0) // DRAW
        {
            Debug.Log($"Round {roundCount}: draw");
        }
        else if(results == 1) // PLAYER WINS
        {
            Debug.Log($"Round {roundCount}: player wins");
        }
        else // ENEMY WINS
        {
            Debug.Log($"Round {roundCount}: enemy wins");
        }

        StartCoroutine(IEStartNewRound());
    }

    IEnumerator IEStartNewRound()
    {
        yield return new WaitForSeconds(3f);
        PlayerController.GetComponent<CardController>().AddCardsToDiscardPile();
        EnemyController.GetComponent<CardController>().AddCardsToDiscardPile();
        MainController.isPlayersTurn = true;
    }

}


public class CompareCards
{
    public int Compare(Card playerCard, Card enemyCard) // 0 = draw, 1 = player, 2 = enemy
    {
        int result = CompareElements(playerCard.elementID, enemyCard.elementID);
        return result;
    }

    private int CompareElements(int playerElement, int enemyElement) // 0 = draw, 1 = player, 2 = enemy
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