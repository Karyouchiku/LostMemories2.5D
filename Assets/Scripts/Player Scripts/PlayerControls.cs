using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    CharacterController controller;
    Animator anim;
    Vector3 move;

    public float gravity;
    public float jumpForce;
    float verticalVelocity;
    float moveX;
    float moveZ;
    public float movementSpeed;
    float animMoveX;
    float animMoveZ;
    bool isMoving = false;
    bool isJumping = false;
    bool isFalling = false;
    float jumpTimer = 0.1f;

    Vector3 flipSprite;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        flipSprite = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {


        if (controller.isGrounded)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");
            isFalling = false;
            //verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetAxisRaw("Fire1") > 0)
            {
                verticalVelocity = jumpForce;
                isJumping = true;

            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            isFalling = true;
        }
        if (isJumping)
        {
            jumpTimer -= 2 * Time.deltaTime;
            if (jumpTimer <= 0)
            {
                jumpTimer = 0.3f;
                isJumping = false;
            }
        }

        if (moveX > 0 && moveZ > 0)
        {
            moveX = 0.75f;
            moveZ = 0.75f;
        }
        if (moveX < 0 && moveZ < 0)
        {
            moveX = -0.75f;
            moveZ = -0.75f;
        }
        if (moveX > 0 && moveZ < 0)
        {
            moveX = 0.75f;
            moveZ = -0.75f;
        }
        if (moveX < 0 && moveZ > 0)
        {
            moveX = -0.75f;
            moveZ = 0.75f;
        }


        move = Vector3.zero;
        move.x = moveX * movementSpeed;
        move.y = verticalVelocity;
        move.z = moveZ * movementSpeed;
        controller.Move(move * Time.deltaTime);

        if (moveX != 0 || moveZ != 0)
        {
            isMoving = true;
            if (moveX > 0)
            {
                flipSprite.x = 1;
            }
            if (moveX < 0)
            {
                flipSprite.x = -1;
            }
            transform.localScale = flipSprite;
        }
        else
        {
            isMoving = false;
        }

        //anim.SetFloat("Horizontal",moveX);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isFalling", isFalling);
    }
}