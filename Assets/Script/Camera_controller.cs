using UnityEngine;
using System.Collections;
 
public class Camera_controller : MonoBehaviour {
     
    float camSens = 10.0f; //How sensitive
    float camSensMouse = 2.0f; //How sensitive
    float mult = 1.0f; 
    private Quaternion localRotation = new Quaternion(0.0f,0.0f,0.0f,1.0f);
    public GameObject auv = null;

    private float rotY = 0.0f;
    private float rotX = 0.0f;
    private float rotZ = 0.0f;
    private float posX = 0.0f;
    private float posY = 0.0f;
    private float posZ = 0.0f;

     

    private void OnEnable() {
        posX = auv.transform.position.x;
        posY = auv.transform.position.y;
        posZ = auv.transform.position.z;
        rotX = auv.transform.rotation.x;
        rotY = auv.transform.rotation.y;
        rotZ = auv.transform.rotation.z;
    }
    void Update () {
        //Change speed
        if (Input.GetKey (KeyCode.LeftShift)){mult = 5.0f;}
        else {mult = 1.0f;}

        //Mouse commands

        if (Input.GetMouseButton(1)) {
            float rotateHorizontal = Input.GetAxis ("Mouse X");
            float rotateVertical = Input.GetAxis ("Mouse Y");
            //transform.RotateAround (auv.transform.position, Vector3.up, rotateHorizontal * camSensMouse * mult);
            //transform.RotateAround (Vector3.zero, -transform.right, rotateVertical * camSensMouse * mult);

            //auv.transform.Rotate(Vector3.up * -rotateVertical * camSensMouse * mult);
            //auv.transform.Rotate(transform.right * rotateHorizontal * camSensMouse * mult);

            //auv.transform.Rotate(-rotateVertical * camSensMouse * mult,rotateHorizontal * camSensMouse * mult,0.0f);

            rotZ = rotateHorizontal * camSensMouse * mult;
            rotY = rotateVertical * camSensMouse * mult;
        }
        //localRotation = Quaternion.Euler(0.0f,rotY,rotZ);
        //transform.rotation = localRotation;

        auv.transform.Rotate(rotY,rotZ,0.0f);

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
}