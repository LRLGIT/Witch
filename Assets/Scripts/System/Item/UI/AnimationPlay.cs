using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlay : MonoBehaviour
{
    public bool playOnce;
    private bool canPlay = true;
    private Animator animator;
    public string animationName = "";

    public bool canPlayAgain;
    public bool cannotPlayAgain;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        if (canPlayAgain && StaticData.stringInfo.Contains(animationName))
        {
            animator.Play("fire-3");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canPlay)
        {
            if(cannotPlayAgain && StaticData.stringInfo.Contains(animationName))return;
            
            PlayAnimation();
            if (playOnce)
            {
                canPlay = false;
            }
        }
    }

    void PlayAnimation()
    {
        if (animator != null && !string.IsNullOrEmpty(animationName))
        {
            animator.Play(animationName, 0, 0);
            if (canPlayAgain||cannotPlayAgain)
            {
                StaticData.stringInfo.Add(animationName);
            }
        }
        else
        {
            Debug.LogWarning("Animator or animation name is not set.");
        }
    }
}
