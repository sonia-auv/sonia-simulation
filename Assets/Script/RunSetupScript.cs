using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSetupScript : MonoBehaviour
{
    private string runSetup = "f";

    public GameObject finale = null;
    public GameObject demiFinale = null;

    // Start is called before the first frame update
    void Start()
    {
        demiFinale.SetActive(true);
        finale.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
