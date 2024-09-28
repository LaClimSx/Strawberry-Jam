using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject logo;
    public GameObject button1;
    public GameObject button2;
    public GameObject logoSmall;
    public GameObject text;
    public GameObject start;
    public void Play()
    {
        DayManager.initGame();
    }
    public void Start()
    {
        logoSmall.SetActive(false);
        text.SetActive(false);
        start.SetActive(false);
    }

    public void step()
    {
        logo.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(false);
        logoSmall.SetActive(true);
        text.SetActive(true);
        start.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
