using System.Collections;
using System.Collections.Generic;
using System;
using Unity.Robotics.ROSTCPConnector;
using tspeed = RosMessageTypes.Std.Int16MultiArrayMsg;
using UnityEngine;

public class RotateThrusters : MonoBehaviour
{
   
    public GameObject[] propellers;
    
    DateTime dt = DateTime.Now;
    DateTime dtk = DateTime.Now;
    public string topicName = "/proc_simulation/thruster_rpm";
    public short[] rpm = {0};
    
    // Start is called before the first frame update
    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<tspeed>(topicName,UpdateSpeed);
        rpm = new short[8];
    }

    // Update is called once per frame
    void Update()
    {
        
        // compute elaspe time since last update call
        dt = DateTime.Now;
        TimeSpan elpase = dt-dtk;
        float ts = (float)elpase.TotalMilliseconds/1000.0f;
        dtk =dt;
      

        // Apply propeler Transform 
       for (int i =0;i<8;++i){
            propellers[i].transform.Rotate(0.0f, 0.0f,((float)rpm[i]*360.0f/60.0f)*ts, Space.Self);
       }
    }

    void UpdateSpeed (tspeed msg){

        // Update rpm velocity
        rpm = msg.data;

    }
}