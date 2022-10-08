using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator spriteAnimation;

    public bool isMoving;
    public bool isJumping;
    public bool isFalling;
    
    public void resetAnimation()
    {
        isMoving = false;
        isJumping = false;
        isFalling = false;
    }
    void Update()
    {
        spriteAnimation.SetBool("isMoving", isMoving);
        spriteAnimation.SetBool("isJumping", isJumping);
        spriteAnimation.SetBool("isFalling", isFalling);
    }
}
