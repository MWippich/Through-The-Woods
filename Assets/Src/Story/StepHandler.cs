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

    [Tooltip("Size of cube within which the player's actions have an effect")]
    [SerializeField] private Vector3 size;

    [SerializeField] private Transform player;

    private StoryHandler storyHandler;

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

        storyHandler = GetComponentInParent<StoryHandler>();

        Debug.Log("Story: " + transform.name);
    }

    private void OnDisable()
    {
        SetAll(false);
    }

    // Activate or deactivate all relevant game objects
    protected void SetAll(bool active)
    {
        foreach (GameObject go in toActivate)
        {
            go.SetActive(active);
        }
    }

    public Transform GetPlayer()
    {
        return player;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }

    public bool IsInCube()
    {
        Vector3 playerPos = player.position;
        Vector3 pos = transform.position;

        bool x = playerPos.x >= pos.x - size.x / 2 && playerPos.x <= pos.x + size.x / 2;
        bool z = playerPos.z >= pos.z - size.z / 2 && playerPos.z <= pos.z + size.z / 2;

        return x && z;
    }

    public void AdvanceStory()
    {
        storyHandler.AdvanceStory();
    }
}
