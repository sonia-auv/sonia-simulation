using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject DropdownQuality = null;

    private void OnEnable()
    {
        // TODO : Actualiser DropDown avec la qualité actuelle
    }
    public void SetQuality(int qualityIndex) 
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
