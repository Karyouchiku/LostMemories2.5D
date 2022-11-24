using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingOfPuzzle2 : MonoBehaviour
{
    IPuzzle2 puzzle;
    PortalDoor thisDoor;
    void Start()
    {
        puzzle = GetComponentInParent<IPuzzle2>();
        thisDoor = GetComponent<PortalDoor>();
    }
    bool oneTimeMethod;
    void Update()
    {
        if (!oneTimeMethod)
        {
            if (!thisDoor.locked)
            {
                IQuest.SetQuest("Get out of the warehouse");
                IObjectives.SetObjectives("");
            }
            else
            {
                IObjectives.SetObjectives("Find the Main Door's key");
            }

        }
    }
}
