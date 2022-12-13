using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class WellBucket : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject key;
    [SerializeField] GameObject bucket;

    private Vector3 initKeyPos;

    public CircularDrive circularDrive;
    public float minZ, maxZ;

    private void Start()
    {
        initKeyPos = key.transform.position;
        key.SetActive(false);
    }

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
            GetKey();
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, maxZ);
        }
    }

    private void GetKey()
    {
        // TODO: Check if bucket is in well
        key.SetActive(true);
        key.transform.position = initKeyPos;
    }
}
