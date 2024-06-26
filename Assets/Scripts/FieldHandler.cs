using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldHandler : MonoBehaviour
{
    public MainController mainController;
    public Transform myDiscardPile;
    GameObject myCard; // Card that is dropped on the field (as a child)

    public void AddCard(GameObject child) // Called everytime a new card is added (DragDropV2.cs)
    {
        // Sets object as a child
        myCard = child;
        myCard.transform.SetParent(transform, false);
        myCard.transform.position = transform.position;

        mainController.NextTurn(); // Send info to the MainController to continue process
    }

    public Card GetCard() // Creates a Card object (class)
    {
        int elementID = myCard.GetComponent<InitCardPrefab>().GetElementID();
        Card card = new Card(elementID);
        return card;
    }

    public void RevealCards() // Reveals all the cards that are child objects (current build supports only 1 child)
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).GetComponent<InitCardPrefab>().SetCardVisibleToOthers();
        }
    }
    /*
        public void RemoveCards() // This method deletes all the cards -> for future updates move cards to the discard pile
        {

            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject); // Destroys game objects completely (current build supports only 1 child)
            }

        }
    */

    public void RemoveCards() //Moves cards to the discard pile
    {
        //Loops through all child objects (cards) in the field
        foreach (Transform child in transform)
        {
            //Moves the card to the discard pile
            child.SetParent(myDiscardPile);
            //Reset card's local position and rotation
            child.localPosition = Vector3.zero; //Sets card position to (0,0,0) inside discard pile
            child.localRotation = Quaternion.identity; //Resets card rotation, making enemycards appear upside down, inside discard pile
        }
    }
}
