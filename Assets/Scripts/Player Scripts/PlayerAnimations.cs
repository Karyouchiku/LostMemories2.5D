using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator anim;

    public bool isMoving;
    public bool isJumping;
    public bool isFalling;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isFalling", isFalling);
    }
}
