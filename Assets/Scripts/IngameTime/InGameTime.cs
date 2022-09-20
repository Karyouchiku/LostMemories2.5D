using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTime : MonoBehaviour
{
    public float inGameTimeSpeed;

    GameObject lighting;
    Light light;
    // Start is called before the first frame update
    void Start()
    {
        lighting = transform.gameObject;
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        lighting.transform.Rotate(new Vector3(inGameTimeSpeed * Time.deltaTime, 0f));
        
        if (lighting.transform.rotation.eulerAngles.x >= 160 && lighting.transform.rotation.eulerAngles.x <= 180)
        {
            //light.color = Color.ye;
            light.color = new Color(1f, 0.92f, 0.17f, 1f);
        }
        if (lighting.transform.rotation.eulerAngles.x >= 180 && lighting.transform.rotation.eulerAngles.x <= 360)
        {
            light.color = Color.black;
        }
        if (lighting.transform.rotation.eulerAngles.x >= 0 && lighting.transform.rotation.eulerAngles.x <= 30)
        {
            light.color = Color.yellow;
        }
        if (lighting.transform.rotation.eulerAngles.x >= 30 && lighting.transform.rotation.eulerAngles.x <= 160)
        {
            light.color = Color.white;
        }
    }
}
