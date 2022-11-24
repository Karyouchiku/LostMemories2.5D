using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightControls : MonoBehaviour
{
    public VariableJoystick joystick;
    public GameObject flashlight;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    Transform cam;
    void Start()
    {
        cam = Camera.main.transform;
    }
    public void FLSwitch(bool turn)
    {
        flashlight.SetActive(turn);
    }
    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(flashlight.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            flashlight.transform.rotation = Quaternion.Euler(18f, angle, 0f);
        }
    }
}
