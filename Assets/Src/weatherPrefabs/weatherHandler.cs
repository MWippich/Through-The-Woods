using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class weatherHandler : MonoBehaviour
{

    public GameObject thunderStrike;
    public ParticleSystem rain;
    public GameObject VR_Camera;

    public float maxIntensity = 3.0f;
    public int increaseIntensityEveryXSecond = 5;
    public bool disableRain = false;
    public bool disableThunder = false;

    private float currentIntensity = 1.0f;
    private GameObject currentThunderStrike = null;
    private float thunderAliveTime = 15.0f;
    private float thunderFlashTime = 1.0f;
    private float waitTimeBeforeFlash = 0.0f;
    private float flashCounter = 0.0f;
    private bool scheduledFlash = false;

    private float rumbleCounter = 0.0f;
    private float waitTimeBeforeRumble = 0.0f;
    private bool scheduledRumble = false;

    private float counter = 0f;
    private float downpourCounter = 0f;

    private float thunderRandCounter = 0f;
   
    public void EnableWeather()
    {
        generate_lightning();
        disableRain = false;
        disableThunder = false;
    }

    // Start is called before the first frame update
    void Start() {
        if (!disableThunder)
        {
            // Starting thunder in the distance, with  flash effect:
            generate_lightning();
        }
    }

    // Instantiates a lightning bolt on the night sky:
    void generate_lightning()
    {
        currentThunderStrike = Instantiate(thunderStrike);

        // Start a flash timer here:
        flashCounter = 0.0f;
        waitTimeBeforeFlash = 0.5f / currentIntensity;
        scheduledFlash = true;

        // Start a rumble timer here:
        rumbleCounter = 0.0f;
        waitTimeBeforeRumble = 10.0f / (5.0f*currentIntensity);
        Destroy(currentThunderStrike, thunderAliveTime);
        scheduledRumble = true;
    }

    // Should randomize a lightning event based on the intensity currently at. 
    void randomize_for_lightning_strike()
    {
        // Chance for generating a thunder strike every second: 
        float chance = 0.05f;
        float randomPercent = Random.Range(0f, 1.0f);
        if(randomPercent < chance)
        {
            generate_lightning();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (disableRain && rain.isPlaying)
        {
            rain.Stop();
            rain.GetComponent<AudioSource>().enabled = false;
        }

        if (!disableRain && rain.isStopped)
        {
            rain.Play();
            rain.GetComponent<AudioSource>().enabled = true;
        }

        if (disableRain)
            return;

        counter += Time.deltaTime;
        flashCounter += Time.deltaTime;
        rumbleCounter += Time.deltaTime;

        if((int)counter >= increaseIntensityEveryXSecond)
        {
            if (currentIntensity < maxIntensity)
            {
                currentIntensity += 0.1f;
                downpourCounter += 20;
                var emission = rain.emission;
                //emission.rateOverTime = (200f + downpourCounter);
            }

            counter = 0f;
            Debug.Log("Increase Rain Intensity! Also randomize for potential new thunderstrike!");

        }

        if (currentThunderStrike == null && !disableThunder) {
            thunderRandCounter += Time.deltaTime;
            // RANDOMIZE FOR LIGHTNING HERE
            if (thunderRandCounter > 1.0f)
            {   
                thunderRandCounter = 0.0f;

                if (!disableThunder)
                    randomize_for_lightning_strike();
            }
        }
        else
        {
            //Debug.Log("Lightning ongoing!");
        }

        if(flashCounter >= waitTimeBeforeFlash && scheduledFlash)
        {
            scheduledFlash = false;
            //VR_Camera.GetComponent<SteamVR_Fade>().lightning_flash(thunderFlashTime);
        }

        if(rumbleCounter >= waitTimeBeforeRumble && scheduledRumble)
        {
            scheduledRumble = false;
            currentThunderStrike.GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown("space") && !disableThunder)
        {
            generate_lightning();
        }
    }
}
