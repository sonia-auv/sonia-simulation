using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Dropdown DropdownTarget;
    public TMP_Dropdown DropdownScene;




    public void StartGame()
    {
        SceneManager.LoadScene(DropdownScene.value + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        GameObject.Find("MainMenu").SetActive(false);
    }
}
