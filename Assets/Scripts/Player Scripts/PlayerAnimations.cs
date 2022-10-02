using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator spriteAnimation;

    public bool isMoving;
    public bool isJumping;
    public bool isFalling;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        spriteAnimation.SetBool("isMoving", isMoving);
        spriteAnimation.SetBool("isJumping", isJumping);
        spriteAnimation.SetBool("isFalling", isFalling);
    }
}
