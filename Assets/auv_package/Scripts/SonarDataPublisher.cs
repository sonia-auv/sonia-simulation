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
    private float[] ranges;
    private GameObject ray;
    public float Resolution = 0.08f;


    void Start()
    {
        sequence = 0;

        // Get camera sample time
        ts = (100.0/sonarFrameRate);

        // Sonar angles
        angleBetweenBins = openningAngle / (float)nBins;

        // Initialize ros publisher
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<LaserScanMsg>(topicName);

        // allocate range array
        ranges = new float[nBins];

        ray =new GameObject();
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

            GatherData();
            Publish();

            // Publish(ref frontRenderer,ref compressedFrontTopicName);

        }
    }
    private void GatherData()
    {
        UInt32 index = 0;
        for(float angle = -openningAngle / 2.0f ; angle < openningAngle / 2.0f; angle+= angleBetweenBins)
        {
            RaycastHit hit;
            ray.transform.rotation = Sonar.transform.rotation;
            ray.transform.Rotate(90-angle,-90,0); //ZXY convention
            
            if(Physics.Raycast(Sonar.transform.position,ray.transform.forward,out hit,20)) // transform forward -> axeZ(bleu)
            {
                Debug.DrawRay(Sonar.transform.position, ray.transform.forward * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
                ranges[index] = hit.distance + UnityEngine.Random.Range(-Resolution/2.0f,Resolution/2.0f);
            }
            else
            {
                Debug.DrawRay(Sonar.transform.position, ray.transform.forward * 1000, Color.red,1);
                Debug.Log("Did not Hit");
                ranges[index] = 20 + UnityEngine.Random.Range(-Resolution/2.0f,Resolution/2.0f); // range max, mais a voir
            }
            ++index;
        }
    }       

    private void Publish()
    {
        LaserScanMsg msg = new LaserScanMsg();
        msg.header.seq = sequence;
        msg.header.frame_id = "BODY";
        msg.angle_min = -openningAngle/2 *3.141592f /180.0f;
        msg.angle_max = openningAngle/2 *3.141592f /180.0f;
        msg.angle_increment = angleBetweenBins *3.141592f /180.0f;
        msg.range_min = 0.20f;
        msg.range_max = 20.0f;
        msg.ranges = ranges;
        ros.Send(topicName, msg);
    }
}