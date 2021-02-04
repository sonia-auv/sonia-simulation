using UnityEngine;
using RosPos = RosMessageTypes.AuvPackage.PosRot;

public class RosSubscriberExample : MonoBehaviour
{
    public GameObject auv;

    void Start()
    {
        ROSConnection.instance.Subscribe<RosPos>("pos_rot", PositionChange);
    }

    void PositionChange(RosPos positionMessage)
    {
        auv.transform.position = new Vector3(positionMessage.pos_x, positionMessage.pos_y, positionMessage.pos_z);

        auv.transform.rotation = new Quaternion(positionMessage.rot_x,positionMessage.rot_y,positionMessage.rot_z,positionMessage.rot_w);
    }
}