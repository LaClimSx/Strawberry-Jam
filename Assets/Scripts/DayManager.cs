using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public DayState currentDayState;

    public void RecordEvent(string eventName)
    {
        currentDayState.triggeredEvents.Add(eventName);
    }

    public void RecordChoice(int choiceIndex)
    {
        currentDayState.madeChoices.Add(choiceIndex);
    }
}

