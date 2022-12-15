using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BucketPlacement : MonoBehaviour
{
    public bool isInWell = false;
    private bool isHoldingBucket = false;
    private bool attached = false;
    public GameObject attachedBucket, looseBucket;

    private void Start()
    {
        attachedBucket.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.parent.CompareTag("Bucket"))
        {
            Debug.Log("Yay, a bucket!");
            looseBucket = collision.transform.parent.gameObject;
            CheckActivate();
            isInWell = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.parent.CompareTag("Bucket"))
        {
            isInWell = false;
        }
    }

    public void SetHoldingBucket(bool holding)
    {
        isHoldingBucket = holding;
        CheckActivate();
        Debug.Log("Holding: " + holding);
    }

    private void CheckActivate()
    {
        if (attached)
        {
            return;
        }
        if (isInWell && !isHoldingBucket && !attachedBucket.activeInHierarchy)
        {

            attachedBucket.SetActive(true);
            looseBucket.SetActive(false);
            attached = true;
        }
    }
}
