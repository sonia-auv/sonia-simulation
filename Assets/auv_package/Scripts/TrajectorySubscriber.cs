using System.Threading;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using TrajectoryPoses = RosMessageTypes.Trajectory.MultiDOFJointTrajectoryPointMsg;

public class TrajectorySubscriber : MonoBehaviour
{
    public GameObject target;
    public string topicName = "/proc_planner/send_trajectory_list";
    

    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<TrajectoryPoses>(topicName,PositionChange);
    } 

    void PositionChange(TrajectoryPoses positionMessage)
    {
        // Get message Info
        Vector3 msgPos = new Vector3((float)positionMessage.transforms[0].translation.y, -(float)positionMessage.transforms[0].translation.z, (float)positionMessage.transforms[0].translation.x);
        Quaternion msgRot = new Quaternion((float)positionMessage.transforms[0].rotation.y,-(float)positionMessage.transforms[0].rotation.z,-(float)positionMessage.transforms[0].rotation.x,(float)positionMessage.transforms[0].rotation.w);

        target.transform.position = msgPos;
        target.transform.rotation = msgRot;
        
    }

         
}
