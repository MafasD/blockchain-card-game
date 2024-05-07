using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragDropV2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] GameObject Frontside, Backside; // Frontside shows all the card info; Backside all the info.
    GameObject PlayerController;
    GameObject PlayerField, CurrentField; // PlayerField = parent of the table card objects; CurrentField = if the field is active.
    Vector2 startPosition; // Origin position where the card is spawned/placed.
    bool isDragging = false; // Flags if the player is moving the card.
    bool isPlaced = false; //This will track whether a card has been placed on the field.

    DeckController deckController; //Reference to the DeckController

    void Awake()
    {
        PlayerField = GameObject.FindWithTag("PlayerField");
        PlayerController = GameObject.FindWithTag("PlayerController");
        startPosition = transform.localPosition; // Sets the start position before runtime
        Frontside.SetActive(true); // Show front side of the card (for the player)
        Backside.SetActive(false); // Hide the backside of the card

        deckController = FindObjectOfType<DeckController>(); //Finds the DeckController
    }

    void OnTriggerEnter2D(Collider2D other) // When both objects BoxCollider2D's trigger collision starts  
    {
        if (isPlaced) // Do nothing if the card is already placed on the field
            return;

        if (other.CompareTag("PlayerField"))
            CurrentField = other.gameObject;

    }

    void OnTriggerExit2D(Collider2D other) // When both objects BoxCollider2D's trigger collision ends
    {
        if (other.CompareTag("PlayerField") && CurrentField != null)
        {
            CurrentField = null;
        }
    }

    //Called when dragging of the card starts.
    public void OnBeginDrag(PointerEventData eventData)
    {
        bool isPlayersTurn = PlayerController.GetComponent<PlayerController>().CheckIfPlayersTurn();
        if (isPlaced || !isPlayersTurn) // Only allows dragging if the card hasn't been placed, so once it's on the field we can't move it anymore.
            return;

        startPosition = transform.localPosition;
        isDragging = true;
    }

    //Continuous call when dragging of the card is active.
    public void OnDrag(PointerEventData eventData)
    {
        if (isPlaced || !isDragging)
            return;

        transform.position = eventData.position; // Update the current location of the card.

    }
    //Called when dragging of the card stops.
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isPlaced)
            return;

        if (CurrentField == PlayerField)
        {
            PlayerField.GetComponent<FieldHandler>().AddCard(gameObject); // Manually adds a card to the PlayerField
            isPlaced = true; // Card is now placed on the PlayerField.
            Frontside.SetActive(false); // Hide card information
            Backside.SetActive(true);

            //Sets the card's scale to 1.2x, because PlayerHand seems to mess with the scaling somehow.
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            //Delays the method to ensure that the card is removed from hand when this is run, because it was running too early and the draw card button was not working as it should.
            Invoke("UpdateDrawButtonState", 0.1f);
        }
        else if (CurrentField == null)
            transform.localPosition = startPosition; // Return card to the starting position

        isDragging = false; // Dragging event ends
    }
    //Calls the method from DeckController
    private void UpdateDrawButtonState()
    {
        deckController.CheckDrawButtonState();
    }
}