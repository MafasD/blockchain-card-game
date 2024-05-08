using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract class that is inherited by PlayerController and EnemyController

public abstract class CardController : MonoBehaviour
{
    public AnimationManager animationManager; // Handles Unity animations.

    public GameObject MyField; // My field parent object where cards are dropped; My hand cards parent object

    protected bool showAnimations = true; // Flag for showing card animations.

    protected bool isAnimationRunning = false; // Flag for informing if the animation is running. (Works only if showAnimations is active)

    public abstract void MyTurn(); // Called when turn starts

    public abstract void EndOfMyTurn(); // Called when turn starts (player)

    public abstract void AddCardsToDiscardPile(); // Remove card from the field -> move it to the discard pile

    public void RevealCardFromField() // Reveals card from the field to the all players
    {
        MyField.GetComponent<FieldHandler>().RevealCards();
    }

    public Card GetMyCard() // Get card information as a Card object (class)
    {
        return MyField.GetComponent<FieldHandler>().GetCard();
    }

    public void SetIsAnimationRunning(bool isAnimationRunning) // Setter for animation is running flag -> Called from the AnimationManager.
    {
        this.isAnimationRunning = isAnimationRunning;
    }

}
