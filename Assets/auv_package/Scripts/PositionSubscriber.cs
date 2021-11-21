using System.Threading;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosPos = RosMessageTypes.Geometry.PoseMsg;

public class PositionSubscriber : MonoBehaviour
{
    public GameObject auv;
    public string topicName = "pos_rot";
    

    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<RosPos>(topicName,PositionChange);
    }   

    void PositionChange(RosPos positionMessage)
    {
        // Get message Info
        Vector3 msgPos = new Vector3((float)positionMessage.position.y, -(float)positionMessage.position.z, (float)positionMessage.position.x);
        Quaternion msgRot = new Quaternion((float)positionMessage.orientation.y,-(float)positionMessage.orientation.z,-(float)positionMessage.orientation.x,(float)positionMessage.orientation.w);

        auv.transform.position = msgPos;
        auv.transform.rotation = msgRot;
        
    }
         
}
