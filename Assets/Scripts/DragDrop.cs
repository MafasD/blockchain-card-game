using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public GameObject Field; //Reference to the game field object, where we place cards.

    private bool isDragging = false; //This will track whether a card is being dragged.
    private GameObject startParent; //The card's starting parent object.
    private Vector2 startPosition; //Starting position of the card.
    private GameObject currentField; //This will keep track of the current field under the card (playerarea/opponentarea).
    private bool isPlaced = false; //This will track whether a card has been placed on the field.

    void Start()
    {
        Field = GameObject.Find("Field"); //Finds the Field object in the gameplay scene.
    }

    //This function is called when a collision happens with another collider, which we use to track when a card is being dragged over the field.
    private void OnCollisionEnter2D(Collision2D collision)  
    {
        currentField = collision.gameObject;
    }

    //Called when a collision with another collider ends, which we use to determine when a card should be returned to original position if the dragging ends.
    private void OnCollisionExit2D(Collision2D collision)
    {
        currentField = null;
    }

    //Called when dragging of the card starts.
    public void StartDrag()
    {
        if (!isPlaced) //Only allows dragging if the card hasn't been placed, so once it's on the field we can't move it anymore.
        {
            isDragging = true;
            startParent = transform.parent.gameObject;
            startPosition = transform.position;
        }
    }

    //Called when dragging of the card stops.
    public void StopDrag()
    {
        if (isDragging && !isPlaced) 
        {
            isDragging = false;
            if (currentField != null) //Checking if card is over a field currently.
            {
                Transform playerField = Field.transform.Find("PlayerField");
                Transform opponentField = Field.transform.Find("OpponentField");

                //This part is so that the card will be placed on the PlayerField, even if dragging ends on the OpponentField.
                if (currentField == playerField.gameObject)
                {
                    transform.SetParent(playerField, false);
                }
                else if (currentField == opponentField.gameObject)
                {
                    transform.SetParent(playerField, false);
                }
                isPlaced = true; //Marks the card as placed.
            }
            else //If card is not over a field on StopDrag, it will be returned to its starting position.
            {
                transform.position = startPosition;
                transform.SetParent(startParent.transform, false); 
            }
        }
    }

    void Update()
    {
        //This updates the position of the card to follow the mouse position.
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }
}