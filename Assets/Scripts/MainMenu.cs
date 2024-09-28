using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private DayManager dayManager;

    private void Start()
    {
        dayManager = FindObjectOfType<DayManager>();
    }

    public void Play()
    {
        SceneManager.LoadScene("Main");
        dayManager.SetCurrentState(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
