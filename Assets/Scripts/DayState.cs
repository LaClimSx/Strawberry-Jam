using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DayState", menuName = "VisualNovel/DayState")]
public class DayState : ScriptableObject
{
    public int dayNumber;
    public List<string> triggeredEvents; // Stores event names or IDs
    public List<int> madeChoices; // Store choice indices to track player decisions
}
