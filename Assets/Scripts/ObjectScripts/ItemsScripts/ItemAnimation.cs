using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    //Animation
    Vector3 itemAnimation;
    [Header("Animation")]

    public float time;
    public float amplify;
    public float frequency;

    [Header("3D Object")]
    public bool is3DObject;
    //Follow Camera Angle
    Camera followCameraAngle;

    void Start()
    {
        if (!is3DObject)
        {
            followCameraAngle = Camera.main;
            amplify = 0.1f;
            frequency = 1.5f;
        }
}

    void Update()
    {
        //Animation
        time = Time.time;
        transform.localPosition = new Vector3(0f, (Mathf.Sin(time * frequency) * amplify),0f);
    }
    //Face the Sprite to the Camera
    void LateUpdate()
    {
        if (!is3DObject)
        {
            transform.LookAt(followCameraAngle.transform);
            transform.rotation = Quaternion.Euler(20f, transform.rotation.eulerAngles.y - 180f, 0f);
        }
    }
}
