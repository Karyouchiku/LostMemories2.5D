using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prologue1 : MonoBehaviour
{
    [Header("Actors")]
    public GameObject[] actor;
    Vector3 movingAnim;
    PlayerAnimations pAnim;
    [HeaderAttribute("Goto Locations")]
    public GameObject[] loc;
    Vector3 target;

    [Header("Data")]
    public float mSpeed;
    public int locNum;
    public bool startMove;
    public PortalDoor door;

    void Start()
    {
        for (int i = 0; i < actor.Length; i++)
        {
            pAnim = actor[i].GetComponent<PlayerAnimations>();
        }
    }
    void Update()
    {
        if (startMove)
        {
            movingAnim = target - actor[0].transform.position;
            target.y = actor[0].transform.position.y;
            actor[0].transform.position = Vector3.MoveTowards(actor[0].transform.position, target, mSpeed * Time.deltaTime);
            pAnim.moveX = movingAnim.x;
            pAnim.moveZ = movingAnim.z;
        }
    }
    public void GoOutSide()
    {
        locNum = 0;
        target = loc[locNum].transform.position;
        startMove = true;
    }

    void OnEnable()
    {
        LocationChecker.onPlayerEnterCol += ChangeLocation;
    }
    void OnDisable()
    {
        LocationChecker.onPlayerEnterCol -= ChangeLocation;
    }

    void ChangeLocation()
    {
        locNum++;
        if (locNum < loc.Length)
        {
            target = loc[locNum].transform.position;
        }
        else
        {
            startMove = false;
            door.Interact();
        }
    }
}
