using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackTransitioning : MonoBehaviour
{
    public Animator blackTransition;

    bool manualOn;

    public void StartTransition2ndVer()
    {
        blackTransition.gameObject.SetActive(true);
        blackTransition.SetBool("isStart", true);
    }

    public void ManualTransitionON()
    {
        blackTransition.gameObject.SetActive(true);
        blackTransition.SetBool("isTransitioning", true);
        Camera.main.GetComponentInParent<CamFollowPlayer>().removeAnimation = true;
        manualOn = true;
    }

    public void ManualTransitionOFF()
    {
        StartCoroutine(ManualTransitionOFFCoroutine());
    }
    IEnumerator ManualTransitionOFFCoroutine()
    {
        Camera.main.GetComponentInParent<CamFollowPlayer>().removeAnimation = false;
        blackTransition.SetBool("isStart", false);
        blackTransition.SetBool("isTransitioning", false);
        yield return new WaitForSeconds(1);
        manualOn = false;
        blackTransition.gameObject.SetActive(false);
    }

    public void StartTransition()
    {
        StartCoroutine(Transition());
    }
    IEnumerator Transition()
    {
        if (!manualOn)
        {
            blackTransition.gameObject.SetActive(true);
            blackTransition.SetBool("isTransitioning", true);
            Camera.main.GetComponentInParent<CamFollowPlayer>().removeAnimation = true;
        }
        yield return new WaitForSeconds(1.5f);
        Camera.main.GetComponentInParent<CamFollowPlayer>().removeAnimation = false;
        blackTransition.SetBool("isTransitioning", false);
        yield return new WaitForSeconds(1f);
        manualOn = false;
        blackTransition.gameObject.SetActive(false);
    }
}
