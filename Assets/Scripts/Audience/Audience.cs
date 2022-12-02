using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Valve.VR;

public class Audience : MonoBehaviour
{
    private void Start()
    {
        SetTrackerIndex();
    }

    private void Update()
    {
        //SetCameraPosition();
        //RenderWindow();
    }

    private void RenderWindow()
    {
        /*
        Renderer windowRenderer = _Window.GetComponent<Renderer>();
        Vector3 boxMin = windowRenderer.bounds.min;
        Vector3 boxMax = windowRenderer.bounds.max;

        Camera cam = GetComponentInChildren<Camera>();

        // Convert to screen space
        Vector3 cboxMin = cam.WorldToScreenPoint(boxMin);
        Vector3 cboxMax = cam.WorldToScreenPoint(boxMax);

        // Scale from 0 to 1
        Vector2 newMin = new Vector2(cboxMin.x / cam.pixelWidth, cboxMin.y / cam.pixelHeight);
        Vector2 newMax = new Vector2(cboxMax.x / cam.pixelWidth, cboxMax.y / cam.pixelHeight);

        Debug.Log(newMin + " to " + newMax);

        //Debug.Log(newMin);
        //_Camera.rect = new Rect(newMin.x, newMin.y, Mathf.Abs(newMax.x - newMin.x), Mathf.Abs(newMax.y - newMin.y));
        */

    }

    // Set external camera to look away from audience (simple solution)
    private void SetCameraPosition()
    {
        //Vector3 lookDirection = _Camera.transform.position - transform.position;
        //Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        //_Camera.transform.rotation = lookRotation;
    }

    // Sets this object to track the Vive Tracker
    private void SetTrackerIndex()
    {
        uint index = 0;

        var error = ETrackedPropertyError.TrackedProp_Success;
        
        // Loops through all potential devices
        for (uint i = 0; i < 16; i++)
        {
            var result = new System.Text.StringBuilder((int)64);
            // Gets model name of device with index i
            OpenVR.System.GetStringTrackedDeviceProperty(i, ETrackedDeviceProperty.Prop_RenderModelName_String, result, 64, ref error);
            
            // If the device is a tracker, we're done
            if (result.ToString().Contains("tracker"))
            {
                index = i;
                break;
            }
        }

        GetComponent<SteamVR_TrackedObject>().index = (SteamVR_TrackedObject.EIndex) index;
    }
}
