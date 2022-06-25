using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class Camera_controller : MonoBehaviour {
     
    float camSens = 10.0f; //How sensitive
    float camSensMouse = 2.0f; //How sensitive
    float mult = 1.0f; 
    private Quaternion localRotation = new Quaternion(0.0f,0.0f,0.0f,1.0f);
    public GameObject flyCam = null;
    public GameObject auv = null;
    public GameObject vueInverseeX = null;

    private float rotY = 0.0f;
    private float rotX = 0.0f;
    private float rotZ = 0.0f;

    private int invX = -1;

    private void OnEnable() {
        flyCam.transform.position = auv.transform.position;
        flyCam.transform.Translate(new Vector3(0,0,-1));

        flyCam.transform.rotation = auv.transform.rotation;
        rotZ = flyCam.transform.eulerAngles.z;
        flyCam.transform.Rotate(0,0,-rotZ);
    }

    void Update () {
        //Change speed
        if (Input.GetKey (KeyCode.LeftShift)){mult = 5.0f;}
        else {mult = 1.0f;}

        //Mouse commands
        if (Input.GetMouseButton(1)) {
            float rotateHorizontal = Input.GetAxis ("Mouse Y");
            float rotateVertical = Input.GetAxis ("Mouse X");

            rotX = rotateHorizontal * camSensMouse * mult * invX;
            rotY = rotateVertical * camSensMouse * mult;
            flyCam.transform.Rotate(rotX,rotY,0.0f);

            //Lock Z axis rotation
            rotZ = flyCam.transform.eulerAngles.z;
            flyCam.transform.Rotate(0,0,-rotZ);
        }

        //Keyboard commands
        if (Input.GetKey (KeyCode.D)){
            flyCam.transform.Translate(new Vector3(camSens * Time.deltaTime * 0.1f * mult,0,0));
        }
        if (Input.GetKey (KeyCode.A)){
            flyCam.transform.Translate(new Vector3(-camSens * Time.deltaTime * 0.1f * mult,0,0));
        }
        if (Input.GetKey (KeyCode.S)){
            flyCam.transform.Translate(new Vector3(0,0,-camSens * Time.deltaTime * 0.1f * mult));
        }
        if (Input.GetKey (KeyCode.W)){
            flyCam.transform.Translate(new Vector3(0,0,camSens * Time.deltaTime * 0.1f * mult));
        }
        if (Input.GetKey (KeyCode.E)){
            flyCam.transform.Translate(new Vector3(0,camSens * Time.deltaTime * 0.1f * mult,0));
        }
        if (Input.GetKey (KeyCode.Q)){
            flyCam.transform.Translate(new Vector3(0,-camSens * Time.deltaTime * 0.1f * mult,0));
        }

    }

    public void ToggleInversionX () {
        Debug.Log("Toggle inversion X");
        if (vueInverseeX.GetComponent<Toggle>().isOn) {invX = 1;}
        else {invX = -1;}
    }

}