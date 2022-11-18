using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 moveAction;
    public SteamVR_ActionSet activate;

    // Start is called before the first frame update
    void Start()
    {
        activate.Activate();


    }

    // Update is called once per frame
    void Update()
    {

        Vector2 m = moveAction[SteamVR_Input_Sources.RightHand].axis;
        //Vector2 ax = trackpad[SteamVR_Input_Sources.RightHand].pose;

    }
}
