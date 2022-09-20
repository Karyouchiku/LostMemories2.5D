using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    PlayerAnimations playerAnimations;
    CharacterController controller;
    Vector3 move;
    SpriteRenderer flipSprite;
    public VariableJoystick joystick;

    public float gravity;
    public float jumpForce;
    float verticalVelocity;
    float moveX;
    float moveZ;
    public float movementSpeed;

    //float jumpTimer = 0.2f;

    bool isOnKeyboardOrGamepad = false;
    bool isOnScreenControls = false;

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
                moveX = joystick.Horizontal;
                moveZ = joystick.Vertical;
            }
            if (verticalVelocity < -0.5f)
            {
            }
            
            verticalVelocity = -gravity * Time.deltaTime;
            playerAnimations.isFalling = false;
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

        if (isOnKeyboardOrGamepad)
        {
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
        }

        move = Vector3.zero;
        move.x = moveX * movementSpeed;
        move.y = verticalVelocity;
        move.z = moveZ * movementSpeed;
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

        Debug.Log(verticalVelocity);
        //Debugging
        //if (isMoving)
        //{
        //    Debug.Log("MoveX = " + moveX + ", MoveZ= " + moveZ);
        //}
    }
}