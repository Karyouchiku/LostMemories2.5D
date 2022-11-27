using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVController : MonoBehaviour
{
    CharacterAnimation dirMovement;
    FieldOfView fov;

    float turnSmoothTime = 0f;
    float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        dirMovement = GetComponent<CharacterAnimation>();
        fov = GetComponentInChildren<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = dirMovement.moveX;
        float vertical = dirMovement.moveZ;

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(fov.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            fov.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
}
