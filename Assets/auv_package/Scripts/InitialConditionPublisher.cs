using RosMessageTypes.AuvPackage;
using UnityEngine;

public class InitialConditionPublisher : MonoBehaviour
{
    ROSConnection ros;
    public string topicName = "initial_condition";

    public GameObject auv;

    void Start()
    {
        // start the ROS connection
        ros = ROSConnection.instance;
        PublishCI();
    }

    private void PublishCI()
    {
        PosRot auvPos = new PosRot(
            auv.transform.position.x,
            auv.transform.position.y,
            auv.transform.position.z,
            auv.transform.rotation.x,
            auv.transform.rotation.y,
            auv.transform.rotation.z,
            auv.transform.rotation.w
        );

        ros.Send(topicName, auvPos);
    }
}