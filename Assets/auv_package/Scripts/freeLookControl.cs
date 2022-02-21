using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
 
public class freeLookControl : MonoBehaviour {

public float scrollSpeed =1;
public float minView = 10;
public float maxView = 70; 
private CinemachineFreeLook freelook;

  void Start(){
        CinemachineCore.GetInputAxis = GetAxisCustom;
         
        freelook = GetComponentInChildren<CinemachineFreeLook>();
    }
    public float GetAxisCustom(string axisName){
        if(axisName == "Mouse X"){
            if (Input.GetMouseButton(0)){
                return UnityEngine.Input.GetAxis("Mouse X");
            } else{
                return 0;
            }
        }
        else if (axisName == "Mouse Y"){
            if (Input.GetMouseButton(0)){
                return UnityEngine.Input.GetAxis("Mouse Y");
            } else{
                return 0;
            }
        }
        return UnityEngine.Input.GetAxis(axisName);
    }

    private void Update() {

      freelook.m_Lens.FieldOfView += Input.mouseScrollDelta.y * scrollSpeed;

      if (freelook.m_Lens.FieldOfView > maxView){
          freelook.m_Lens.FieldOfView = maxView;
      }
      else if (freelook.m_Lens.FieldOfView < minView){
          freelook.m_Lens.FieldOfView = minView;
      }
    }
}
