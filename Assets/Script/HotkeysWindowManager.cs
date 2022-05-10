using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeysWindowOpener : MonoBehaviour
{
    public GameObject HotkeysModalWindow;

    // public void CloseWindow()
    // {
    //     if(HotkeysModalWindow != null)
    //     {
    //         if(HotkeysModalWindow.active)
    //         {
    //              HotkeysModalWindow.SetActive(false);
    //         }
    //         else
    //         {
    //             HotkeysModalWindow.SetActive(true);
    //         }
    //     }
    // }

    public void OpenWindow()
    {
        if (HotkeysModalWindow != null)
        {
            bool isActive = HotkeysModalWindow.activeSelf;
            HotkeysModalWindow.SetActive(!isActive);
        }
    }
}
