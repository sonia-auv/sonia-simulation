using UnityEngine;
using System.Collections;
 
public class Camera_controller : MonoBehaviour {
     
    float camSens = 10.0f; //How sensitive
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

        if(Input.GetKey(KeyCode.E)) {
            rotY += Time.deltaTime * camSens;
        }
        if(Input.GetKey(KeyCode.Q)) {
            rotY -= Time.deltaTime * camSens;
        } 
        if(Input.GetKey(KeyCode.F)) {
            rotX += Time.deltaTime * camSens;
        }
        if(Input.GetKey(KeyCode.R)) {
            rotX -= Time.deltaTime * camSens;
        }
        if(Input.GetKey(KeyCode.X)) {
            rotZ += Time.deltaTime * camSens;
        }
        if(Input.GetKey(KeyCode.C)) {
            rotZ -= Time.deltaTime * camSens;
        }
        localRotation = Quaternion.Euler(rotX,rotY, rotZ);
        transform.rotation = localRotation;
       
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