using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlinkChildReplace : MonoBehaviour, IGazeSensitve
{

    public int blinkInterval = 1;
    public List<GameObject> objects1, objects2;

    public bool changeOnlyInPeriphery = false;
    public float peripheryAngle = 30f;

    private bool swap = true;
    private int blinkCounter = 0;

    private float currentAngle = 0f;

    private void Start()
    {
        foreach (GameObject go in objects1)
        {
            go.SetActive(swap);
        }
        foreach (GameObject go in objects2)
        {
            go.SetActive(!swap);
        }
    }

    public void UpdateGaze(float angle, float distToPlayer, bool blinking)
    {
        if (!blinking && angle < 179.5f) {
            currentAngle = angle;
        }
    }

    public void OnBlinkStart(float distToPlayer)
    {
        blinkCounter++;

        if(distToPlayer < 1)
        {
            return;
        }

        Debug.Log("angle:");
        Debug.Log(currentAngle);
        if(changeOnlyInPeriphery && currentAngle <= peripheryAngle)
        {
            return;
        }

        if(blinkCounter >= blinkInterval)
        {
            blinkCounter = 0;
            swap = !swap;
            foreach (GameObject go in objects1)
            {
                go.SetActive(swap);
            }
            foreach (GameObject go in objects2)
            {
                go.SetActive(!swap);
            }
        }
    }
}
