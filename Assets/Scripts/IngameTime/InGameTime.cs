using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTime : MonoBehaviour
{
    public float inGameTimeSpeed;

    GameObject lighting;
    Light sunLight;
    // Start is called before the first frame update
    void Start()
    {
        lighting = transform.gameObject;
        sunLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        lighting.transform.Rotate(new Vector3(inGameTimeSpeed * Time.deltaTime, 0f));
        
        if (lighting.transform.rotation.eulerAngles.x >= 160 && lighting.transform.rotation.eulerAngles.x <= 180)
        {
            //light.color = Color.ye;
            sunLight.color = new Color(1f, 0.92f, 0.17f, 1f);
        }
        if (lighting.transform.rotation.eulerAngles.x >= 180 && lighting.transform.rotation.eulerAngles.x <= 360)
        {
            sunLight.color = Color.black;
        }
        if (lighting.transform.rotation.eulerAngles.x >= 0 && lighting.transform.rotation.eulerAngles.x <= 30)
        {
            sunLight.color = Color.yellow;
        }
        if (lighting.transform.rotation.eulerAngles.x >= 30 && lighting.transform.rotation.eulerAngles.x <= 160)
        {
            sunLight.color = Color.white;
        }
    }
}
