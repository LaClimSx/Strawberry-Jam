using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextDay : MonoBehaviour
{
    // Reference to the DayManager
    private DayManager dayManager;
    public Text buttonText;

    private void Start()
    {
        // Find the DayManager in the scene (assuming it's attached to a GameObject in the scene)
        dayManager = FindObjectOfType<DayManager>();

        if (dayManager == null)
        {
            Debug.LogError("DayManager not found in the scene.");
        }
        var state = dayManager.GetCurrentState();
        Debug.Log(state);
        if (state != 4 && state != 5) buttonText.text = "Continue";
        else buttonText.text = "The End!";
    }


    public void Finish()
    {
        if (dayManager != null)
        {
            // Call the TransitionToNextState method from DayManager
            dayManager.TransitionToNextState();
        }
        Debug.Log(dayManager.GetCurrentState());
        if(dayManager.GetCurrentState() != 6) SceneManager.LoadScene("Main");
        else SceneManager.LoadScene("Credits");
    }
}
