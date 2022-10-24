using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator spriteAnimation;

    public float moveX;
    public float moveZ;

    public void resetAnimation()
    {
        moveX = 0f;
        moveZ = 0f;
    }
    void Update()
    {
        spriteAnimation.SetFloat("MoveX", moveX);
        spriteAnimation.SetFloat("MoveZ", moveZ);
    }
}
