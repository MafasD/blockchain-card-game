using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public AnimationManager animationManager; // Manager who takes care of animation events.
    Animator animator; // Animator in this game object.

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayDrawCard() // Plays the "DrawCard" animation of the chosen Animator (controller).
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
        animationManager.AnimationTriggerToDiscardPile(); // Mid section event trigger when animation is still running.
    }

    public void AnimationEndEventTriggerToDiscardPile() // Final event trigger when animation is fully played.
    {
        animationManager.AnimationEndToDiscardPile();
    }

}
