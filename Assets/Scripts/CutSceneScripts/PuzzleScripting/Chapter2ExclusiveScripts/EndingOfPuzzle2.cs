using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingOfPuzzle2 : MonoBehaviour, IInteractor
{
    IPuzzle2 puzzle;
    PortalDoor thisDoor;
    void Start()
    {
        puzzle = GetComponentInParent<IPuzzle2>();
        thisDoor = GetComponent<PortalDoor>();
    }

    public void Interact()
    {
        if (thisDoor.locked)
        {
            IObjectives.SetObjectives("Find the Main Door's key");
        }
        else
        {
            IQuest.SetQuest("Get out of the warehouse");
            IObjectives.SetObjectives("");
            puzzle.EndingPuzzle();
        }
    }
}
