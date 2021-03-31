
using UnityEngine;
using System.Collections;

public class Player_control : MonoBehaviour
{
    public string toggleFront = "";
    public string toggleBottom = "";

    private GameObject front = null;
    private GameObject bottom = null;

    void Start()
    {
        front = GameObject.Find("Front");
        bottom = GameObject.Find("Bottom");
        
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleFront))
        {
            ToggleFront();
            UpdateDisplay();
            Debug.Log("ToggleFront");
        }

        if (Input.GetKeyUp(toggleBottom))
        {
            ToggleBottom();
            UpdateDisplay();
            Debug.Log("ToggleBottom");
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