using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static void Play()
    {
        SceneManager.LoadScene("Main");
    }

    public static void Exit()
    {
        Application.Quit();
    }
}
