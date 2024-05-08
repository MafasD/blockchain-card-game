using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public AnimationManager animationManager;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayDrawCard()
    {
        animator.SetTrigger("DrawCard");

    }
    
    public void AnimationEventTriggerDrawCard() // Called from the EnemyDrawCard animation event.
    {
        animationManager.AnimationEndDrawCard();
    }

    public void PlayMoveToDiscardPile()
    {
        animator.SetTrigger("ToDiscardPile");
    }

    public void AnimationEventTriggerToDiscardPile() // Call from the EnemyCardsToDiscardPile animation event.
    {
        animationManager.AnimationEndToDiscardPile();
    }

}
