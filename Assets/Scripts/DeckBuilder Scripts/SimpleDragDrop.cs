using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Simplified drag and drop script that is attached to the card itself.

namespace DeckBuilder
{
    public class SimpleDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        GameObject deckField, currentField; // PlayerField = parent of the table card objects; CurrentField = if the field is active.
        GameObject parentObj, mainCanvas;
        Vector2 startPosition; // Origin position where the card is spawned/placed
        bool isPlayerDeckCard = false;

        void Awake()
        {
            deckField = GameObject.FindWithTag("PlayerField");
            mainCanvas = GameObject.FindWithTag("MainCanvas");
            startPosition = transform.localPosition; // Sets the start position before runtime
        }

        public void SetIsPlayerDeckCard(bool isPlayerDeckCard)
        {
            this.isPlayerDeckCard = isPlayerDeckCard;
        }

        void OnTriggerEnter2D(Collider2D other) // When both objects BoxCollider2D's trigger collision starts  
        {
            if (other.CompareTag("PlayerField"))
                currentField = other.gameObject;

        }

        void OnTriggerExit2D(Collider2D other) // When both objects BoxCollider2D's trigger collision ends
        {
            if (other.CompareTag("PlayerField"))
                currentField = null;

        }

        public void OnBeginDrag(PointerEventData eventData) // Called when the mouse key is pressed.
        {
            startPosition = transform.localPosition; // Sets the last position before drag.
            parentObj = transform.parent.gameObject; // Sets the last parent object before drag.
            transform.SetParent(mainCanvas.transform); // Set current parent to the main canvas.

        }

        public void OnDrag(PointerEventData eventData) // Called while the mouse key is pressed. (Object is dragged)
        {
            transform.position = eventData.position; // Changes position based on coursor location.

        }

        public void OnEndDrag(PointerEventData eventData) // Called when the mouse key press is released.
        {
            if (!isPlayerDeckCard && currentField == deckField) // Add a new card to the deck.
                deckField.GetComponent<DeckContentHandler>().AddCard(gameObject, GetComponent<InitCardPrefab>().GetElementID()); // Spawn new duplicate card to the player's deck.

            else if (isPlayerDeckCard && transform.localPosition.x > 300) // When the card's position is higher than 300 -> delete card.
            {
                deckField.GetComponent<DeckContentHandler>().DeleteCard(gameObject); // Gives command to delete this object from the scene.
                return;
            }

            // Returns object to the origin position with the same origin parent.
            transform.SetParent(parentObj.transform);
            transform.localPosition = startPosition;
        }

    }
}