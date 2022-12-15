using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class WellBucket : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject key;
    [SerializeField] GameObject bucket;

    public CircularDrive circularDrive;
    public float minZ, maxZ;
    public UnityEvent onBucketBottom;

    private bool hasBeenDown = false;
    private bool recentlyUp = true;

    private void Start()
    {
        key.SetActive(false);
        bucket.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += new Vector3(0, 0, circularDrive.rotationSpeed*100);
        if(transform.localPosition.z < minZ)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, minZ);
            if (hasBeenDown)
            {
                GetKey();
            }
        }
        if (transform.localPosition.z > maxZ)
        {

            if (GetComponent<BucketPlacement>().isInWell)
            {
                hasBeenDown = true;
                if (recentlyUp)
                {
                    onBucketBottom.Invoke();
                    recentlyUp = false;
                }
            }
            
            
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, maxZ);
        }
        else
        {
            recentlyUp = true;
        }
    }

    private void GetKey()
    {
        key.SetActive(true);
        bucket.SetActive(true);
        GetComponent<BucketPlacement>().attachedBucket.SetActive(false);
    }
}
