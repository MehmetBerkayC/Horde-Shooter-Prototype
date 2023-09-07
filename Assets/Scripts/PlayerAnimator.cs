using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    PlayerMovement playerMovement;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if(playerMovement.playerInputs.x != 0 || playerMovement.playerInputs.y != 0)
        {
            animator.SetBool("Move", true);

            SpriteDirectionChecker();
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }

    void SpriteDirectionChecker()
    {
        if(playerMovement.lastHorizontalVector < 0)
        {
            spriteRenderer.flipX = true;
        }   
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}