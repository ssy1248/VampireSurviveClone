using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimaotr : MonoBehaviour
{
    Animator animator;
    PlayerMovement pm;
    SpriteRenderer sr;

    void Start()
    {
        animator = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(pm.moveDir.x != 0 || pm.moveDir.y != 0)
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
        if(pm.lastHorizontalVector < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
