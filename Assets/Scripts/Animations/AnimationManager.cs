using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationManager : MonoBehaviour
{
    public GameObject cardAnimationParent;
    protected AnimationHandler animHandler;
    protected CardController cardController;
    protected GameObject myField, myCard;

    private void Awake()
    {
        animHandler = cardAnimationParent.GetComponent<AnimationHandler>();
        cardController = GetComponent<CardController>();
        myField = SetMyField();
    }

    public abstract GameObject SetMyField(); // Determines what field object is used.

    public abstract void SendCardToDiscardPile(); // Functionality for sending card to discard pile.

    public abstract void AnimationEndDrawCard(); // Called when the animation ends event is called.

    public virtual void SendCardToMyField(GameObject myCard) { } // Functionality for sending card to the field.

    public void AnimationTriggerToDiscardPile() // Event trigger called in the middle of "ToDiscardPile" animation event.
    {
        myCard.transform.SetParent(myField.transform, false); // Set card back to the field when animation ends.
        myField.GetComponent<FieldHandler>().RemoveCards();
    }

    public void AnimationEndToDiscardPile() // Event trigger called when "ToDiscardPile" animation ends.
    {
        cardController.SetIsAnimationRunning(false);
    }

}