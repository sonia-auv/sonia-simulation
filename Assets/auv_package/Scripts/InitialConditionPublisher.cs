using UnityEngine;
using RosMessageTypes.Geometry;
using System.Threading;
using System.Collections;
using Unity.Robotics.ROSTCPConnector;

// Singleton Class

public class InitialConditionPublisher : MonoBehaviour
{
   

    private UnityEngine.Quaternion lm2u= new UnityEngine.Quaternion(0.7071f,0.0f,0.0f,-0.7071f);  // Euler(-90, 0, 0) unity to matlab  linear transformation (unity to NED frame)
    private UnityEngine.Quaternion rm2u= new UnityEngine.Quaternion(0.0f,0.7071f,0.0f,-0.7071f);  // Euler(0, 0, 90)  unity to angular transformation (initial rotation of the fbx)

    private static InitialConditionPublisher _instance;
    public static InitialConditionPublisher Instance 
    
    { 
        get { return _instance; } 
    } 

    private void Awake() 
    { 
        if (_instance != null && _instance != this) 
        { 
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    } 

    ROSConnection ros;
    public string topicName = "initial_condition";

    public GameObject auv;

    void Start()
    {
        // start the ROS connection
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<PoseMsg>(topicName);
        publishInitialCondition();
    }

    public void publishInitialCondition()
    {
        // Get AUV pose from unity
        UnityEngine.Vector3 msgPos = new UnityEngine.Vector3(auv.transform.position.x, auv.transform.position.y, auv.transform.position.z);
        UnityEngine.Quaternion msgRot = new UnityEngine.Quaternion(auv.transform.rotation.x,auv.transform.rotation.y,auv.transform.rotation.z,auv.transform.rotation.w);

        // transform matlab frame to unity frame 
        UnityEngine.Vector3 pos = (lm2u*msgPos); // Transform position
        UnityEngine.Quaternion ori = msgRot * rm2u ; // Transform the inital fbx rotation.
      
        // remap the quaternion to map to match the NED frame
        PoseMsg auvPos = new PoseMsg(new PointMsg(pos.x, pos.y, pos.z), 
            new QuaternionMsg( ori.y, ori.z, ori.x, - ori.w)
        );

        ros.Send(topicName, auvPos);
    }
}