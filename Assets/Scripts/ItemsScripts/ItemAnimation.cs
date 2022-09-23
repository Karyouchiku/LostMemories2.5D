using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    Vector3 itemAnimation;
    float speed = 0.05f;
    bool turn;
    void Update()
    {
        itemAnimation = Vector3.zero;
        itemAnimation.y = 1f;

        if (transform.localPosition.y >= 0.05f)
        {
            turn = true;
        }
        if (transform.localPosition.y <= 0)
        {
            turn = false;
        }

        if (turn)
        {
            
            transform.localPosition -= itemAnimation * speed * Time.deltaTime;
            
        }
        else
        {
            transform.localPosition += itemAnimation * speed * Time.deltaTime;
        }
        
    }
}
