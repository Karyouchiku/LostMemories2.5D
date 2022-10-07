using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour, ISaveable
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

    public object SaveState()
    {
        return new SaveData()
        {
            positionX = transform.position.x,
            positionY = transform.position.y,
            positionZ = transform.position.z
        };
    }

    public void LoadState(object state)
    {
        Vector3 position;
        var saveData = (SaveData)state;
        position.x = saveData.positionX;
        position.y = saveData.positionY;
        position.z = saveData.positionZ;
        transform.position = position;
    }

    [Serializable]
    struct SaveData
    {
        public float positionX;
        public float positionY;
        public float positionZ;
    }
}
