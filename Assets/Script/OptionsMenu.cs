using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdownQuality = null;

    private void OnEnable()
    {
        dropdownQuality.value = QualitySettings.GetQualityLevel();
    }
    public void SetQuality(int qualityIndex) 
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
