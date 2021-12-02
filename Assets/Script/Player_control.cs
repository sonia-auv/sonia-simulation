
using UnityEngine;
using System.Collections;

public class Player_control : MonoBehaviour
{
    public string toggleFront = "";
    public string toggleBottom = "";
    public string toggleFlyCam = "";

    public GameObject front = null;
    public GameObject bottom = null;
    public GameObject flyCam = null;
    public GameObject mainMenu = null;
    public GameObject optionsMenu = null;

    void Update()
    {
        if (Input.GetKeyDown(toggleFront))
        {
            ToggleFront();
            UpdateDisplay();
            Debug.Log("ToggleFront");
        }

        if (Input.GetKeyDown(toggleBottom))
        {
            ToggleBottom();
            UpdateDisplay();
            Debug.Log("ToggleBottom");
        }

        if (Input.GetKeyDown(toggleFlyCam))
        {
            ToggleFlyCam();
            Debug.Log("toggleFlyCam");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
            Debug.Log("toggleMenu");
        }
    }

    private void ToggleFront()
    {
        if (front.activeSelf)
        {
            front.SetActive(false);
        }
        else
        {
            front.SetActive(true);
        }
    }

    private void ToggleBottom()
    {
        if (bottom.activeSelf)
        {
            bottom.SetActive(false);
        }
        else
        {
            bottom.SetActive(true);
        }
    }

    private void ToggleFlyCam()
    {
        if (flyCam.activeSelf)
        {
            flyCam.SetActive(false);
        }
        else
        {
            flyCam.SetActive(true);
        }
    }

    private void ToggleMenu()
    {
        if (optionsMenu.activeSelf)
        {
            optionsMenu.SetActive(false);
        }
        mainMenu.SetActive(!mainMenu.activeSelf);

    }

    private void UpdateDisplay()
    {
        if (bottom.activeSelf && front.activeSelf)
        {
            front.GetComponent<Camera>().rect = new Rect(0.5f,0.0f,0.5f,1.0f);
            bottom.GetComponent<Camera>().rect = new Rect(0.0f,0.0f,0.5f,1.0f);
            Debug.Log("SplitScreen");
        }
        else
        {
            front.GetComponent<Camera>().rect = new Rect(0.0f,0.0f,1.0f,1.0f);
            bottom.GetComponent<Camera>().rect = new Rect(0.0f,0.0f,1.0f,1.0f);
            Debug.Log("Single Cam");
        }
    }
}