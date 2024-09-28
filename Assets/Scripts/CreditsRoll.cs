using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsRoll : MonoBehaviour
{
    public float speed = 50f; // Speed of the credit roll
    public RectTransform creditsTransform; // Reference to the RectTransform of the credits

    void Update()
    {
        // Move the credits upward by modifying its anchoredPosition
        creditsTransform.anchoredPosition += new Vector2(0, speed * Time.deltaTime);
        if (creditsTransform.anchoredPosition.y > 1280f)
        {
            Debug.Log("End");
            Application.Quit();
        }
    }
}
