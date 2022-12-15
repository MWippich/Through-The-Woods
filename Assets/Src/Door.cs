using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Door : MonoBehaviour
{
    [SerializeField] private Camera VRCamera;

    private AudioSource source;
    private float fadeTime;

    private Quaternion startRotation;
    private Quaternion endRotation;
    private bool activated = false;

    private void Start()
    {
        startRotation = transform.rotation;
        endRotation.eulerAngles = startRotation.eulerAngles + new Vector3(0, 0, -90);
        
        source = GetComponent<AudioSource>();
        fadeTime = source.clip.length;
        //StartCoroutine(DelayedFade());
    }

    IEnumerator DelayedFade()
    {
        yield return new WaitForSeconds(2);
        Fade();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            Fade();
        }
    }

    private void Fade()
    {
        if (!activated)
        {
            source.Play();
            VRCamera.GetComponent<SteamVR_Fade>().FadeToBlack(fadeTime);
            activated = true;
        }
        
    }

    private void Update()
    {
        if (source.isPlaying)
            Rotate();
    }

    private void Rotate()
    {
        float t = source.time / source.clip.length;

        transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
    }
}
