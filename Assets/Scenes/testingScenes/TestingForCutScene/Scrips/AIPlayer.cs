using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class AIPlayer : MonoBehaviour
{
    public DialogueDatabase db;
    
    Vector3 target;
    public GameObject goLeft;
    public GameObject goRight;
    public float mspeed;
    public bool movePlayerAI;

    public GameObject otherActor;
    public bool startFollow;
    public bool moveOtherAI;
    
    void Update()
    {
        if (movePlayerAI)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, mspeed * Time.deltaTime);
        }
        if (moveOtherAI)
        {
            otherActor.transform.position = Vector3.MoveTowards(otherActor.transform.position, transform.position, (mspeed - 1f)* Time.deltaTime);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CutScenePosLoc")
        {
            movePlayerAI = false;
        }
        if (other.tag == "InteractableNPC")
        {
            moveOtherAI = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (startFollow)
        {
            if (other.tag == "InteractableNPC")
            {
                moveOtherAI = true;
            }

        }
    }
    public void RunLeft()
    {
        movePlayerAI = true;
        target = goLeft.transform.position;
        target.y = transform.position.y;
    }
    public void RunRight()
    {
        movePlayerAI = true;
        target = goRight.transform.position;
        target.y = transform.position.y;
    }
    public void OtherActorFollows()
    {
        moveOtherAI = true;
        startFollow = true;
    }
}
