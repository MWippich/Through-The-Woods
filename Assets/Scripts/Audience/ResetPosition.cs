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
    private float cameraScaling;

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
            CalculateScaling();
        }
    }

    private void CalculateAngle()
    {
        // Project screen and window positions to 2D plane
        Vector3 realProjected = Vector3.ProjectOnPlane(bottomRight - topLeft, Vector3.up);
        Vector3 virtualProjected = Vector3.ProjectOnPlane(plane.BottomRight - plane.TopLeft, Vector3.up);

        // Get angle between projected vectors
        angleToPlane = Vector3.SignedAngle(realProjected, virtualProjected, Vector3.up) - transform.localRotation.eulerAngles.y; 
    }

    private void CalculateScaling()
    {
        float screenMagnitude = Vector3.Magnitude(topLeft - bottomRight);
        float windowMagnitude = Vector3.Magnitude(plane.TopLeft - plane.BottomRight);

        cameraScaling = windowMagnitude / screenMagnitude;
    }

    private void Update()
    {
        OffsetPosition();
        ScalePosition();
    }

    // Offset camera position based on real world tracker position
    private void OffsetPosition()
    {
        if (InactiveTracker())
            return;
        
        // Set tracker offset
        tracker.parent.localPosition = -centerOffset;

        // Rotate camera according to angle between virtual window and real world screen
        Quaternion rot = Quaternion.AngleAxis(angleToPlane, Vector3.up);
        projectionCamera.transform.localPosition = rot * (initialLocalPosition + trackerBase.Translation);
    }

    // Scale camera position based on screen and window size difference
    private void ScalePosition()
    {
        Vector3 current = projectionCamera.transform.localPosition;
        projectionCamera.transform.localPosition = new Vector3(current.x * cameraScaling, current.y * cameraScaling, current.z);
    }

    // Returns true if tracker is non-existent or is not currently being tracked
    private bool InactiveTracker()
    {
        return trackerBase == null || !trackerBase.IsTracking;
    }
}
