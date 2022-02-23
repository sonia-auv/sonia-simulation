using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenScript : MonoBehaviour
{
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
    public void Change() 
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("Screen mode changed.");
    }
}
