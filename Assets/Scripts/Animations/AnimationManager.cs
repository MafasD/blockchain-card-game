using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationManager : MonoBehaviour
{
    public GameObject CardAnimationParent;
    protected AnimationHandler animHandler;
    protected CardController cardController;
    protected GameObject MyField, MyCard;

    private void Awake()
    {
        animHandler = CardAnimationParent.GetComponent<AnimationHandler>();
        cardController = GetComponent<CardController>();
        MyField = SetMyField();
    }

    public abstract GameObject SetMyField();

    public abstract void SendCardToDiscardPile();

    public abstract void AnimationEndDrawCard();

    public virtual void SendCardToMyField(GameObject myCard) { }

    public void AnimationTriggerToDiscardPile()
    {
        MyCard.transform.SetParent(MyField.transform, false); // Set card back to the field when animation ends.
        MyField.GetComponent<FieldHandler>().RemoveCards();
    }

    public void AnimationEndToDiscardPile()
    {
        cardController.SetIsAnimationRunning(false);
    }

}