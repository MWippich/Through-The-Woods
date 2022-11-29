using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazePeripheryEnable : MonoBehaviour, IGazeSensitve
{

    public float peripheryMinAngle = 60f;
    public GameObject[] enableGameObjects;
    private bool enabledAll;

    // Start is called before the first frame update
    void Start()
    {
        enabledAll = true;
    }

    public void UpdateGaze(float angle, float distToPlayer, bool blinking)
    {
        if (!blinking)
        {
            if (distToPlayer > 1 && angle >= peripheryMinAngle)
            {
                if (!enabledAll)
                {
                    EnableAll(true);
                }
            }
            else if (enabledAll)
            {
                EnableAll(false);
            }
        }
    }

    private void EnableAll(bool val)
    {
        foreach(GameObject go in enableGameObjects)
        {
            go.SetActive(val); 
        }
        enabledAll = val;
    }

    public void OnBlinkStart(float distToPlayer)
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
