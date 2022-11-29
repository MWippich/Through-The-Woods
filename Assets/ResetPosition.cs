using Apt.Unity.Projection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class ResetPosition : MonoBehaviour
{
    public bool leftBorder;
    public bool rightBorder;

    [SerializeField] Transform tracker;
    [SerializeField] Transform cameraOffset;
    [SerializeField] ProjectionPlane plane;


    public Vector3 topLeft;
    public Vector3 bottomRight;

    public Vector3 centerOffset;
    public Vector3 virtualCenterPoint;

    public float angleToPlane;

    public TrackerBase trackerBase;

    [SerializeField]
    private ProjectionPlaneCamera projectionCamera;
    private Vector3 initialLocalPosition;

    private void Start()
    {
        initialLocalPosition = projectionCamera.transform.localPosition;
    }

    private void OnValidate()
    {

        if (leftBorder)
        {
            AddTopLeft();  
        }
        if (rightBorder)
        {
            AddBottomRight();
        }
    }

    public void AddTopLeft()
    {
        topLeft = tracker.localPosition;
        leftBorder = false;

        CalculateCenter();
    }

    public void AddBottomRight()
    {
        bottomRight = tracker.localPosition;
        rightBorder = false;

        CalculateCenter();
    }

    private bool HaveBothCorners()
    {
        return topLeft != Vector3.zero && bottomRight != Vector3.zero;
    }

    private void CalculateCenter()
    {
        if (HaveBothCorners())
        {
            virtualCenterPoint = (plane.TopLeft + plane.BottomRight) / 2;
            centerOffset = (topLeft + bottomRight) / 2;

            CalculateAngle();
        }
    }

    private void CalculateAngle()
    {
        Vector3 realProjected = Vector3.ProjectOnPlane(bottomRight - topLeft, Vector3.up);
        Vector3 virtualProjected = Vector3.ProjectOnPlane(plane.BottomRight - plane.TopLeft, Vector3.up);

        angleToPlane = Vector3.SignedAngle(realProjected, virtualProjected, Vector3.up) - transform.localRotation.eulerAngles.y; 
    }

    private void Update()
    {
        if (trackerBase == null)
            return;

        if (trackerBase.IsTracking) 
        {
            Transform offset = tracker.parent;
            offset.localPosition = -centerOffset;

            Quaternion rot = Quaternion.AngleAxis(angleToPlane, Vector3.up); 

            projectionCamera.transform.localPosition = rot * (initialLocalPosition + trackerBase.Translation);// - virtualCenterPoint) + virtualCenterPoint;

        }
    }
}
