using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    //Animation
    Vector3 itemAnimation;
    [Header("Animation")]
    public float speed;
    public float increaseSpeed = 0.1f;
    public float decreaseSpeed = 0.2f;
    public float maxHeight = 0.05f;
    public float minHeight = 0f;

    bool turn;

    //Follow Camera Angle
    Camera followCameraAngle;

    void Start()
    {
        followCameraAngle = Camera.main;
    }

    void Update()
    {
        //Animation
        itemAnimation = Vector3.zero;
        itemAnimation.y = 1f;

        if (transform.localPosition.y >= maxHeight)
        {
            speed = 0f;
            turn = true;
        }
        if (transform.localPosition.y <= minHeight)
        {
            speed = 0f;
            turn = false;
        }

        if (turn)
        {
            
            if (transform.localPosition.y >= minHeight && transform.localPosition.y <= (maxHeight * 0.3f))
            {
                speed -= decreaseSpeed * Time.deltaTime;
            }
            else
            {
                speed += increaseSpeed * Time.deltaTime;
            }

            transform.localPosition -= itemAnimation * speed * Time.deltaTime;
            
        }
        else
        {
            if (transform.localPosition.y >= (maxHeight * 0.7f) && transform.localPosition.y <= maxHeight)
            {
                speed -= decreaseSpeed * Time.deltaTime;
            }
            else
            {
                speed += increaseSpeed * Time.deltaTime;
            }

            transform.localPosition += itemAnimation * speed * Time.deltaTime;
        }

    }
    //Face the Sprite to the Camera
    void LateUpdate()
    {
        transform.LookAt(followCameraAngle.transform);
        transform.rotation = Quaternion.Euler(20f, transform.rotation.eulerAngles.y - 180f, 0f);
    }
}
