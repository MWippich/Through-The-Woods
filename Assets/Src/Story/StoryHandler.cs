using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This class handles the game's story
 * Every step is defined by creating a game object as a child of this object (name is irrelevant)
 * That object must have a StepHandler script attached
 * When you wish to advance the story, simply call the AdvanceStory() function from anywhere
 */

public class StoryHandler : MonoBehaviour
{
    [Tooltip("All steps in story, come from children")]
    [SerializeField] private StepHandler[] story;
    [Tooltip("Set to one lower than the step you want to be on, default -1")]
    [SerializeField] private int currentStep = -1;

    private void Start()
    {
        story = transform.GetComponentsInChildren<StepHandler>();

        foreach (StepHandler step in story)
        {
            step.enabled = false;
        }

        AdvanceStory();

        
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reset");
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        */
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Deactivate current step, activate next step
    public void AdvanceStory()
    {
        if (currentStep == story.Length - 1)
            return;

        if (currentStep >= 0)
            story[currentStep].enabled = false;

        currentStep++;

        story[currentStep].enabled = true;
        
    }
}
