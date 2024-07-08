
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
    private string customConfig = "c";

    public GameObject[] configurationLayouts = null;
    public GameObject customSceneConfig = null;
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
    public GameObject transformEditor = null;
    public GameObject transformEditorButton = null;

    private int configIndex = 0;

    void Start()
    {
        freeCam.SetActive(true);
        flyCam.SetActive(false);
        AUV8.SetActive(true);
        AUV7.SetActive(false);
        freeLookAUV8.SetActive(true);
        freeLookAUV7.SetActive(false);
        if(customSceneConfig != null) customSceneConfig.SetActive(false);
        if(transformEditor) transformEditor.SetActive(false);
        if(transformEditorButton) transformEditorButton.SetActive(false);

        DeactivateAllConfigurationLayouts();
        configurationLayouts[0].SetActive(true);
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
            if (customSceneConfig != null) customSceneConfig.SetActive(false);
            if (transformEditor) transformEditor.SetActive(false);
            if (transformEditorButton) transformEditorButton.SetActive(false);

            ToggleLayoutConfiguration();
        }

        if (customSceneConfig!= null && Input.GetKeyDown(customConfig))
        {
            DeactivateAllConfigurationLayouts();
            customSceneConfig.SetActive(true);
            if (transformEditor) transformEditor.SetActive(true);
            if (transformEditorButton) transformEditorButton.SetActive(true);
        }

            if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
            Debug.Log("toggleMenu");
        }
    }

    public void ToggleCustomConfigurationUI()
    {
        if (transformEditor)
        {
            transformEditor.SetActive(!transformEditor.activeSelf);
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

    private void ToggleLayoutConfiguration()
    {
        if (configurationLayouts.Length < 2)
            return;
        configIndex++;
        configurationLayouts[(configIndex-1) % configurationLayouts.Length].SetActive(false);
        configurationLayouts[configIndex % configurationLayouts.Length].SetActive(true);

    }

    private void DeactivateAllConfigurationLayouts()
    {
        foreach (GameObject configLayout in configurationLayouts)
        {
            configLayout.SetActive(false);
        }
    }
}
