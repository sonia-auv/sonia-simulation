using System.Threading;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using MultiTrajectoryPoses = RosMessageTypes.Trajectory.MultiDOFJointTrajectoryPointMsg;
using TrajectoryReset = RosMessageTypes.Std.BoolMsg;
using SingleWaypoint = RosMessageTypes.SoniaCommon.AddPoseMsg;
using InitialPose = RosMessageTypes.Geometry.PoseMsg; 
using Point = RosMessageTypes.Geometry.PointMsg;
using Orient = RosMessageTypes.Geometry.QuaternionMsg;


public class TrajectorySubscriber : MonoBehaviour
{
    public GameObject singleTarget;
    public GameObject auv;
    private string trajectoryTopicName = "/proc_planner/send_trajectory_list";
    private string resetTopicName = "/proc_planner/reset_trajectory";
    private string singleTopicName = "/proc_control/add_pose";
    private string initialConditionTopicName = "/proc_simulation/start_simulation";
    private InitialPose initialPose;
    

    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<MultiTrajectoryPoses>(trajectoryTopicName,MultiTrajectory);
        ROSConnection.GetOrCreateInstance().Subscribe<TrajectoryReset>(resetTopicName,ResetTrajectory);
        ROSConnection.GetOrCreateInstance().Subscribe<SingleWaypoint>(singleTopicName,SingleTrajectory);
        ROSConnection.GetOrCreateInstance().Subscribe<InitialPose>(initialConditionTopicName,GetInitialPose);
        initialPose = new InitialPose(new Point(0,0,0),new Orient(0,0,0,0));

    } 

    void GetInitialPose(InitialPose pose)
    {
        initialPose = pose;
    }

    void MultiTrajectory(MultiTrajectoryPoses trajectoryMessage)
    {
        // Get message Info
        int length = trajectoryMessage.transforms.Length;

        for (int i = 0; i <= length; i = i+10) 
        {
            //Vector3 msgPos = new Vector3((float)initialPose.position.y + (float)trajectoryMessage.transforms[i].translation.y, -(float)trajectoryMessage.transforms[i].translation.z, (float)initialPose.position.x + (float)trajectoryMessage.transforms[i].translation.x);
            Vector3 msgPos = new Vector3((float)trajectoryMessage.transforms[i].translation.y, -(float)trajectoryMessage.transforms[i].translation.z, (float)trajectoryMessage.transforms[i].translation.x);
            Quaternion msgRot = new Quaternion(-(float)trajectoryMessage.transforms[i].rotation.y, (float)trajectoryMessage.transforms[i].rotation.z, (float)trajectoryMessage.transforms[i].rotation.x, (float)trajectoryMessage.transforms[i].rotation.w);

            GameObject targetClone = Instantiate(singleTarget, msgPos, msgRot);

            Debug.Log(msgPos);

            Destroy(targetClone, 5+i/10);
        }
        
    }

    void SingleTrajectory(SingleWaypoint waypoint)
    {

        Vector3 msgPos = new Vector3((float)waypoint.position.y, -(float)waypoint.position.z, (float)waypoint.position.x);
        Quaternion msgRot = Quaternion.Euler(-(float)waypoint.orientation.y, (float)waypoint.orientation.z, -(float)waypoint.orientation.x);

        int frame = waypoint.frame;

        if(frame == 0)
        {
            singleTarget.transform.position = msgPos;
            singleTarget.transform.rotation = msgRot;
        }
        else if(frame == 1)
        {
            singleTarget.transform.position = auv.transform.position + msgPos;
            // msgRot.x = -msgRot.x;
            // msgRot.y = -msgRot.y;
            // msgRot.z = -msgRot.z;
            singleTarget.transform.rotation = auv.transform.rotation * msgRot;
        }
        else if(frame == 2)
        {
            singleTarget.transform.position = auv.transform.position + msgPos;
            singleTarget.transform.rotation = msgRot;
        }
        else if(frame == 3)
        {
            singleTarget.transform.position = msgPos;
            // msgRot.x = -msgRot.x;
            // msgRot.y = -msgRot.y;
            // msgRot.z = -msgRot.z;
            singleTarget.transform.rotation = auv.transform.rotation * msgRot;
        }
    }


    void ResetTrajectory(TrajectoryReset trajectoryReset)
    {
        if(trajectoryReset.data)
        {
            TrajectoryDone();
        }
    }

    void TrajectoryDone()
    {

    }
}
