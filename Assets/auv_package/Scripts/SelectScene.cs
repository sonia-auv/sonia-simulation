using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.SoniaCommon;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;


/// <summary>
/// Service that gets a submarine position and sends it as initial condition to start the simulation.
/// </summary>
public class SelectAUV : MonoBehaviour
{
    [SerializeField]
    string m_ServiceName = "set_auv_srv";

    public GameObject AUV8 = null;
    public GameObject AUV7 = null;
    public GameObject freeLookAUV7 = null;
    public GameObject freeLookAUV8 = null;

    private GameObject activeAUV = null;

    void Start()
    {
        // register the service with ROS
        ROSConnection.GetOrCreateInstance().ImplementService<SetAUVServiceRequest, SetAUVServiceResponse>(m_ServiceName, SetAUVSimulation);
    }

    /// <summary>
    ///  Callback to respond to the request
    /// </summary>
    /// <param name="request">service request containing the object name</param>
    /// <returns>service response containing the object pose (or 0 if object not found)</returns>
    private SetAUVServiceResponse SetAUVSimulation(SetAUVServiceRequest request)
    {
        // process the service request
        Debug.Log("Received request to change AUV for : " + request.object_name);

        // prepare a response
        SetAUVServiceResponse SetAUVServiceResponse = new SetAUVServiceResponse();
        // Find a game object with the requested name
        if (request.object_name == "AUV8")
        {
            AUV7.SetActive(false);
            freeLookAUV7.SetActive(false);
            AUV8.SetActive(true);
            freeLookAUV8.SetActive(true);
        }
        if (request.object_name == "AUV7")
        {
            AUV7.SetActive(true);
            freeLookAUV7.SetActive(true);
            AUV8.SetActive(false);
            freeLookAUV8.SetActive(false);
        }

        activeAUV = GameObject.Find(request.object_name);

        if (request.object_name == activeAUV.name)
        {
            SetAUVServiceResponse.success = true;
        }
        else
        {
            SetAUVServiceResponse.success = true;
        }

        return SetAUVServiceResponse;
    }
}