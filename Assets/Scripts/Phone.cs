using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    private DayManager dayManager;
    public int counter;
    private Button button1;
    private Button button2;
    private Button button3;
    private GameObject btn1Object;
    private GameObject btn2Object;
    private GameObject btn3Object;

    // Start is called before the first frame update
    void Start()
    {
        dayManager = FindObjectOfType<DayManager>();

        var state = dayManager.GetCurrentState();

        // Disable the game object if the current state is 1
        if (state == 1 || state == 2 || state == 3)
        {
            this.gameObject.SetActive(false);
        }

        counter = 0;

        // Find buttons by their tags and assign their Button components
        btn1Object = GameObject.FindWithTag("button1");
        if (btn1Object != null)
        {
            button1 = btn1Object.GetComponent<Button>();
        }

        btn2Object = GameObject.FindWithTag("button2");
        if (btn2Object != null)
        {
            button2 = btn2Object.GetComponent<Button>();
        }

        btn3Object = GameObject.FindWithTag("button3");
        if (btn3Object != null)
        {
            button3 = btn3Object.GetComponent<Button>();
        }
    }

    public void counterPlus(Button me)
    {
        counter++;
        me.interactable = false;
        if(counter >= 2)
        {
            button1.interactable = false;
            button2.interactable = false;
            button3.interactable = false;
        }
    }
}