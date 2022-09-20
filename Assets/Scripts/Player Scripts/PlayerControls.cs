using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{

    CharacterController controller;
    Animator anim;
    Vector3 move;
    Vector3 flipSprite;
    public VariableJoystick joystick;

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

    bool isOnKeyboardOrGamepad = false;
    bool isOnScreenControls = false;

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

            isFalling = false;
            //verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetAxisRaw("Jump") > 0)
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

        if (isOnKeyboardOrGamepad)
        {
            if (moveX > 0 && moveZ > 0)
            {
                moveX *= 0.5f;
                moveZ *= 0.5f;
            }
            if (moveX < 0 && moveZ < 0)
            {
                moveX *= 0.5f;
                moveZ *= 0.5f;
            }
            if (moveX > 0 && moveZ < 0)
            {
                moveX *= 0.5f;
                moveZ *= 0.5f;
            }
            if (moveX < 0 && moveZ > 0)
            {
                moveX *= 0.5f;
                moveZ *= 0.5f;
            }
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

        if (isMoving)
        {
            Debug.Log("MoveX = " + moveX + ", MoveZ= " + moveZ);

        }
    }
}