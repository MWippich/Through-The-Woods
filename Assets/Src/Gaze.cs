using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;
using Tobii.StreamEngine;
using Tobii.XR;
using UnityEngine.Events;

public class Gaze : MonoBehaviour
{

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

        UpdateBlink(data);
    }

    void UpdateBlink(TobiiXR_EyeTrackingData data)
    {
        bool currBlink = gazeRay.Direction == Vector3.forward && gazeRay.Origin == Vector3.zero;

        framesSinceChange += 1;
        if (blinking)
        {
            blinkTime += Time.deltaTime;
            if (!longBlink && blinkTime >= longBlinkThreshold)
            {
                longBlink = true;
                onLongBlink.Invoke();
            }
        }

        if (currBlink && !blinking)
        {
            if (framesSinceChange >= 3)
            {
                blinking = true;
                onBlinkStart.Invoke();
            }
            framesSinceChange = 0;
        }

        if (blinking && !currBlink)
        {
            if (framesSinceChange >= 3)
            {
                blinking = false;
                onBlinkEnd.Invoke();
                onBlinkEndTime.Invoke(blinkTime);
                numBlinks++;
                Debug.Log(numBlinks);
                if (longBlink)
                {
                    numLongBlinks++;
                    Debug.Log("long: " + numLongBlinks);
                    longBlink = false;
                    onLongBlinkEnd.Invoke();
                    onLongBlinkEndTime.Invoke(blinkTime);
                }
                blinkTime = 0.0f;
            }
            framesSinceChange = 0;
        }
        
    }


}
