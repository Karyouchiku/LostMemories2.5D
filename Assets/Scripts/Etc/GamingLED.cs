using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamingLED : MonoBehaviour, IInteractor
{
    public GameObject _LEDLights;
    public bool turn;
    public void Interact()
    {
        turn = !turn;
        LightsOn(turn);
    }
    void LightsOn(bool turn)
    {
        _LEDLights.SetActive(turn);
    }
}
