using UnityEngine;
using RosMessageTypes.Geometry;
using System.Threading;
using System.Collections;
// Singleton Class

public class InitialConditionPublisher : MonoBehaviour
{
   

    

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
        ros = ROSConnection.instance;
        publishInitialCondition();
    }

    public void publishInitialCondition()
    {
        
        UnityEngine.Quaternion lm2u= new UnityEngine.Quaternion(0.7071f,0.0f,0.0f,0.7071f);  //.Euler(-90, 0, 0) unity to matlab  linear transformation (unity to NED frame)
        UnityEngine.Quaternion rm2u= new UnityEngine.Quaternion(0.0f,0.7071f,0.0f,-0.7071f);  //Euler(0, 0, 90)  unity to angular transformation (initial rotation of the fbx)
        // Get AUV pose from unity
        UnityEngine.Vector3 msgPos = new UnityEngine.Vector3(auv.transform.position.x, auv.transform.position.y, auv.transform.position.z);
        UnityEngine.Quaternion msgRot = new UnityEngine.Quaternion(auv.transform.rotation.x,auv.transform.rotation.y,auv.transform.rotation.z,auv.transform.rotation.w);

        // transform matlab frame to unity frame 
        UnityEngine.Vector3 pos = (lm2u*msgPos); // Transform position
        UnityEngine.Quaternion ori = msgRot * rm2u ; // Transform the inital fbx rotation.
        Debug.Log(auv.transform.position.y);
        Debug.Log(ori);
        Debug.Log(pos);
        // remap the quaternion to  map to match the the NED frame
        Pose auvPos = new Pose(new Point(pos.x, -pos.y, -pos.z), 
            new Quaternion( ori.y, ori.z, ori.x, - ori.w)
        );

        ros.Send(topicName, auvPos);
    }
}