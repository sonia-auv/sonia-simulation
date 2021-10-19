using UnityEngine;
using System.Collections;
 
public class Camera_controller : MonoBehaviour {
     
    float camSens = 10.0f; //How sensitive
    float camSensMouse = 1.0f; //How sensitive
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

        float rotateHorizontal = Input.GetAxis ("Mouse X");
        float rotateVertical = Input.GetAxis ("Mouse Y");

        transform.RotateAround (auv.transform.position, Vector3.up, rotateHorizontal * camSensMouse);
        transform.RotateAround (Vector3.zero, -transform.right, rotateVertical * camSensMouse);
       
        //Keyboard commands
        if (Input.GetKey (KeyCode.D)){
            posX += Time.deltaTime * 0.1f *camSens;
        }
        if (Input.GetKey (KeyCode.A)){
            posX -= Time.deltaTime * 0.1f *camSens;
        }
        if (Input.GetKey (KeyCode.S)){
            posZ -= Time.deltaTime * 0.1f *camSens;
        }
        if (Input.GetKey (KeyCode.W)){
            posZ += Time.deltaTime * 0.1f *camSens;
        }
        if (Input.GetKey (KeyCode.LeftShift)){
            posY += Time.deltaTime * 0.1f *camSens;
        }
        if (Input.GetKey (KeyCode.LeftControl)){
            posY -= Time.deltaTime * 0.1f *camSens;
        }
        transform.position = new Vector3(posX,posY,posZ);
    }
}