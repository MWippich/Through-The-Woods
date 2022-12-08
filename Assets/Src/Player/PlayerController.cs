using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 moveDirectionAction;
    public List<SteamVR_Action_Boolean> moveActions;
    public SteamVR_ActionSet activate;
    public GameObject playerCamera, lHand, rHand;

    public float moveSpeed;
    public float maxSpeedThreshold = 0.8f;
    private Rigidbody rb;

    private bool moving = false;

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
            if (!moving)
            {
                lHand.GetComponent<HandPhysics>().onMoving(true);
                rHand.GetComponent<HandPhysics>().onMoving(true);
                moving = true;
            }

            // TODO: Check whether this is correct? Move speed is currently higher backwards
            Vector3 moveVec = new Vector3(Mathf.Clamp(m.x, -maxSpeedThreshold, maxSpeedThreshold) / maxSpeedThreshold, 0f, Mathf.Clamp(m.y, -maxSpeedThreshold, maxSpeedThreshold) / maxSpeedThreshold);

            rb.MovePosition(transform.position + Vector3.ProjectOnPlane(playerCamera.transform.TransformDirection(moveVec), Vector3.up) * Time.deltaTime * moveSpeed);
            //Debug.Log(moveVec);
        } else
        {
            if (moving)
            {
                lHand.GetComponent<HandPhysics>().onMoving(false);
                rHand.GetComponent<HandPhysics>().onMoving(false);

                moving = false;
            }

        }

    }
}
