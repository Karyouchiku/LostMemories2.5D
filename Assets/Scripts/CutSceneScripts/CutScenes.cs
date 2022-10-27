using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CutScenes
{
    public void StartMoving();
    public void EnterDoor();
    public void ChangeLocation(int i);
    public void EndingScene();
    public void LocationCheck();
}
