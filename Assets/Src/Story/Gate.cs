using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gate : MonoBehaviour
{
    [SerializeField] private Transform gate;
    private Quaternion closedGate;
    private Quaternion openGate;
    
    private bool gateOpened = false; // Trigger only once
    private bool gateClosed = false; // Trigger only once
    private AudioSource gateAudio;

    public AudioClip gateOpen;
    public AudioClip gateClose;

    private StepHandler stepHandler;

    private bool open = true;

    private void Start()
    {
        gateAudio = GetComponent<AudioSource>();
        stepHandler = GetComponent<StepHandler>();

        closedGate = gate.rotation;
        openGate = closedGate;
        openGate.eulerAngles = closedGate.eulerAngles + new Vector3(0, -90, 0);
    }

    private void Update()
    {
        if (gateAudio.isPlaying)
            RotateGate();
    }

    private void RotateGate()
    {
        float t = gateAudio.time / gateAudio.clip.length;

        if (open)
            gate.rotation = Quaternion.Slerp(closedGate, openGate, t);
        else
        {
            gate.rotation = Quaternion.Slerp(openGate, closedGate, t);

            if (t >= 0.9f && stepHandler.enabled)
                stepHandler.AdvanceStory();
        }
            
    }

    private void UpdateClip(AudioClip clip)
    {
        if (gateAudio.clip != clip)
            gateAudio.clip = clip;
    }

    private void OpenGate()
    {
        if (stepHandler.IsInCube())
        {
            Debug.Log("Gate Opened");
            UpdateClip(gateOpen);
            gateAudio.Play();
            gateOpened = true;
        }
    }

    private void CloseGate()
    {
        if (IsPastGate())
        {
            UpdateClip(gateClose);
            gateAudio.Play();
            gateClosed = true;
            open = false;
        }
    }

    private bool IsPastGate()
    {
        Vector3 playerPos = stepHandler.GetPlayer().position;
        Vector3 gatePos = gate.position;

        return gatePos.z > playerPos.z && Vector3.Distance(playerPos, gatePos) > 6.0;
    }

    public void LongBlink()
    {
        //Debug.Log("There was a long blink");
        //whispers.Play();
        if (!gateOpened)
            OpenGate();

    }

    public void LongBlinkEnd()
    {
        //Debug.Log("There was a long blink");
        //whispers.Stop();
    }

    public void Blink()
    {
        //Debug.Log("There was a blink");
        if (!gateAudio.isPlaying && gateOpened && !gateClosed)
            CloseGate();
    }
}
