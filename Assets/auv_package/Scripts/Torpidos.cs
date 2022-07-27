using System.Threading;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;

using System.Collections.Generic;
using RosMessageTypes.SoniaCommon;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;

public class Torpidos : MonoBehaviour
{
    public GameObject auv;

    public GameObject Projectile;
    public float launchVelocity = 10f;

    public float xOffset = 0;   // Translation X par rapport au ref du GameObject de ce composant 
    public float yOffset = 0;   // Translation Y par rapport au ref du GameObject de ce composant
    public float zOffset = 0;   // Translation Z par rapport au ref du GameObject de ce composant 
    

    public float xRotOffset = 0; // Euler, Convention ZXY, Aligner le X torpile avec le X du Sub 
    public float yRotOffset = 0; // Euler, Convention ZXY, Aligner le X torpile avec le X du Sub
    public float zRotOffset = 0; // Euler, Convention ZXY, Aligner le X torpile avec le X du Sub

    private Pose pose;
    private byte response;
    private bool torpidoPortState = true;
    private bool torpidoPortStarboard = true;
    private ROSConnection ros;

    private string SubTopicName = "/provider_actuators/do_action_to_actuators";
    private string PubTopicName = "/provider_actuators/do_action_from_actuators";

    
    List<GameObject> generatedObjects = new List<GameObject>();

    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<ActuatorSendReplyMsg>(PubTopicName);
        ros.Subscribe<ActuatorDoActionMsg>(SubTopicName,ShotHandler);

        pose.position = auv.transform.position + new Vector3(xOffset,yOffset,zOffset);
        Quaternion rotation = Quaternion.Euler(xRotOffset,yRotOffset,zRotOffset);
        pose.rotation = auv.transform.rotation * rotation; 
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            foreach(var obj in generatedObjects)
            {
                Destroy(obj);
                torpidoPortState = torpidoPortStarboard = true;
            }
        }
    }

    void ShotHandler(ActuatorDoActionMsg msgReceive)
    {
        if(msgReceive.element == 0 && msgReceive.action == 1)
        {
            if(msgReceive.side == 0 && torpidoPortState)
            {
                torpidoPortState = false; // Shots fired ;)
                response = 1; // succes
            }
            else if(msgReceive.side == 1 && torpidoPortStarboard)
            {
                torpidoPortStarboard = false;
                response = 1; // succes
            }
            else
            {
                response = 0; // Failure
            }

            if(response == 1)
            {
                fire();
            }
            Publish(msgReceive);
        }        
    }

    void fire()
    {
        // Fire !
        GameObject Torpido = Instantiate(Projectile, pose.position, pose.rotation); // zxy convention
        generatedObjects.Add(Torpido);
        Torpido.GetComponent<Rigidbody>().AddRelativeForce(new Vector3 (launchVelocity, 0 ,0)); 
    }


    private void Publish(ActuatorDoActionMsg msgReceive)
    {
        ActuatorSendReplyMsg msg = new ActuatorSendReplyMsg();
        
        msg.element = msgReceive.element;
        msg.side = msgReceive.side;
        msg.response = response;

        ros.Send(PubTopicName, msg);
    }
         
}
