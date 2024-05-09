using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : AnimationManager
{
    public override GameObject SetMyField()
    {
        return GameObject.FindWithTag("EnemyField");
    }

    public override void SendCardToMyField(GameObject myCard)
    {
        base.myCard = myCard;
        cardAnimationParent.transform.position = base.myCard.transform.position;
        base.myCard.transform.SetParent(cardAnimationParent.transform, false);
        base.myCard.transform.position = cardAnimationParent.transform.position;
        animHandler.PlayDrawCard();
    }

    public override void AnimationEndDrawCard()
    {
        cardController.SetIsAnimationRunning(false);
        myField.GetComponent<FieldHandler>().AddCard(myCard); // Adds card manually to the field
    }

    public override void SendCardToDiscardPile()
    {
        myCard.transform.SetParent(cardAnimationParent.transform, false); // Set parent to object that has animation attached to it.
        animHandler.PlayMoveToDiscardPile();
    }

}
