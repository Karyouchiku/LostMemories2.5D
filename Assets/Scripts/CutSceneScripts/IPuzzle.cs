using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuzzle 
{
    public void MovePlayer(bool move);
    public void MovePlayer(bool move, float moveSpeed);
    public void DisableControls(bool turn);
    public void RestrictedArea(int MoveToPositionID);
}
