using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackTransitioning : MonoBehaviour
{
    public Animator blackTransition;

    public void StartTransition()
    {
        StartCoroutine(Transition());
    }
    IEnumerator Transition()
    {
        blackTransition.gameObject.SetActive(true);

        blackTransition.SetBool("isTransitioning", true);

        yield return new WaitForSeconds(2);
        blackTransition.gameObject.SetActive(false);
        
    }
}
