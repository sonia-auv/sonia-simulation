using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.SoniaCommon;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;


/// <summary>
/// Service that gets a submarine position and sends it as initial condition to start the simulation.
/// </summary>
public class GetInitialConditionService : MonoBehaviour
{
    [SerializeField]
    string m_ServiceName = "obj_pose_srv";

    void Start()
    {
        // register the service with ROS
        ROSConnection.GetOrCreateInstance().ImplementService<ObjectPoseServiceRequest, ObjectPoseServiceResponse>(m_ServiceName, GetObjectPose);
    }

    /// <summary>
    ///  Callback to respond to the request
    /// </summary>
    /// <param name="request">service request containing the object name</param>
    /// <returns>service response containing the object pose (or 0 if object not found)</returns>
    private ObjectPoseServiceResponse GetObjectPose(ObjectPoseServiceRequest request)
    {
        // process the service request
        Debug.Log("Received request for object: " + request.object_name);

        // prepare a response
        ObjectPoseServiceResponse objectPoseResponse = new ObjectPoseServiceResponse();
        // Find a game object with the requested name
        GameObject gameObject = GameObject.Find(request.object_name);
        if (gameObject)
        {
            // Fill-in the response with the object pose converted from Unity coordinate to ROS coordinate system
            objectPoseResponse.object_pose.position.x = gameObject.transform.position.z;
            objectPoseResponse.object_pose.position.y = gameObject.transform.position.x;
            objectPoseResponse.object_pose.position.z = -gameObject.transform.position.y;

            objectPoseResponse.object_pose.orientation.x = gameObject.transform.rotation.z;
            objectPoseResponse.object_pose.orientation.y = -gameObject.transform.rotation.x;
            objectPoseResponse.object_pose.orientation.z = gameObject.transform.rotation.y;
            objectPoseResponse.object_pose.orientation.w = gameObject.transform.rotation.w;
        }
        else
        {
            objectPoseResponse.object_pose.position.x = 0;
            objectPoseResponse.object_pose.position.y = 0;
            objectPoseResponse.object_pose.position.z = 0;

            objectPoseResponse.object_pose.orientation.x = 0;
            objectPoseResponse.object_pose.orientation.y = 0;
            objectPoseResponse.object_pose.orientation.z = 0;
            objectPoseResponse.object_pose.orientation.w = 0;
        }

        return objectPoseResponse;
    }
}