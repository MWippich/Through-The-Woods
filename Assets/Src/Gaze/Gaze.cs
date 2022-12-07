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
        //Get all objects tagged as "GazeSensitive" and iterate over them
        GameObject[] gazeSensitives = GameObject.FindGameObjectsWithTag("GazeSensitive");
        foreach (GameObject gazeSensitive in gazeSensitives)
        {

            float angle = 180f;
            float dist = (player.transform.position - gazeSensitive.transform.position).magnitude;
            // If the gaze ray has the default values, the user is blinking
            bool currBlink = gazeRay.Direction == Vector3.forward && gazeRay.Origin == Vector3.zero;
            if (!currBlink)
            {
                //Calculate the direction of the ray, relative to this gazeSensitive object and save the angle to the object.
                Vector3 toObj = gazeSensitive.transform.position - gazeRay.Origin;

                angle = Vector3.Angle(gazeRay.Direction, toObj);
            }

            foreach (IGazeSensitve comp in gazeSensitive.GetComponents(typeof(IGazeSensitve)))
            {
                //For every IGazeSensitive component, do "UpdateGaze" with the distance to the player and the angle to the object.
                //If the player is blinking angle is the max value of 180.
                comp.UpdateGaze(angle, dist, currBlink);
            }
        }
    }

    private void UpdateBlink()
    {
        
        // If the gaze ray has the default values, the user is blinking
        bool currBlink = gazeRay.Direction == Vector3.forward && gazeRay.Origin == Vector3.zero;

        framesSinceChange += 1;
        if (blinking)
        {
            //increment blink time.
            blinkTime += Time.deltaTime;
            if (!longBlink && blinkTime >= longBlinkThreshold)
            {
                //If the user blinked long enough, mark as "long blink" and fire "longBlink" Event.
                longBlink = true;
                OnLongBlinkStart();
            }
        }

        //User just started blinking
        if (currBlink && !blinking)
        {
            //Blink must last at least 2 frames, otherwise we assume it is a hardware inaccuracy.
            if (framesSinceChange >= 2)
            {
                blinking = true;
                OnBlinkStart();
            }
            framesSinceChange = 0;
        }

        //User just stopped blinknig
        if (blinking && !currBlink)
        {
            //Blinknig must be false at least to frames to be registered, otherwise assumed to be hardware inaccuracy.
            if (framesSinceChange >= 2)
            {
                //Blink ended, fire blink ended event
                blinking = false;
                OnBlinkEnd(blinkTime);
                numBlinks++;
                //Debug.Log(numBlinks);
                if (longBlink)
                {
                    // If long blink, fire long bling event
                    numLongBlinks++;
                    //Debug.Log("long: " + numLongBlinks);
                    longBlink = false;
                    OnLongBlinkEnd(blinkTime);
                }
                blinkTime = 0.0f;
            }
            framesSinceChange = 0;
        }

    }
    
    //Below are functions to fire various events, both from this class, as well as calling the IGazeSensitive components.

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
