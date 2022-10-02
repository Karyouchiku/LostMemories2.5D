using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [Header("Camera Settings")]
    public float followSpeed;
    public Transform followTarget;

    [Header("Camera Position")]
    public float camX;
    public float camY;
    public float camZ;
    void Update()
    {

        Vector3 newPosition = new Vector3(followTarget.position.x + camX, followTarget.position.y + camY, followTarget.position.z + camZ);
        transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed * Time.deltaTime);
        
    }
}
