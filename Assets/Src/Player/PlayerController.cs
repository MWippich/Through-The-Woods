using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 moveDirectionAction;
    public List<SteamVR_Action_Boolean> moveActions;
    public SteamVR_ActionSet activate;
    public GameObject playerCamera;

    public float moveSpeed;
    public float maxSpeedThreshold = 0.8f;
    private Rigidbody rb; 


    // Start is called before the first frame update
    void Start()
    {
        activate.Activate();

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        Vector2 m = moveDirectionAction[SteamVR_Input_Sources.LeftHand].axis;
        bool pressed = moveActions.Any((SteamVR_Action_Boolean action)  =>  action[SteamVR_Input_Sources.LeftHand].state);

        if (pressed)
        {
            Vector3 moveVec = new Vector3(Mathf.Clamp(m.x, -maxSpeedThreshold, maxSpeedThreshold) / maxSpeedThreshold, 0f, Mathf.Clamp(m.y, -maxSpeedThreshold, maxSpeedThreshold) / maxSpeedThreshold);
            
            rb.MovePosition(transform.position + Vector3.ProjectOnPlane(playerCamera.transform.TransformDirection(moveVec), Vector3.up) * Time.deltaTime * moveSpeed);
        }

    }
}
