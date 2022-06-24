
using UnityEngine;
using System.Collections;

public class Player_control : MonoBehaviour
{
    //private string toggleCam = "v";
    private string displayFreeCam = "1";
    private string displayFlyCam = "2";

    public GameObject front = null;
    public GameObject bottom = null;
    public GameObject flyCam = null;
    public GameObject freeCam = null;
    public GameObject mainMenu = null;
    public GameObject optionsMenu = null;

    void Start()
    {
            freeCam.SetActive(true);
            flyCam.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(displayFreeCam))
        {
            freeCam.SetActive(true);
            flyCam.SetActive(false);
            Debug.Log("Display Free Cam");
        }

        if (Input.GetKeyDown(displayFlyCam))
        {
            freeCam.SetActive(false);
            flyCam.SetActive(true);
            Debug.Log("Display Fly Cam");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
            Debug.Log("toggleMenu");
        }
    }

    // private void ToggleFlyCam()
    // {
    //     if (flyCam.activeSelf)
    //     {
    //         flyCam.SetActive(false);
    //     }
    //     else
    //     {
    //         flyCam.SetActive(true);
    //     }
    // }

    private void ToggleMenu()
    {
        if (optionsMenu.activeSelf)
        {
            optionsMenu.SetActive(false);
        }
        mainMenu.SetActive(!mainMenu.activeSelf);
    }
}
