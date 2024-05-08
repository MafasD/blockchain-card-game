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
        MyCard = myCard;
        CardAnimationParent.transform.position = MyCard.transform.position;
        MyCard.transform.SetParent(CardAnimationParent.transform, false);
        MyCard.transform.position = CardAnimationParent.transform.position;
        animHandler.PlayDrawCard();
    }

    public override void AnimationEndDrawCard()
    {
        cardController.SetIsAnimationRunning(false);
        MyField.GetComponent<FieldHandler>().AddCard(MyCard); // Adds card manually to the field
    }

    public override void SendCardToDiscardPile()
    {
        MyCard.transform.SetParent(CardAnimationParent.transform, false); // Set parent to object that has animation attached to it.
        animHandler.PlayMoveToDiscardPile();
    }

}
