using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Dropdown dropdownTarget;
    public TMP_Dropdown dropdownScene;
    public GameObject mainMenu;

    public void StartGame()
    {
        SceneManager.LoadScene(dropdownScene.value + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(dropdownScene.value + 1);
        mainMenu.SetActive(false);
    }
}
