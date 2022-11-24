using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuzzle2
{
    public void EndingPuzzle();
    public void GotoPosition(int actorID, int locationID);
    public void GotoPosition(int actorID, int locationID, float moveSpeed);
    public void GotoPosition(int actorID, int locationID, float moveSpeed, float waitSec);
}
