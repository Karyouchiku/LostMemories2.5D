using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    //private Componets
    PlayerAnimations playerAnimations;
    CharacterController controller;
    Vector3 move;
    SpriteRenderer flipSprite;

    [Header("Movement Settings")]
    public float gravity;

    //Remove this sht later
    public float jumpForce;
    public float movementSpeed;
    
    float verticalVelocity;

    [Header("MomementValue - readonly")]
    public float moveX;
    public float moveZ;

    //float jumpTimer = 0.2f;


    [Header("Joystick Settings")]
    public VariableJoystick joystick;
    public float JoystickLimiter;
    bool isOnScreenControls = false;
    bool isOnKeyboardOrGamepad = false;

    [Header("Controller Manager")]
    public bool isControlEnable = true;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnimations = GetComponent<PlayerAnimations>();
        flipSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlEnable)
        {
            MovementScript();
        }

    }

    private void MovementScript()
    {

        if (controller.isGrounded)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                isOnKeyboardOrGamepad = true;
                isOnScreenControls = false;
            }
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                isOnScreenControls = true;
                isOnKeyboardOrGamepad = false;
            }

            //keyboard or gamepad controls
            if (isOnKeyboardOrGamepad)
            {
                moveX = Input.GetAxisRaw("Horizontal");
                moveZ = Input.GetAxisRaw("Vertical");
            }

            //UI Controls
            if (isOnScreenControls)
            {
                if (joystick.Horizontal >= -JoystickLimiter && joystick.Horizontal <= JoystickLimiter)
                {
                    moveX = 0f;
                }
                else
                {
                    moveX = joystick.Horizontal;
                }
                if (joystick.Vertical >= -JoystickLimiter && joystick.Vertical <= JoystickLimiter)
                {
                    moveZ = 0f;
                }
                else
                {
                    moveZ = joystick.Vertical;
                }
            }


            verticalVelocity = -gravity * Time.deltaTime;
            playerAnimations.isFalling = false;

            //JUMP Remove later this is just for testing shts
            if (Input.GetAxisRaw("Jump") > 0)
            {
                verticalVelocity = jumpForce;
                playerAnimations.isJumping = true;

            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            if (verticalVelocity <= -1.5f)
            {
                playerAnimations.isFalling = true;
            }
        }

        if (playerAnimations.isJumping)
        {
            //jumpTimer -= 2 * Time.deltaTime;
            if (verticalVelocity <= 0f)
            {
                //jumpTimer = 0.4f;
                playerAnimations.isJumping = false;
            }
        }

        //Movement Result 
        move = Vector3.zero;
        move.x = moveX * movementSpeed;
        move.y = verticalVelocity;
        move.z = moveZ * movementSpeed;
        Vector3.Normalize(move);
        controller.Move(move * Time.deltaTime);

        if (moveX != 0 || moveZ != 0)
        {
            playerAnimations.isMoving = true;
            if (moveX > 0)
            {
                flipSprite.flipX = false;
            }
            if (moveX < 0)
            {
                flipSprite.flipX = true;
            }
            //transform.localScale = flipSprite;
        }
        else
        {
            playerAnimations.isMoving = false;
        }
    }
}