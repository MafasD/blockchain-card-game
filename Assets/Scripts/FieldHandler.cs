using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldHandler : MonoBehaviour
{
    public GameObject MainController;
    GameObject MyCard;

    private void Awake()
    {
        if(MainController == null)
            MainController = GameObject.FindWithTag("MainController");
    }

    public void AddCard(GameObject child)
    {
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);

        MyCard = child;

        MyCard.transform.SetParent(transform, false);
        MyCard.transform.position = transform.position;

        MainController.GetComponent<MainController>().NextTurn();
    }

    public Card GetCard()
    {
        int elementID = MyCard.GetComponent<CardPrefabInitScript>().GetElementID();
        int damage = 1;
        Card card = new Card(elementID,damage);
        return card;
    }

    public void RevealCards()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).GetComponent<CardPrefabInitScript>().SetCardVisibleToOthers();
        }
    }

    public void RemoveCards()
    {
        Destroy(transform.GetChild(0).gameObject);
            
    }
}
