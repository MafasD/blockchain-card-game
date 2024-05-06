using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldHandler : MonoBehaviour
{
    public MainController MainController;
    GameObject MyCard; // Card that is dropped on the field (as a child)

    public void AddCard(GameObject child) // Called everytime a new card is added (DragDropV2.cs)
    {
        // Sets object as a child
        MyCard = child;
        MyCard.transform.SetParent(transform, false);
        MyCard.transform.position = transform.position;

        MainController.NextTurn(); // Send info to the MainController to continue process
    }

    public Card GetCard() // Creates a Card object (class)
    {
        int elementID = MyCard.GetComponent<InitCardPrefab>().GetElementID();
        int damage = 1; // Just for the looks
        Card card = new Card(elementID,damage);
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

    public void RemoveCards(Transform discardPile) //Moves cards to the discard pile
    {
        //Loops through all child objects (cards) in the field
        foreach (Transform child in transform)
        {
            //Moves the card to the discard pile
            child.SetParent(discardPile);
            //Reset card's local position and rotation
            child.localPosition = Vector3.zero; //Sets card position to (0,0,0) inside discard pile
            child.localRotation = Quaternion.identity; //Resets card rotation, making enemycards appear upside down, inside discard pile
        }
    }
}
