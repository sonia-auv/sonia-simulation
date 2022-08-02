
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_control : MonoBehaviour
{
    private string displayFreeCam = "1";
    private string displayFlyCam = "2";
    private string displayAUV7 = "7";
    private string displayAUV8 = "8";
    private string hideCovers = "b";
    private string runSetup = "f";

    public GameObject finale = null;
    public GameObject demiFinale = null;
    public GameObject front = null;
    public GameObject bottom = null;
    public GameObject flyCam = null;
    public GameObject freeCam = null;
    public GameObject freeLookAUV7 = null;
    public GameObject freeLookAUV8 = null;
    public GameObject AUV7 = null;
    public GameObject AUV8 = null;
    public GameObject mainMenu = null;
    public GameObject optionsMenu = null;

    void Start()
    {
            freeCam.SetActive(true);
            flyCam.SetActive(false);
            AUV8.SetActive(true);
            AUV7.SetActive(false);
            freeLookAUV8.SetActive(true);
            freeLookAUV7.SetActive(false);
            demiFinale.SetActive(false);
            finale.SetActive(true);
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

        if (Input.GetKeyDown(displayAUV7))
        {
            AUV7.SetActive(true);
            freeLookAUV7.SetActive(true);
            AUV8.SetActive(false);
            freeLookAUV8.SetActive(false);
            Debug.Log("Display AUV7");
        }

        if (Input.GetKeyDown(displayAUV8))
        {
            AUV7.SetActive(false);
            freeLookAUV7.SetActive(false);
            AUV8.SetActive(true);
            freeLookAUV8.SetActive(true);
            Debug.Log("Display AUV8");
        }

        if (Input.GetKeyDown(hideCovers))
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Hideable"))
            {
                int LayerInvisible = LayerMask.NameToLayer("Invisible");
                int LayerObstacle = LayerMask.NameToLayer("Obstacle");
                if (item.layer == LayerInvisible)
                {
                    item.layer = LayerObstacle;
                    ChangeChildLayer(item.transform, LayerObstacle);
                }
                else if (item.layer == LayerObstacle)
                {
                    item.layer = LayerInvisible;
                    ChangeChildLayer(item.transform, LayerInvisible);
                }
            }
        }

        if (Input.GetKeyDown(runSetup))
        {
            if (finale.activeSelf)
            {
                finale.SetActive(false);
                demiFinale.SetActive(true);
            }
            else
            {
                finale.SetActive(true);
                demiFinale.SetActive(false);
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
            Debug.Log("toggleMenu");
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

    private void ChangeChildLayer(Transform parent, int newLayer)
    {
        parent.gameObject.layer = newLayer;
        for (int i = 0, count = parent.childCount; i < count; i++)
        {
            ChangeChildLayer(parent.GetChild(i), newLayer);
        }
    }
}
