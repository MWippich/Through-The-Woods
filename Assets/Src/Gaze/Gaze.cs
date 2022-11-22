using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;
using Tobii.StreamEngine;
using Tobii.XR;
using UnityEngine.Events;
using UnityEngine.Assertions;

public class Gaze : MonoBehaviour
{

    public GameObject player;

    public float longBlinkThreshold = 1.0f;

    public UnityEvent onBlinkStart, onBlinkEnd, onLongBlink, onLongBlinkEnd;
    public UnityEvent<float> onBlinkEndTime, onLongBlinkEndTime;
    bool blinking = false, longBlink = false;
    float blinkTime = 0f;
    int framesSinceChange = 0;
    int numBlinks = 0, numLongBlinks = 0;


    TobiiXR_GazeRay gazeRay;

    // Update is called once per frame
    void Update()
    {
        TobiiXR_EyeTrackingData data = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);

        gazeRay = data.GazeRay;
        Debug.DrawRay(gazeRay.Origin, gazeRay.Direction);

        UpdateBlink();
        UpdateGaze();
    }

    private void UpdateGaze()
    {
        GameObject[] gazeSensitives = GameObject.FindGameObjectsWithTag("GazeSensitive");
        foreach (GameObject gazeSensitive in gazeSensitives)
        {

            float angle = 180f;
            float dist = (player.transform.position - gazeSensitive.transform.position).magnitude;
            bool currBlink = gazeRay.Direction == Vector3.forward && gazeRay.Origin == Vector3.zero;
            if (!currBlink)
            {
                Vector3 toObj = gazeSensitive.transform.position - gazeRay.Origin;

                angle = Vector3.Angle(gazeRay.Direction, toObj);
            }

            foreach (IGazeSensitve comp in gazeSensitive.GetComponents(typeof(IGazeSensitve)))
            {
                comp.UpdateGaze(angle, dist);
            }
        }
    }

    private void UpdateBlink()
    {
        bool currBlink = gazeRay.Direction == Vector3.forward && gazeRay.Origin == Vector3.zero;

        framesSinceChange += 1;
        if (blinking)
        {
            blinkTime += Time.deltaTime;
            if (!longBlink && blinkTime >= longBlinkThreshold)
            {
                longBlink = true;
                OnLongBlinkStart();
            }
        }

        if (currBlink && !blinking)
        {
            if (framesSinceChange >= 2)
            {
                blinking = true;
                OnBlinkStart();
            }
            framesSinceChange = 0;
        }

        if (blinking && !currBlink)
        {
            if (framesSinceChange >= 2)
            {
                blinking = false;
                OnBlinkEnd(blinkTime);
                numBlinks++;
                Debug.Log(numBlinks);
                if (longBlink)
                {
                    numLongBlinks++;
                    Debug.Log("long: " + numLongBlinks);
                    longBlink = false;
                    OnLongBlinkEnd(blinkTime);
                }
                blinkTime = 0.0f;
            }
            framesSinceChange = 0;
        }

    }

    private void OnBlinkStart()
    {

        onBlinkStart.Invoke();

        GameObject[] gazeSensitives = GameObject.FindGameObjectsWithTag("GazeSensitive");
        foreach (GameObject gazeSensitive in gazeSensitives)
        {
            float dist = (player.transform.position - gazeSensitive.transform.position).magnitude;
            foreach (IGazeSensitve comp in gazeSensitive.GetComponents(typeof(IGazeSensitve)))
            {
                comp.OnBlinkStart(dist);
            }
        }
    }
    private void OnLongBlinkStart()
    {

        onLongBlink.Invoke();

        GameObject[] gazeSensitives = GameObject.FindGameObjectsWithTag("GazeSensitive");
        foreach (GameObject gazeSensitive in gazeSensitives)
        {
            float dist = (player.transform.position - gazeSensitive.transform.position).magnitude;
            foreach (IGazeSensitve comp in gazeSensitive.GetComponents(typeof(IGazeSensitve)))
            {
                comp.OnLongBlinkStart(dist);
            }
        }
    }
    private void OnBlinkEnd(float time)
    {
        onBlinkEnd.Invoke();
        onBlinkEndTime.Invoke(time);

        GameObject[] gazeSensitives = GameObject.FindGameObjectsWithTag("GazeSensitive");
        foreach (GameObject gazeSensitive in gazeSensitives)
        {
            float dist = (player.transform.position - gazeSensitive.transform.position).magnitude;
            foreach (IGazeSensitve comp in gazeSensitive.GetComponents(typeof(IGazeSensitve)))
            {
                comp.OnBlinkEnd(dist, time);
            }
        }
    }

    private void OnLongBlinkEnd(float time)
    {
        onLongBlinkEnd.Invoke();
        onLongBlinkEndTime.Invoke(blinkTime);

        GameObject[] gazeSensitives = GameObject.FindGameObjectsWithTag("GazeSensitive");
        foreach (GameObject gazeSensitive in gazeSensitives)
        {
            float dist = (player.transform.position - gazeSensitive.transform.position).magnitude;
            foreach (IGazeSensitve comp in gazeSensitive.GetComponents(typeof(IGazeSensitve)))
            {
                comp.OnLongBlinkEnd(dist, time);
            }
        }
    }

}
