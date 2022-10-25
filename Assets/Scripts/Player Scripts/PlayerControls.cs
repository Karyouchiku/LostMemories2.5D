using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour, ISaveable
{
    //private Componets
    PlayerAnimations playerAnimations;
    CharacterController controller;
    Vector3 move;
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

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnimations = GetComponent<PlayerAnimations>();
    }

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

        }


        //Movement Result 

        playerAnimations.moveX = moveX;
        playerAnimations.moveZ = moveZ;
        move = new Vector3(moveX, 0f, moveZ).normalized;
        move.x *= movementSpeed * Time.deltaTime;
        move.z *= movementSpeed * Time.deltaTime;
        controller.Move(cam.rotation * ((Vector3.forward * move.z) + (Vector3.right * move.x)) + (Vector3.down * 5f * Time.deltaTime));
    }

    public object SaveState()
    {
        return new SaveData()
        {
            position = new float[]
            {
                transform.position.x,
                transform.position.y,
                transform.position.z
            }
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        Vector3 position;

        position.x = saveData.position[0];
        position.y = saveData.position[1];
        position.z = saveData.position[2];

        transform.position = position;
    }

    [Serializable]
    struct SaveData
    {
        public float[] position;
    }
}