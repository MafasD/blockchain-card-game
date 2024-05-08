using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationManager : MonoBehaviour
{
    public GameObject CardAnimationParent;
    protected AnimationHandler animHandler;
    protected GameObject MyField, MyCard;

    private void Awake()
    {
        string fieldName = SetMyField();
        MyField = GameObject.FindWithTag(fieldName);
        animHandler = CardAnimationParent.GetComponent<AnimationHandler>();
    }

    public abstract string SetMyField();

    public abstract void SendCardToDiscardPile();

    public virtual void SendCardToMyField(GameObject myCard) { }

    public virtual void AnimationEndDrawCard() { }


    public void AnimationEndToDiscardPile()
    {
        MyCard.transform.SetParent(MyField.transform, false); // Set card back to the field when animation ends.
        MyField.GetComponent<FieldHandler>().RemoveCards();
    }

}
