using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shed : StepHandler
{
    [SerializeField] private Transform leftDoor;
    [SerializeField] private Transform rightDoor;

    [SerializeField] private weatherHandler weatherHandler;
    [SerializeField] private Light directional;

    [SerializeField] private Transform statue;

    private Transform statuePos;

    private AudioSource audioSource;

    private Quaternion leftClosed, rightClosed, rightOpen, leftOpen;

    private bool movedStatue = false;
    
    private void OnEnable()
    {
        CustomOnEnable();
        audioSource = GetComponent<AudioSource>();
        OpenDoors();
    }

    private void Start()
    {
        leftClosed = leftDoor.rotation;
        rightClosed = rightDoor.rotation;

        leftOpen.eulerAngles = leftClosed.eulerAngles + new Vector3(0, -90, 0);
        rightOpen.eulerAngles = leftClosed.eulerAngles + new Vector3(0, 90, 0);

        statuePos = transform.GetChild(0);
    }

    private void Update()
    {
        if (audioSource.isPlaying)
            Rotate();

        if (!movedStatue && IsInCube())
        {
            statue.position = new Vector3(statuePos.position.x, statue.position.y, statuePos.position.z);
            statue.rotation = statuePos.rotation;
            movedStatue = true;
        }
    }

    private void Rotate()
    {
        float t = audioSource.time / audioSource.clip.length;

        leftDoor.rotation = Quaternion.Slerp(leftClosed, leftOpen, t);
        rightDoor.rotation = Quaternion.Slerp(rightClosed, rightOpen, t);
    }

    private void OpenDoors()
    {
        audioSource.Play();
        weatherHandler.EnableWeather();
        RenderSettings.ambientLight = new Color(0.1f, 0.1f, 0.1f);
        RenderSettings.ambientIntensity = 0;

        directional.intensity = 0.0f;
    }
}
