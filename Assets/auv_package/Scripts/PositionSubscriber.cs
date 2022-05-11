using System.Threading;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using TrueRosPos = RosMessageTypes.Geometry.PoseMsg;
using EstimatedRosPos = RosMessageTypes.Nav.OdometryMsg;

public class PositionSubscriber : MonoBehaviour
{
    public GameObject auv;
    public string trueStateTopicName = "/proc_simulation/true_states";
    private string estimatedTopicName = "/telemetry/auv_states";
    
    private 

    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<TrueRosPos>(trueStateTopicName,TruePositionChange);
        ROSConnection.GetOrCreateInstance().Subscribe<EstimatedRosPos>(estimatedTopicName,EstimatedPositionChange);


    }   

    void EstimatedPositionChange(EstimatedRosPos positionMessage)
    {
        // Get message Info
        Vector3 msgPos = new Vector3((float)positionMessage.pose.pose.position.y, -(float)positionMessage.pose.pose.position.z, (float)positionMessage.pose.pose.position.x);
        Quaternion msgRot = Quaternion.Euler(-(float)positionMessage.pose.pose.orientation.y, (float)positionMessage.pose.pose.orientation.z, -(float)positionMessage.pose.pose.orientation.x);
        
        msgRot.x = -msgRot.x;
        msgRot.y = -msgRot.y;
        msgRot.z = -msgRot.z;
        
        auv.transform.position = msgPos;
        auv.transform.rotation = msgRot;
    }

    void TruePositionChange(TrueRosPos positionMessage)
    {
        // Get message Info
        Vector3 msgPos = new Vector3((float)positionMessage.position.y, -(float)positionMessage.position.z, (float)positionMessage.position.x);
        Quaternion msgRot = new Quaternion(-(float)positionMessage.orientation.y,(float)positionMessage.orientation.z,(float)positionMessage.orientation.x,(float)positionMessage.orientation.w);
        
        auv.transform.position = msgPos;
        auv.transform.rotation = msgRot;
    }
         
}
