using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider item)
    {
        ICollectible collectible = item.GetComponent<ICollectible>();
        if (collectible != null)
        {
            collectible.Collect();
        }
    }
}
