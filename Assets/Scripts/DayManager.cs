using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class DayManager : MonoBehaviour
{
    public DayState currentDayState;
    
    public GameObject pagePrefab;
    public GameObject messagePrefab;
    
    public GameObject pagesHolder;
    public GameObject messageHolder;

    public GameObject button1;
    public Button button2;
    public Button button3;

    public bool mainIsLoading;

    // Dictionary to hold the state transition maps for each day
    private Dictionary<int, Dictionary<int, int>> dayTransitionMap = new Dictionary<int, Dictionary<int, int>>();

    private Dictionary<int, string[]> dialogues = new Dictionary<int, string[]>
        {
            { 1, new [] {"Hello","World" } },  // State 1 -> State 2
            { 2, new [] {"Hello","World" } }   // State 2 -> State 3
        };
    private Dictionary<int, string> debriefs = new Dictionary<int, string>
        {
            { 1, "Hello" },  // State 1 -> State 2
            { 2, "World" }   // State 2 -> State 3
        };

    private Dictionary<int, string[]> folders = new Dictionary<int, string[]>
        {
            { 1, new [] {"Hello"} },  // State 1 -> State 2
            { 2, new [] {"World"} }   // State 2 -> State 3
        };

    private void Start()
    {
        
        
        var state1Transitions = new Dictionary<int, int>
        {
            { 0, 2 },  // State 1 -> State 2
            { 1, 3 }   // State 1 -> State 3
        };

        var state2Transitions = new Dictionary<int, int>
        {
            { 0, 4 },  // State 2 -> State 4
            { 1, 5 }   // State 2 -> State 5
        };

        var state3Transitions = new Dictionary<int, int>
        {
            { 0, 4 },  // State 3 -> State 4
            { 1, 5 }   // State 3 -> State 5
        };

        var state4Transitions = new Dictionary<int, int>
        {
            { 0, 6 },  // State 2 -> End
            { 1, 6 }   // State 2 -> End
        };

        var state5Transitions = new Dictionary<int, int>
        {
            { 0, 6 },  // State 2 -> End
            { 1, 6 }   // State 2 -> End
        };

        // Add these dictionaries to the main dayTransitionMap
        dayTransitionMap.Add(1, state1Transitions);
        dayTransitionMap.Add(2, state2Transitions);
        dayTransitionMap.Add(3, state3Transitions);
        dayTransitionMap.Add(4, state4Transitions);
        dayTransitionMap.Add(5, state5Transitions);     

        // You can add more day-specific transition maps as needed...
    }

    // Method to get the current state
    public int GetCurrentState()
    {
        return currentDayState.state;
    }

    

    // Method to set the current state
    public void SetCurrentState(int newState)
    {
        currentDayState.state = newState;
        Debug.Log("State updated to: " + newState);
    }

    // Method to transition to the next state based on the current day and state
    public void TransitionToNextState()
    {
        int currentState = GetCurrentState();
        int currentChoice = currentDayState.choice;

        if (dayTransitionMap.ContainsKey(currentState))
        {
            Dictionary<int, int> stateTransitionMap = dayTransitionMap[currentState];

            if (stateTransitionMap.ContainsKey(currentChoice))
            {
                int nextState = stateTransitionMap[currentChoice];
                SetCurrentState(nextState);
                Debug.Log("Transitioned to next state: " + nextState);
            }
            else
            {
                Debug.LogWarning("No transition found for current state: " + currentState + " on Day " + currentChoice);
            }
        }
        
        // Load the Main scene after transitioning to the next state
        SceneManager.LoadScene("Main");

        mainIsLoading = true;


    }


    private void Update()
    {
        if (mainIsLoading && SceneManager.GetActiveScene().isLoaded)
        {
            pagesHolder = GameObject.FindWithTag("PageHolder");
            messageHolder = GameObject.FindWithTag("MessageHolder");

            mainIsLoading = false;
        }
    }

    public void onButtonClick(int button)
    {
        foreach (string s in getDialog(button))
        {
            
        }
    }

    public string[] getDialog(int button)
    {
        int show = 10*GetCurrentState() + button;
        return dialogues[show];
    }


    public string getDebrief()
    {
        int show = 10 * GetCurrentState() + currentDayState.choice;
        return debriefs[show];
    }

    public string[] getFolder()
    {
        int show = GetCurrentState();
        return folders[show];
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}