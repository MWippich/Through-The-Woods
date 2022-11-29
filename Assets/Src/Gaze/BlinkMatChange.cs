using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkMatChange : MonoBehaviour, IGazeSensitve
{

    public Material m1, m2;
    private bool swap = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateGaze(float angle, float distToPlayer, bool blinknig)
    {
        //Debug.Log(angle);
        //Debug.Log(distToPlayer);
    }



    public void OnBlinkStart(float distToPlayer)
    {
        swap = !swap;
        if (swap)
        {
            Debug.Log("m1");
            GetComponent<Renderer>().material = m1;
        } else
        {
            Debug.Log("m2");
            GetComponent<Renderer>().material = m2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
