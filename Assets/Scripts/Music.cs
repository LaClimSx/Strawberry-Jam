using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource sound;
    void Awake()
    {
        DontDestroyOnLoad(sound);
    }
}
