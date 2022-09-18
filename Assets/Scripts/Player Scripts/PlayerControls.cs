using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    CharacterController controller;
    Rigidbody rb;
    public float gravity = 5f;
    public float jumpforce = 10f;
    float verticalVelocity;
    public float movementSpeed;
    float moveX;
    float moveZ;
    Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        //movementSpeed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Move Horizontal") * movementSpeed;
        moveZ = Input.GetAxis("Move Vertical") * movementSpeed;

        //moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpforce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        Vector3 move = Vector3.zero;
        move.x = moveX;
        move.y = verticalVelocity;
        move.z = moveZ;
        controller.Move(move * Time.deltaTime);
    }

    void FixedUpdate() {
        //rb.velocity = new Vector3(moveDirection.x * movementSpeed * Time.deltaTime, 0f, moveDirection.z * movementSpeed * Time.deltaTime);
        
    }
}