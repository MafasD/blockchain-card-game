using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public AnimationManager animationManager;
    Animator animator;

    private void Awake()
    {
        Debug.Log("Getting animator");
        animator = GetComponent<Animator>();
    }

    public void PlayDrawCard()
    {
        animator.SetTrigger("DrawCard");

    }
    
    public void AnimationEndsDrawCard() // Called from the EnemyDrawCard animation event.
    {
        animationManager.AnimationEndDrawCard();
    }

    public void PlayMoveToDiscardPile()
    {
        animator.SetTrigger("ToDiscardPile");
    }

    public void AnimationEndsToDiscardPile() // Call from the EnemyCardsToDiscardPile animation event.
    {
        animationManager.AnimationEndToDiscardPile();
    }
}
