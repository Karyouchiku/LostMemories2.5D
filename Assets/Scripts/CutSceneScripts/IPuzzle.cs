using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuzzle 
{
    
    public void MovePlayer(bool move);//always false 
    public void MovePlayer(bool move, float moveSpeed, int MoveToPositionID);
    public void DisableControls(bool turn);
    public void FinishingPuzzle();


}
