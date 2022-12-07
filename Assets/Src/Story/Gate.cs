using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private AudioSource whispers;

    private void Start()
    {
        whispers = GetComponent<AudioSource>();
    }

    public void OpenGate()
    {
        Debug.Log("Gate Opened");
    }

    public void LongBlink()
    {
        Debug.Log("There was a long blink");
        whispers.Play();

    }

    public void LongBlinkEnd()
    {
        Debug.Log("There was a long blink");
        whispers.Stop();
    }

    public void Blink()
    {
        Debug.Log("There was a blink");
    }
}
