using RosMessageTypes.Geometry;
using UnityEngine;

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
        Pose auvPos = new Pose(new Point(auv.transform.position.x,auv.transform.position.y,auv.transform.position.z), 
            new Quaternion( auv.transform.rotation.x,auv.transform.rotation.y,auv.transform.rotation.z,auv.transform.rotation.w)
        );

        ros.Send(topicName, auvPos);
    }
}