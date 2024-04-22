using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldHandler : MonoBehaviour
{
    public GameObject MainController;
    GameObject MyCard;

    public void AddCard(GameObject child)
    {
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);

        MyCard = child;

        MyCard.transform.SetParent(transform, false);
        MyCard.transform.position = transform.position;

        if(MainController != null)
            MainController.GetComponent<MainController>().NextTurn();
    }

    public GameObject GetCard()
    {
        return MyCard;
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
