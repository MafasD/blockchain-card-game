using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class DeckContentHandler : MonoBehaviour
{
    public DeckBuilderController controller;
    [SerializeField]GameObject[] childs;

    private void Awake()
    {
        childs = new GameObject[transform.childCount];

        for(int i = 0; i < childs.Length; i++)
        {
            childs[i] = transform.GetChild(i).gameObject;
        
        }


    }

    // Spawns a new card to the player's deck
    public void AddCard(GameObject card, int elementID)
    {
        GameObject newCard = Instantiate(card);
        newCard.transform.SetParent(transform.GetChild(elementID));
        newCard.GetComponent<SimpleDragDrop>().SetIsDeckCard(true);
        newCard.transform.localScale = card.transform.localScale; // Keeps card size the same
        int count = childs[elementID].transform.childCount;
        controller.UpdateCardCount(elementID, count);

    }

    // Get count of all the element childs
    public int GetChildCount()
    {
        int count = 0;

        for(int i = 0; i < childs.Length; i++)
        {
            count += childs[i].transform.childCount;
        }

        return count;
    }

    // Deletes the card game object completely
    public void DeleteCard(int elementID)
    {
        int count = childs[elementID].transform.childCount;
        controller.UpdateCardCount(elementID, count);
    }
}
