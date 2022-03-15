using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenManager : MonoBehaviour
{
    public GameObject toggler;

    // Start is called before the first frame update
    void Start()
    {
        //print(toggler.GetComponent<Toggle>().isOn);
        toggler.GetComponent<Toggle>().isOn = Screen.fullScreen;
    }

    public void userToggle(bool tog) 
    {
        Screen.fullScreen = tog;
        //print(tog);
    }
}