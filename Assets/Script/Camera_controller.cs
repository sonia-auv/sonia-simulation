using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class Camera_controller : MonoBehaviour {
     
    float camSens = 10.0f; //How sensitive
    float camSensMouse = 2.0f; //How sensitive
    float mult = 1.0f; 
    private Quaternion localRotation = new Quaternion(0.0f,0.0f,0.0f,1.0f);
    public GameObject auv = null;
    public GameObject vueInverseeX = null;

    private float rotY = 0.0f;
    private float rotX = 0.0f;
    private float rotZ = 0.0f;
    private float posX = 0.0f;
    private float posY = 0.0f;
    private float posZ = 0.0f;

    private int invX = -1;

     

    private void OnEnable() {
        posX = auv.transform.position.x;
        posY = auv.transform.position.y;
        posZ = auv.transform.position.z;
        rotX = 0;
        rotY = 0;
        rotZ = auv.transform.rotation.z;
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
            auv.transform.Rotate(rotX,rotY,0.0f);

            //Lock Z axis rotation
            rotZ = auv.transform.eulerAngles.z;
            auv.transform.Rotate(0,0,-rotZ);
        }



        //Keyboard commands
        if (Input.GetKey (KeyCode.D)){
            auv.transform.Translate(new Vector3(camSens * Time.deltaTime * 0.1f * mult,0,0));
        }
        if (Input.GetKey (KeyCode.A)){
            auv.transform.Translate(new Vector3(-camSens * Time.deltaTime * 0.1f * mult,0,0));
        }
        if (Input.GetKey (KeyCode.S)){
            auv.transform.Translate(new Vector3(0,0,-camSens * Time.deltaTime * 0.1f * mult));
        }
        if (Input.GetKey (KeyCode.W)){
            auv.transform.Translate(new Vector3(0,0,camSens * Time.deltaTime * 0.1f * mult));
        }
        if (Input.GetKey (KeyCode.E)){
            auv.transform.Translate(new Vector3(0,camSens * Time.deltaTime * 0.1f * mult,0));
        }
        if (Input.GetKey (KeyCode.Q)){
            auv.transform.Translate(new Vector3(0,-camSens * Time.deltaTime * 0.1f * mult,0));
        }

    }

    public void ToggleInversionX () {
        Debug.Log("Toggle inversion X");
        if (vueInverseeX.GetComponent<Toggle>().isOn) {invX = 1;}
        else {invX = -1;}
    }

}