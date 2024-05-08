using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : AnimationManager
{
    public override GameObject SetMyField()
    {
        return GameObject.FindWithTag("PlayerField");
    }

    public override void SendCardToDiscardPile()
    {
        MyCard = MyField.transform.GetChild(0).gameObject;
        MyCard.transform.SetParent(CardAnimationParent.transform, false); // Set parent to object that has animation attached to it.
        MyCard.transform.position = MyField.transform.position;
        animHandler.PlayMoveToDiscardPile();
    }

    public override void AnimationEndDrawCard()
    {
        cardController.SetIsAnimationRunning(false);
    }
}