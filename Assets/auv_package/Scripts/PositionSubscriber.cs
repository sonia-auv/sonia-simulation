using System.Threading;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosPos = RosMessageTypes.Geometry.PoseMsg;

public class PositionSubscriber : MonoBehaviour
{
    public GameObject auv;
    public string topicName = "pos_rot";
    
    //private Quaternion lm2u= Quaternion.Euler(90, 0, 0); //matlab to unity linear transformation
    //private Quaternion rm2u= Quaternion.Euler(0, 0, -90); //matlab to unity angular transformation

    void Start()
    {
        //ROSConnection.instance.Subscribe<RosPos>(topicName,PositionChange);
        ROSConnection.GetOrCreateInstance().Subscribe<RosPos>(topicName,PositionChange);
    }   

    void PositionChange(RosPos positionMessage)
    {
        // Get message Info
        //Vector3 msgPos = new Vector3((float)positionMessage.position.x, (float)positionMessage.position.y, (float)positionMessage.position.z);
        //Quaternion msgRot = new Quaternion((float)positionMessage.orientation.x,(float)positionMessage.orientation.y,(float)positionMessage.orientation.z,(float)positionMessage.orientation.w);
	           
        Vector3 msgPos = new Vector3(-(float)positionMessage.position.y, (float)positionMessage.position.z, (float)positionMessage.position.x);
        Quaternion msgRot = new Quaternion((float)positionMessage.orientation.y,-(float)positionMessage.orientation.z,-(float)positionMessage.orientation.x,(float)positionMessage.orientation.w);

        // transform matlab frame to unity frame 
        //Vector3 position = lm2u*msgPos;
        //Quaternion orientation = msgRot *rm2u; // Transform the inital fbx rotation.
         

        //auv.transform.position = position;
        auv.transform.position = msgPos;
        auv.transform.rotation = msgRot;
        
        // remap the vector map to match the unity frame
        //auv.transform.rotation = new Quaternion(orientation.y,orientation.z,orientation.x,-orientation.w);
    }
         
}
