using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using RosMessageTypes.Sensor;
using RosMessageTypes.Std;
using Unity.Robotics.ROSTCPConnector;


public class SonarDataPublisher : MonoBehaviour
{
    ROSConnection ros;
    public GameObject Sonar;
    // public GameObject bottomCamera;

    private UInt32 sonarFrameRate = 10; //Hz
    private string topicName = "/provider_sonar/LaserScan";
    private UInt32 sequence;
    private double ts; // Camera sample time
    private DateTime dtk = DateTime.Now; // Time when last image is generate
    private DateTime dt = DateTime.Now; // Current time

    private float openningAngle = 90;
    private UInt32 nBins = 128;
    private float angleBetweenBins;


    void Start()
    {
        sequence = 0;

        // Get camera sample time
        ts = (1000.0/sonarFrameRate);

        // Sonar angles
        angleBetweenBins = openningAngle / nBins;

        // Initialize ros publisher
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<LaserScanMsg>(topicName);
    }
    private void Update(){
        

    }

    private void LateUpdate() 
    {
        PublishSonarData();
    }

    private void PublishSonarData()
    {
        // compute elapsed time since last image generated
        dt = DateTime.Now; 
        TimeSpan elapse = dt - dtk;

        // Check if its time to generate a new image.
        if (elapse.TotalMilliseconds >= ts)
        {
            dtk = dt; // Update new time
            sequence = sequence + 1 ;



            // Publish(ref frontRenderer,ref compressedFrontTopicName);

        }
    }
    private void GatherData()
    {
        UInt32 index = 0;
        for(float angle = -openningAngle / 2 ; angle <= openningAngle / 2; angle+= angleBetweenBins)
        {
            RaycastHit hit;
            if(Sonar.Physics.Raycast(Sonar.transform.position,Sonar.transform.TransformDirection(Vector3.forward),out hit,20,layerMasker));
        }
    }

    private void Publish(ref GameObject cam, ref string rawTopic, ref string compressedTopic)
    {
        HeaderMsg header = new HeaderMsg();
        
        ros.Send(rawTopic, cameraData);
        Destroy(tex);
    }