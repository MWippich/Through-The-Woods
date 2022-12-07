using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * This class handles a single story step
 * Add a second script to this game object that handles specific functions (e.g. opens the gate)
 * You can invoke functions in that class from the inspector, using the UnityEvents below
 * If a game object should be activated only during this step, put it in the toActivate list (from inspector)
 */

public class StepHandler : MonoBehaviour, IGazeSensitve
{
    [Tooltip("All objects that should be activated as part of this story step")]
    [SerializeField] List<GameObject> toActivate;

    [Header("Events")]
    [SerializeField] UnityEvent onEnable;
    [SerializeField] UnityEvent onBlink;
    [SerializeField] UnityEvent onBlinkEnd;
    [SerializeField] UnityEvent onLongBlink;
    [SerializeField] UnityEvent onLongBlinkEnd;

    private void OnEnable()
    {
        SetAll(true);
        onEnable.Invoke();
    }

    private void OnDisable()
    {
        SetAll(false);
    }

    // Activate or deactivate all relevant game objects
    private void SetAll(bool active)
    {
        foreach (GameObject go in toActivate)
        {
            go.SetActive(active);
        }
    }

    // Activate something on a long blink
    public void OnLongBlinkStart(float dist)
    {
        onLongBlink.Invoke();
    }

    public void OnLongBlinkEnd(float dist, float time)
    {
        onLongBlinkEnd.Invoke();
    }

    // Activate something on any blink
    public void OnBlinkStart(float dist)
    {
        onBlink.Invoke();
    }
}
