using System.Threading;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using TrueRosPos = RosMessageTypes.Geometry.PoseMsg;
using EstimatedRosPos = RosMessageTypes.Nav.OdometryMsg;

using Unity.Robotics.ROSTCPConnector.ROSGeometry;

public class PositionSubscriber : MonoBehaviour
{
    public GameObject AUV8;
    public GameObject AUV7;
    public GameObject origin;
    private GameObject auv;
    private string startSimulationTopicName = "/proc_simulation/start_simulation";
    private string trueStateTopicName = "/proc_simulation/true_states";
    private string estimatedTopicName = "/proc_nav/auv_states";

    private bool simulationMode = false ;

    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<TrueRosPos>(startSimulationTopicName,StartSimulation);
        ROSConnection.GetOrCreateInstance().Subscribe<TrueRosPos>(trueStateTopicName,TruePositionChange);
        ROSConnection.GetOrCreateInstance().Subscribe<EstimatedRosPos>(estimatedTopicName,EstimatedPositionChange);
        if (AUV7.activeInHierarchy) 
        {
            auv = AUV7;
        }
        else 
        {
            auv = AUV8;
        }
        Vector3 pose = auv.transform.position;
        pose.y = (float)0.0;
        origin.transform.position = pose;
        origin.transform.rotation = auv.transform.rotation;
    }

    void Update()
    {
        if (AUV8.activeInHierarchy)
        {
            auv = AUV8;
        }
        else if (AUV7.activeInHierarchy) 
        {
            auv = AUV7;
        }
    }

    void StartSimulation(TrueRosPos positionMessage)
    {
        simulationMode = true;
        Vector3 msgPos = new Vector3((float)positionMessage.position.y, 0.0f, (float)positionMessage.position.x);
        Quaternion msgRot = new Quaternion(-(float)0.0,(float)0.0,-(float)0.0,(float)0.0);
        origin.transform.position = msgPos;
        origin.transform.rotation = msgRot;
    }

    void TruePositionChange(TrueRosPos positionMessage)
    {
        if (simulationMode)
        {
            // Get message Info
            Vector3 msgPos = new Vector3((float)origin.transform.position.x + (float)positionMessage.position.y, -(float)positionMessage.position.z, (float)origin.transform.position.z + (float)positionMessage.position.x);
            Quaternion msgRot = new Quaternion((float)positionMessage.orientation.x,(float)positionMessage.orientation.y,(float)positionMessage.orientation.z,(float)positionMessage.orientation.w);

            auv.transform.position = msgPos;
            auv.transform.rotation = ConvertRightHandedToLeftHandedQuaternion(msgRot);
        }
    }

    void EstimatedPositionChange(EstimatedRosPos positionMessage)
    {
        if (!simulationMode)
        {
            //Get message Info
            Vector3 msgPos = new Vector3((float)origin.transform.position.x + (float)positionMessage.pose.pose.position.y, -(float)positionMessage.pose.pose.position.z, (float)origin.transform.position.z + (float)positionMessage.pose.pose.position.x);
            //Quaternion msgRot = Quaternion.Euler((float)positionMessage.pose.pose.orientation.y, -(float)positionMessage.pose.pose.orientation.z, (float)positionMessage.pose.pose.orientation.x);
            Quaternion msgRot = new Quaternion((float)positionMessage.pose.pose.orientation.x, (float)positionMessage.pose.pose.orientation.y, (float)positionMessage.pose.pose.orientation.z, (float)positionMessage.pose.pose.orientation.w);
            
            auv.transform.position = msgPos;
            auv.transform.rotation = ConvertRightHandedToLeftHandedQuaternion(msgRot);
        }
    }

    private Quaternion ConvertRightHandedToLeftHandedQuaternion (Quaternion rightHandedQuaternion)
    {
        return new Quaternion (-rightHandedQuaternion.y,
                            rightHandedQuaternion.z,
                            - rightHandedQuaternion.x,
                                rightHandedQuaternion.w);
    }
         
}
