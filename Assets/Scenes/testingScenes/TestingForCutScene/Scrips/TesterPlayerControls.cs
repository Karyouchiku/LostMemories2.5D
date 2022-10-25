using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterPlayerControls : MonoBehaviour
{
    //private Componets
    CharacterController controller;
    Vector3 move;
    public Transform cam;

    [Header("Movement Settings")]
    public float movementSpeed;

    [Header("MomementValue - readonly")]
    public float moveX;
    public float moveZ;
    public float targetAngle;

    [Header("Controller Manager")]
    public bool isControlEnable = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
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
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");

        }
        //Movement Result 
        move = new Vector3(moveX, 0f, moveZ).normalized;
        move.x *= movementSpeed * Time.deltaTime;
        move.z *= movementSpeed * Time.deltaTime;
        controller.Move(cam.rotation * ((Vector3.forward * move.z) + (Vector3.right * move.x)) + (Vector3.down * 5f * Time.deltaTime));
    }
}
