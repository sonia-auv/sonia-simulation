using System.Threading;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using MultiTrajectoryPoses = RosMessageTypes.Trajectory.MultiDOFJointTrajectoryPointMsg;
using SingleWaypoint = RosMessageTypes.SoniaCommon.AddPoseMsg;
using Point = RosMessageTypes.Geometry.PointMsg;
using Orient = RosMessageTypes.Geometry.QuaternionMsg;


public class TrajectorySubscriber : MonoBehaviour
{
    public GameObject singleTarget;
    public GameObject auv;
    private string trajectoryTopicName = "/proc_planner/send_trajectory_list";
    private string singleTopicName = "/proc_control/add_pose";
    

    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<MultiTrajectoryPoses>(trajectoryTopicName,MultiTrajectory);
        ROSConnection.GetOrCreateInstance().Subscribe<SingleWaypoint>(singleTopicName,SingleTrajectory);
    } 

    void MultiTrajectory(MultiTrajectoryPoses trajectoryMessage)
    {
        // Get message Info
        int length = trajectoryMessage.transforms.Length;

        for (int i = 0; i < length; i = i+10) 
        {
            Vector3 msgPos = new Vector3((float)singleTarget.transform.position.x + (float)trajectoryMessage.transforms[i].translation.y, -(float)trajectoryMessage.transforms[i].translation.z, (float)singleTarget.transform.position.z + (float)trajectoryMessage.transforms[i].translation.x);
            Quaternion msgRot = new Quaternion(-(float)trajectoryMessage.transforms[i].rotation.y, (float)trajectoryMessage.transforms[i].rotation.z, -(float)trajectoryMessage.transforms[i].rotation.x, (float)trajectoryMessage.transforms[i].rotation.w);

            //GameObject targetClone = Instantiate(singleTarget, msgPos, singleTarget.transform.rotation * msgRot);
            GameObject targetClone = Instantiate(singleTarget, msgPos, msgRot);

            Destroy(targetClone, 5+i/10);

        }
    }

    void SingleTrajectory(SingleWaypoint waypoint)
    {

        Vector3 msgPos = new Vector3((float)0, -(float)0, (float)0);
        Quaternion msgRot = Quaternion.Euler(-(float)waypoint.orientation.y, (float)waypoint.orientation.z, -(float)waypoint.orientation.x);

        int frame = waypoint.frame;
        int time = waypoint.speed;

        if(frame == 0)
        {
            msgPos = new Vector3((float)singleTarget.transform.position.x + (float)waypoint.position.y, -(float)waypoint.position.z, (float)singleTarget.transform.position.z + (float)waypoint.position.x);
            // msgRot.x = -msgRot.x;
            // msgRot.y = -msgRot.y;
            // msgRot.z = -msgRot.z;
            msgRot = singleTarget.transform.rotation * msgRot;
        }
        else if(frame == 1)
        {
            msgPos = new Vector3((float)auv.transform.position.x - (float)waypoint.position.y, (float)auv.transform.position.y - (float)waypoint.position.z, (float)auv.transform.position.z - (float)waypoint.position.x);
            // msgRot.x = -msgRot.x;
            // msgRot.y = -msgRot.y;
            // msgRot.z = -msgRot.z;
            msgRot = auv.transform.rotation * msgRot;
        }
        else if(frame == 2)
        {
            msgPos = new Vector3((float)auv.transform.position.x + (float)waypoint.position.y, (float)auv.transform.position.y - (float)waypoint.position.z, (float)auv.transform.position.z + (float)waypoint.position.x);
            // msgRot.x = -msgRot.x;
            // msgRot.y = -msgRot.y;
            // msgRot.z = -msgRot.z;
            msgRot = singleTarget.transform.rotation * msgRot;
        }
        else if(frame == 3)
        {
            msgPos = new Vector3((float)singleTarget.transform.position.x + (float)waypoint.position.y, -(float)waypoint.position.z, (float)singleTarget.transform.position.z + (float)waypoint.position.x);
            // msgRot.x = -msgRot.x;
            // msgRot.y = -msgRot.y;
            // msgRot.z = -msgRot.z;
            msgRot = auv.transform.rotation * msgRot;
        }

        // Rotation ne fonctionne pas bien
        GameObject targetClone = Instantiate(singleTarget, msgPos, msgRot);

        Destroy(targetClone, time + 2);

    }

}
