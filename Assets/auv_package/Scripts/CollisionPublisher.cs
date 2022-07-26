using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using RosMessageTypes.Std;
using RosMessageTypes.Nav;
using Unity.Robotics.ROSTCPConnector;


public class CollisionPublisher : MonoBehaviour
{
    ROSConnection ros;
    private UInt32 sequence;

    private string topicName = "/proc_control/measurmment_residual";

    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<OdometryMsg>(topicName);

        sequence = 0;
    }



    void OnCollisionEnter(UnityEngine.Collision hit)
    {
        Publish();
        Debug.Log("Another object has entered the collision:" + hit.gameObject ); 
    }
     void Update () 
    {
    }

    private void Publish()
    {
        OdometryMsg msg = new OdometryMsg();
        msg.header.seq = sequence;
        msg.header.frame_id = "BODY";
        
        msg.child_frame_id =  "ENU";

        msg.twist.twist.angular.y = 1000;

        ros.Send(topicName, msg);
    }
}