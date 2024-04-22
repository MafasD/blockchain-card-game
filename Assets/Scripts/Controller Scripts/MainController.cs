using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public GameObject PlayerController, EnemyController;
    public Button nextRoundBtn;
    GameObject CurrentController;
    int turnCount = 0;
    bool isNextTurn = false;

    public static bool PlayerCardLock { get; set; }

    void Awake()
    {
        CurrentController = PlayerController;
        MainController.PlayerCardLock = false;
        nextRoundBtn.gameObject.SetActive(false);
    }

    void Update()
    {
        if(isNextTurn)
        {
            isNextTurn = false;
            StartCoroutine(IEStartNewRound());
            
        }
    }


    public void NextTurn()
    {
        if (CurrentController == PlayerController)
        {
            Debug.Log("Enemy turn");
            CurrentController = EnemyController;
            MainController.PlayerCardLock = true;
        }
        else
        {
            Debug.Log("Player turn");
            CurrentController = PlayerController;
            MainController.PlayerCardLock = false;
        }
        turnCount++;

        if(turnCount % 2 == 0)
        {
            RevealCards();
            return;
        }

        CurrentController.GetComponent<CardController>().MyTurn();
    }

    public void RevealCards()
    {
        PlayerController.GetComponent<CardController>().RevealCardFromField();
        EnemyController.GetComponent<CardController>().RevealCardFromField();
        nextRoundBtn.gameObject.SetActive(true);
        isNextTurn = true;
    }

    void CompareCards()
    {
        GameObject playerCard = PlayerController.GetComponent<CardController>().GetMyCard();
        GameObject enemyCard = EnemyController.GetComponent<CardController>().GetComponent<CardController>().GetMyCard();

        if(playerCard != null && enemyCard != null ) 
            return;
    }

    IEnumerator IECompareCards()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator IEStartNewRound()
    {
        yield return new WaitForSeconds(1f);
        PlayerController.GetComponent<CardController>().AddCardsToDiscardPile();
        EnemyController.GetComponent<CardController>().AddCardsToDiscardPile();
    }

}
