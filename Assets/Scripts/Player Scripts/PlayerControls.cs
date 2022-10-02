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
    public SpriteRenderer sprite;
    public Transform cam;

    [Header("Movement Settings")]
    public float movementSpeed;
    
    [Header("MomementValue - readonly")]
    public float moveX;
    public float moveZ;
    public float targetAngle;

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
            playerAnimations.isFalling = false;

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

        }
        else
        {
            playerAnimations.isFalling = true;
        }


        //Movement Result 
        move = new Vector3(moveX, 0f, moveZ).normalized;
        move.x *= movementSpeed * Time.deltaTime;
        move.z *= movementSpeed * Time.deltaTime;
        //controller.Move(move * Time.deltaTime);
        controller.Move(cam.rotation * ((Vector3.forward * move.z) + (Vector3.right * move.x)) + (Vector3.down * 5f * Time.deltaTime));


        if (moveX != 0 || moveZ != 0)
        {
            playerAnimations.isMoving = true;
            if (moveX > 0)
            {
                sprite.flipX = false;
            }
            if (moveX < 0)
            {
                sprite.flipX = true;
            }
            //transform.localScale = flipSprite;
        }
        else
        {
            playerAnimations.isMoving = false;
        }
    }
}