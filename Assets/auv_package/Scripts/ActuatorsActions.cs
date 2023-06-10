using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.SoniaCommon;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;


/// <summary>
/// Service that gets a submarine position and sends it as initial condition to start the simulation.
/// </summary>
public class ActuatorsActions : MonoBehaviour
{
    [SerializeField]
    string m_ServiceName = "/sonia_common/ActuatorDoActionSrv";

    // public GameObject droppers AUV8/AUV7
    // public GameObject torpedoes AUV8/AUV7
    
    public GameObject auv7 = null;
    public GameObject auv8 = null;

    void Start()
    {
        // register the service with ROS
        ROSConnection.GetOrCreateInstance().ImplementService<ActuatorDoActionSrvRequest, ActuatorDoActionSrvResponse>(m_ServiceName, SetActuatorsActions);
    }

    /// <summary>
    ///  Callback to respond to the request
    /// </summary>
    /// <param name="request">service request containing the object name</param>
    /// <returns>service response containing the object pose (or 0 if object not found)</returns>
    private ActuatorDoActionSrvResponse SetActuatorsActions(ActuatorDoActionSrvRequest request)
    {
        // process the service request
        Debug.Log("Received request to do an actuator action.");

        // prepare a response
        ActuatorDoActionSrvResponse ActuatorServiceResponse = new ActuatorDoActionSrvResponse();
        
        if (auv7.activeSelf)
        {
            // Find a game object with the requested name
            if ((ActuatorDoActionSrvRequest.ELEMENT_DROPPER == request.element) && (ActuatorDoActionSrvRequest.ACTION_DROPPER_LAUNCH == request.action))
            {
                Debug.Log("Dropping droppers...");
                // TODO
            }

            if ((ActuatorDoActionSrvRequest.ELEMENT_TORPEDO == request.element) && (ActuatorDoActionSrvRequest.ACTION_TORPEDO_LAUNCH == request.action))
            {
                Debug.Log("Launching torpedoes...");
                // TODO
            }
        }
        else // AUV8
        {

        }

        return ActuatorServiceResponse;
    }
}