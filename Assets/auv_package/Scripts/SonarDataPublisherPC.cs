using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using RosMessageTypes.Sensor;
using RosMessageTypes.Std;
using Unity.Robotics.ROSTCPConnector;


public class SonarDataPublisherPC : MonoBehaviour
{
    ROSConnection ros;
    public GameObject Sonar;
    // public GameObject bottomCamera;

    private UInt32 sonarFrameRate = 10; //Hz
    private string topicName = "/provider_sonar/point_cloud2";
    private UInt32 sequence;
    private double ts; // Camera sample time
    private DateTime dtk = DateTime.Now; // Time when last image is generate
    private DateTime dt = DateTime.Now; // Current time

    private float openningAngle = 90;
    private UInt32 nBins = 128;
    private float angleBetweenBins;
    private List<float> data = new List<float>();
    private List<PointFieldMsg> fields = new List<PointFieldMsg>();
        // fields.name = "Intensity";
        // fields.offset = 4;
        // fields.datatype = 7; // float32
        // field.count = 4;
    private GameObject ray;
    public float Resolution = 0.08f;

    private uint height = 1;
    private uint width = 0;

    //public PointFieldMsg[] fields;
    private bool is_bigendian = false;
    //  Is this data bigendian?
    private uint point_step = 32;
    //  Length of a point in bytes
    private uint row_step = 0;
    //  Actual point data, size is (row_step*height)
    private bool is_dense = true;

    public float range = 20;


    void Start()
    {
        sequence = 0;

        // Get camera sample time
        ts = (100.0/sonarFrameRate);

        // Sonar angles
        angleBetweenBins = openningAngle / (float)nBins;

        // Initialize ros publisher
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<PointCloud2Msg>(topicName);
        ray = new GameObject();
        fields.Add(new PointFieldMsg("x", 0, 7, 1));
        fields.Add(new PointFieldMsg("y", 4, 7, 1));
        fields.Add(new PointFieldMsg("z", 8, 7, 1));
        fields.Add(new PointFieldMsg("intensity", 12, 7, 1));
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
            if(width != 0)
            {
                Publish();
            }
        }
    }
    private void GatherData()
    {
        UInt32 index = 0;
        for(float angle = -openningAngle / 2.0f ; angle < openningAngle / 2.0f; angle+= angleBetweenBins)
        {
            ray.transform.position = Sonar.transform.position;
            ray.transform.rotation = Sonar.transform.rotation;
            ray.transform.Rotate(90-angle,-90,0); //ZXY convention
            RaycastHit[] hits = Physics.RaycastAll(Sonar.transform.position,ray.transform.forward,range);

            if(Convert.ToBoolean(hits.Length)) // transform forward -> axeZ(bleu)
            {
                hitHandler(hits,ref data,angle);
            }
            else
            {
                Debug.DrawRay(Sonar.transform.position, ray.transform.forward * range, Color.red);
                // addData(range, angle); // no more hit -> max value
                Debug.Log("Did not Hit");
            }
            ++index;
        }
    }
    private void addData(in RaycastHit newhit,in float rayAngle) //true hit
    {
        data.Add( newhit.distance * (float) Math.Cos((Math.PI / 180) *rayAngle) + UnityEngine.Random.Range(-Resolution/2.0f,Resolution/2.0f) ); // x
        data.Add( newhit.distance * (float) Math.Sin((Math.PI / 180) *rayAngle) + UnityEngine.Random.Range(-Resolution/2.0f,Resolution/2.0f)); // y
        data.Add((float) 0); // z
        data.Add((float) 0.1); // intensity
        ++width;
        Debug.Log("distance X: " + newhit.distance * (float) Math.Cos(rayAngle) + "distance Y: " + newhit.distance * (float) Math.Sin(rayAngle) + "distance hit: " + newhit.distance);
    }

    private void addData(in float distance,in float rayAngle) // bad hit (i.e max range)
    {
        data.Add( distance * (float) Math.Cos((Math.PI / 180) *rayAngle) ); // x
        data.Add( distance * (float) Math.Sin((Math.PI / 180) *rayAngle) ); // y
        data.Add((float) 0); // z
        data.Add((float) 0); // intensity
        ++width;
        Debug.Log("distance X: " + distance * (float) Math.Sin(rayAngle) + "distance Y: " + distance * (float) Math.Cos(rayAngle) + "distance hit: " + distance + " bad hit");
    }
    private void hitHandler(in RaycastHit[] hits, ref List<float> data, in float rayAngle)
    {
            float dist = 0;
            RaycastHit hit = hits[0];
            RaycastHit lastHit = hits[0];
            RaycastHit maxHit = hits[0]; // max distance hit (For debug + max range data in pc2)
            for (int i = 0; i < hits.Length; ++i)
            {
                if(i >= 1)
                {
                    lastHit = hit;
                }
                hit = hits[i];
                if(hit.distance > maxHit.distance)
                {
                    maxHit = hit;
                }
                addData(hit, rayAngle);

                if(i == 0)
                {
                    Debug.DrawRay(Sonar.transform.position, ray.transform.forward * hit.distance, Color.green); // first hit
                }
                else
                {
                    Debug.DrawRay(lastHit.point, ray.transform.forward * (hit.distance - lastHit.distance), Color.green); // still a good hit
                }
            }
            if(maxHit.distance < range) // add max value in result
            {
                Debug.DrawRay(maxHit.point, ray.transform.forward * (range - maxHit.distance), Color.yellow); // no more hit -> max value
                //addData(range, rayAngle);
            }
            Debug.Log("Did Hit");   
    }

    private void Publish()
    {
        PointCloud2Msg msg = new PointCloud2Msg();
        msg.header.seq = sequence;
        msg.header.frame_id = "BODY";
        msg.height = 1;
        msg.width = width;
        msg.fields = fields.ToArray();
        msg.is_bigendian = false;
        msg.point_step = 16;
        msg.row_step = msg.height * width * msg.point_step;
        Array.Resize(ref msg.data, data.Count() * 4);
        Buffer.BlockCopy(data.ToArray(), 0, msg.data, 0, data.Count() * 4);
        msg.is_dense = true;
        ros.Send(topicName, msg);

        data.Clear();
        width = 0;
    }
}