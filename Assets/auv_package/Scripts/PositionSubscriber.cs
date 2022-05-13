using System.Threading;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using TrueRosPos = RosMessageTypes.Geometry.PoseMsg;
using EstimatedRosPos = RosMessageTypes.Nav.OdometryMsg;

public class PositionSubscriber : MonoBehaviour
{
    public GameObject auv;
    public GameObject origin;
    private string startSimulationTopicName = "/proc_simulation/start_simulation";
    private string trueStateTopicName = "/proc_simulation/true_states";
    private string estimatedTopicName = "/telemetry/auv_states";
    
    //private Vector3 initPose;
    //private Quaternion initOrientation;
    private bool simulationMode = false ;

    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<TrueRosPos>(startSimulationTopicName,StartSimulation);
        ROSConnection.GetOrCreateInstance().Subscribe<TrueRosPos>(trueStateTopicName,TruePositionChange);
        ROSConnection.GetOrCreateInstance().Subscribe<EstimatedRosPos>(estimatedTopicName,EstimatedPositionChange);
        Vector3 pose = auv.transform.position;
        pose.y = (float)0.0;
        origin.transform.position = pose;
        origin.transform.rotation = auv.transform.rotation;

    }   

    void StartSimulation(TrueRosPos positionMessage)
    {
        // Get message Info
        //initPose = new Vector3((float)positionMessage.position.y, -(float)positionMessage.position.z, (float)positionMessage.position.x);
        //initOrientation = new Quaternion(-(float)positionMessage.orientation.y,(float)positionMessage.orientation.z,(float)positionMessage.orientation.x,(float)positionMessage.orientation.w);

        simulationMode = true;
        // Debug.Log(initPose);
        // Debug.Log(initOrientation);
    }

    void TruePositionChange(TrueRosPos positionMessage)
    {
        if (simulationMode)
        {
            // Get message Info
            Vector3 msgPos = new Vector3((float)positionMessage.position.y, -(float)positionMessage.position.z, (float)positionMessage.position.x);
            Quaternion msgRot = new Quaternion(-(float)positionMessage.orientation.y,(float)positionMessage.orientation.z,(float)positionMessage.orientation.x,(float)positionMessage.orientation.w);
            
            auv.transform.position = msgPos;
            // msgRot.x = -msgRot.x;
            // msgRot.y = -msgRot.y;
            // msgRot.z = -msgRot.z;
            auv.transform.rotation = msgRot;
        }
    }

    void EstimatedPositionChange(EstimatedRosPos positionMessage)
    {
        if (!simulationMode)
        {
            // Get message Info
            Vector3 msgPos = new Vector3((float)origin.transform.position.x + (float)positionMessage.pose.pose.position.y, -(float)positionMessage.pose.pose.position.z, (float)origin.transform.position.z + (float)positionMessage.pose.pose.position.x);
            Quaternion msgRot = Quaternion.Euler(-(float)positionMessage.pose.pose.orientation.y, (float)positionMessage.pose.pose.orientation.z, -(float)positionMessage.pose.pose.orientation.x);
            
            // msgRot.x = -msgRot.x;
            // msgRot.y = -msgRot.y;
            // msgRot.z = -msgRot.z;
            
            //auv.transform.position = initPose + msgPos;
            //auv.transform.rotation = initOrientation * msgRot;

            auv.transform.position = msgPos;
            auv.transform.rotation = origin.transform.rotation * msgRot;
        }
    }
         
}
