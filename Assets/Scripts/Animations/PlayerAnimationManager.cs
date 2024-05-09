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
        myCard = myField.transform.GetChild(0).gameObject;
        myCard.transform.SetParent(cardAnimationParent.transform, false); // Set parent to object that has animation attached to it.
        myCard.transform.position = myField.transform.position;
        animHandler.PlayMoveToDiscardPile();
    }

    public override void AnimationEndDrawCard()
    {
        cardController.SetIsAnimationRunning(false);
    }
}