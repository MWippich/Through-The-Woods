using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class WellBucket : MonoBehaviour
{
    // Start is called before the first frame update

    public CircularDrive circularDrive;

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(0, circularDrive.rotationSpeed, 0);
    }
}
