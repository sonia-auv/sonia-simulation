// using UnityEngine;
// using Unity.Robotics.ROSTCPConnector;
// using RosMessageTypes.SoniaCommon;
// using Unity.Robotics.ROSTCPConnector.ROSGeometry;


// /// <summary>
// /// Service that gets a submarine position and sends it as initial condition to start the simulation.
// /// </summary>
// public class SetAUVService : MonoBehaviour
// {
//     [SerializeField]
//     string m_ServiceName = "set_auv_srv";

//     void Start()
//     {
//         // register the service with ROS
//         ROSConnection.GetOrCreateInstance().ImplementService<SetAUVServiceRequest, SetAUVServiceResponse>(m_ServiceName, SetAUV);
//     }

//     /// <summary>
//     ///  Callback to respond to the request
//     /// </summary>
//     /// <param name="request">service request containing the object name</param>
//     /// <returns>service response containing the object pose (or 0 if object not found)</returns>
//     private SetAUVServiceResponse SetAUV(SetAUVServiceRequest request)
//     {
//         // process the service request
//         Debug.Log("Received request to change AUV for : " + request.auv);

//         // prepare a response
//         SetAUVServiceResponse setAUVResponse = new SetAUVServiceResponse();
//         // Find a game object with the requested name
//         foreach (GameObject auv in GameObject.FindGameObjectsWithTag("AUV"))
//         {
//             if (auv.name == request.auv)
//             {
//                 auv.
//             }
//         }
//         GameObject gameObject = GameObject.Find(request.auv);

//         return objectPoseResponse;
//     }
// }