using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.GetChild(0).GetComponent<ResetPosition>().AddTopLeft();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.GetChild(0).GetComponent<ResetPosition>().AddBottomRight();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.GetChild(1).GetComponent<ResetPosition>().AddTopLeft();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            transform.GetChild(1).GetComponent<ResetPosition>().AddBottomRight();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            transform.GetChild(2).GetComponent<ResetPosition>().AddTopLeft();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            transform.GetChild(2).GetComponent<ResetPosition>().AddBottomRight();
        }
    }
}
