using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class ResetPosition : MonoBehaviour
{
    
    public bool resetCameraOne;
    public bool resetCameraTwo;
    public bool resetCameraThree;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnValidate()
    {

        if (resetCameraOne){
            Transform child = transform.Find("Projection Plane One").Find("TrackerOffset");
            child.localPosition = -child.GetChild(0).gameObject.transform.localPosition;
            resetCameraOne = false;  
        }
        if (resetCameraTwo)
        {
            Transform child = transform.Find("Projection Plane Two").Find("TrackerOffset");
            child.localPosition = -child.GetChild(0).gameObject.transform.localPosition;
            resetCameraTwo = false;
        }
        if (resetCameraThree)
        {
            Transform child = transform.Find("Projection Plane Three").Find("TrackerOffset");
            child.localPosition = -child.GetChild(0).gameObject.transform.localPosition;
            resetCameraThree = false;
        }

    }


}
