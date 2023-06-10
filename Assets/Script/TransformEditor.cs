using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TransformEditor : MonoBehaviour
{
    [SerializeField] GameObject[] obstacles;
    [SerializeField] TMP_Dropdown obstacleDropdow;
    [SerializeField] Toggle toggleButton;
    [SerializeField] List<string> obstacleNames;

    [SerializeField] TMP_InputField posX;
    [SerializeField] TMP_InputField posY;
    [SerializeField] TMP_InputField posZ;
    
    [SerializeField] TMP_InputField rotX;
    [SerializeField] TMP_InputField rotY;
    [SerializeField] TMP_InputField rotZ;

    GameObject selectedObstacle;


    void Start()
    {        
        foreach (GameObject obstacle in obstacles)
        {
            obstacleNames.Add(obstacle.name);
        }
        
        obstacleDropdow.AddOptions(obstacleNames);
    }

    public void SetSelectedObstacle(TMP_Dropdown dropdown)
    {
        Debug.Log("Drodown value: " + dropdown.options[dropdown.value].text);
        selectedObstacle = obstacles[dropdown.value - 1];

        if (selectedObstacle != null)
        {
            posX.text = selectedObstacle.transform.position.x.ToString();
            posY.text = selectedObstacle.transform.position.y.ToString();
            posZ.text = selectedObstacle.transform.position.z.ToString();
            
            rotX.text = selectedObstacle.transform.eulerAngles.x.ToString();
            rotY.text = selectedObstacle.transform.eulerAngles.y.ToString();
            rotZ.text = selectedObstacle.transform.eulerAngles.z.ToString();

            toggleButton.isOn = selectedObstacle.activeSelf;
        }
    }


    public void SetObstacleTransformValues()
    {
        selectedObstacle.transform.position = new Vector3(float.Parse(posX.text), float.Parse(posY.text), float.Parse(posZ.text));
        selectedObstacle.transform.eulerAngles = new Vector3(float.Parse(rotX.text), float.Parse(rotY.text), float.Parse(rotZ.text));
    }

    public void ToggleObstacleActive()
    {
        if(selectedObstacle != null)
        {
            selectedObstacle.SetActive(toggleButton.isOn);
        }
    }
}
