using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [Header("Camera Settings")]
    public float followSpeed;
    public Transform followTarget;
    public float lockCameraZ;

    private void Start()
    {
        lockCameraZ = followTarget.position.z - 2f;
    }
    void Update()
    {

        Vector3 newPosition = new Vector3(followTarget.position.x, followTarget.position.y + 1f, lockCameraZ);
        transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed * Time.deltaTime);
        
    }
}
