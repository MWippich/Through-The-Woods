using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazePeripheryEnable : MonoBehaviour, IGazeSensitve
{

    public peripheryMinAngle = 60f;
    public Component[] components;
    private bool enabledAll;

    // Start is called before the first frame update
    void Start()
    {
        enabledAll = true;
    }

    public void UpdateGaze(float angle, float distToPlayer, bool blinknig)
    {
        if(distToPlayer > 1 && angle >= peripheryMinAngle)
        {
            if (!enabledAll)
            {
                EnableAll(true);
            }
        } else if (enabledAll)
        {
            EnableAll(false)
        }
    }

    private void EnableAll(bool val)
    {
        foreach(Component comp in components)
        {
            comp.enabled = val;    
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
