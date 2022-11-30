using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazePeripheryEnable : MonoBehaviour, IGazeSensitve
{

    public float peripheryMinAngle = 60f;
    public float peripheryBorderRange = 10f;
    public GameObject[] enableGameObjects;
    public float maxLightIntensity = 1f;
    public float interpolationTime = 0.25f;
    public bool interpolate = true;

    private float targetIntensity = 1f;
    private float currentIntensity = 1f;
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

            if (interpolate)
            {
                if (angle <= peripheryMinAngle)
                {
                    targetIntensity = 0f;
                }
                else if (angle > peripheryMinAngle + peripheryBorderRange)
                {
                    targetIntensity = maxLightIntensity;
                }
                else
                {
                    targetIntensity = Mathf.Clamp((angle - peripheryMinAngle) / peripheryBorderRange, 0f, 1f) * maxLightIntensity;
                }
            } else
            {
                if (angle <= peripheryMinAngle)
                {
                    if (enabledAll)
                    {
                        EnableAll(false);
                        enabledAll = false;
                    }
                }
                else
                {
                    if (!enabledAll)
                    {
                        EnableAll(true);
                        enabledAll = true;
                    }
                }
            }
        }

        if (interpolate)
        {
            InterpolateAll();
        }
    }

    private void InterpolateAll()
    {
        float maxDiff = maxLightIntensity * Time.deltaTime / interpolationTime;
        float diff = Mathf.Clamp(targetIntensity - currentIntensity , -maxDiff, maxDiff);

        currentIntensity = Mathf.Clamp(currentIntensity + diff , 0, maxLightIntensity);

        if(currentIntensity <= 0.1)
        {
            if (enabledAll)
            {
                EnableAll(false);
                enabledAll = false;
            }
        } else
        {
            if (!enabledAll)
            {
                EnableAll(true);
                enabledAll = true;
            }
        }

        foreach (GameObject go in enableGameObjects)
        {
            Light light = go.GetComponent<Light>();
            if (light != null)
            {
                light.intensity = currentIntensity;
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
