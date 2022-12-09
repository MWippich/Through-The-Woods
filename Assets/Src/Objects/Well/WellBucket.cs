using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class WellBucket : MonoBehaviour
{
    // Start is called before the first frame update

    public CircularDrive circularDrive;
    public float minZ, maxZ;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += new Vector3(0, 0, circularDrive.rotationSpeed*100);
        if(transform.localPosition.z < minZ)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, minZ);
        }
        if (transform.localPosition.z > maxZ)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, maxZ);
        }
    }
}
