using System.Threading;
using UnityEngine;
using RosPos = RosMessageTypes.Geometry.Pose;

public class PositionSubscriber : MonoBehaviour
{
    public GameObject auv;
    public string topicName = "pos_rot";

    void Start()
    {
        ROSConnection.instance.Subscribe<RosPos>(topicName,PositionChange);
    }   

    void PositionChange(RosPos positionMessage)
    {
        auv.transform.position = new Vector3((float)positionMessage.position.x, (float)positionMessage.position.y, (float)positionMessage.position.z);

        auv.transform.rotation = new Quaternion((float)positionMessage.orientation.x,(float)positionMessage.orientation.y,(float)positionMessage.orientation.z,(float)positionMessage.orientation.w);
    }
}