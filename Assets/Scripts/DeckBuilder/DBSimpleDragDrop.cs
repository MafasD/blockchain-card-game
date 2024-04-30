using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DeckBuilder
{
    public class SimpleDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        GameObject DeckField, CurrentField; // PlayerField = parent of the table card objects; CurrentField = if the field is active.
        GameObject ParentObj, MainCanvas;
        Vector2 startPosition; // Origin position where the card is spawned/placed
        bool isDeckCard = false;

        void Awake()
        {
            DeckField = GameObject.FindWithTag("PlayerField");
            MainCanvas = GameObject.FindWithTag("MainCanvas");
            startPosition = transform.localPosition; // Sets the start position before runtime
        }

        public void SetIsDeckCard(bool isDeckCard)
        {
            this.isDeckCard = isDeckCard;
        }

        void OnTriggerEnter2D(Collider2D other) // When both objects BoxCollider2D's trigger collision starts  
        {
            if (other.CompareTag("PlayerField"))
                CurrentField = other.gameObject;

        }

        void OnTriggerExit2D(Collider2D other) // When both objects BoxCollider2D's trigger collision ends
        {
            if (other.CompareTag("PlayerField"))
                CurrentField = null;

            else if (other.CompareTag("TrashField"))
                CurrentField = null;

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            startPosition = transform.localPosition;
            ParentObj = transform.parent.gameObject;
            transform.SetParent(MainCanvas.transform);

        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!isDeckCard && CurrentField == DeckField) // Add a new card to the deck
                DeckField.GetComponent<DeckContentHandler>().AddCard(gameObject, GetComponent<InitCardPrefab>().GetElementID());

            else if (isDeckCard && transform.localPosition.x > 300) // Delete card based on horizontal position
            {
                DeckField.GetComponent<DeckContentHandler>().DeleteCard(GetComponent<InitCardPrefab>().GetElementID());
                Destroy(gameObject);
                return;
            }

            transform.SetParent(ParentObj.transform);
            transform.localPosition = startPosition;
        }

    }
}