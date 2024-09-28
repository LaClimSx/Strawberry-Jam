using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextDay : MonoBehaviour
{
    // Reference to the DayManager
    private DayManager dayManager;

    private void Start()
    {
        // Find the DayManager in the scene (assuming it's attached to a GameObject in the scene)
        dayManager = FindObjectOfType<DayManager>();

        if (dayManager == null)
        {
            Debug.LogError("DayManager not found in the scene.");
        }
    }

    public void Finish()
    {
        if (dayManager != null)
        {
            // Call the TransitionToNextState method from DayManager
            dayManager.TransitionToNextState();
        }

    }
}
