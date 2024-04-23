using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldHandler : MonoBehaviour
{
    public GameObject MainController;
    GameObject MyCard; // Card that is dropped on the field

    private void Awake()
    {
        if(MainController == null)
            MainController = GameObject.FindWithTag("MainController");
    }

    public void AddCard(GameObject child) // Called everytime a card is dropped as a child
    {
        if (transform.childCount > 0) // Checks if card already exists on the field (not necessary)
            Destroy(transform.GetChild(0).gameObject);

        MyCard = child;
        MyCard.transform.SetParent(transform, false);
        MyCard.transform.position = transform.position;

        MainController.GetComponent<MainController>().NextTurn(); // Send info to the main controller to continue process
    }

    public Card GetCard() // Creates a Card object (class)
    {
        int elementID = MyCard.GetComponent<InitCardPrefab>().GetElementID();
        int damage = 1; // Just for the looks
        Card card = new Card(elementID,damage);
        return card;
    }

    public void RevealCards() // Reveals all the cards that are child objects
    {
        // Loop not necessary in current build
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).GetComponent<InitCardPrefab>().SetCardVisibleToOthers();
        }
    }

    public void RemoveCards() // Remove all the cards that are child objects
    {
        // Loop not necessary in current build
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject); ;
        }
            
    }
}
