using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public GameObject FreeCam;
    public GameObject FlyCam;

    // Start is called before the first frame update
    void Start()
    {
            FreeCam.SetActive(true);
            FlyCam.SetActive(false);
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetButtonDown("Switch1"))
    //     {
    //         FreeCam.SetActive(true);
    //         FlyCam.SetActive(false);
    //     }

    //     if (Input.GetButtonDown("Switch4"))
    //     {
    //         FreeCam.SetActive(false);
    //         FlyCam.SetActive(true);
    //     }
        
    // }
}
