using System.Threading;
using System;
using RosMessageTypes.AuvPackage;
using UnityEngine;


public class CamDataPublisher : MonoBehaviour
{
    ROSConnection ros;
    public string frontTopicName = "sensor_msgs/Front_camera_simulation";
    public string bottomTopicName = "sensor_msgs/Bottom_camera_simulation";
    public UInt32 imageHeight = 720;
    public UInt32 imageWidth = 720;
    public string imageEncoding = "rgb8";


    public GameObject frontRenderer;
    public GameObject bottomRenderer;
    private UInt32 sequence;

    void Start()
    {
        sequence = 0;
        // start the ROS connection
        ros = ROSConnection.instance;
    }

    private void Update() 
    {
        PublishCamData();
        System.Threading.Thread.Sleep(100);
    }
    private void PublishCamData()
    {
        sequence = sequence + 1 ;
        if (frontRenderer.activeSelf)
        {
            PublishFront();
        }
        if (bottomRenderer.activeSelf)
        {
            PublishBottom();
        }
    }

    private void PublishFront()
    {
        Texture2D tex = new Texture2D((int)imageHeight,(int)imageWidth);
        RenderTexture.active = frontRenderer.GetComponent<Camera>().targetTexture;
        tex.ReadPixels(new Rect(0, 0, imageWidth, imageHeight), 0, 0);
        tex.Apply();

        RosMessageTypes.Std.Header header = new RosMessageTypes.Std.Header();
        
        // header.seq = sequence;

        // header.frame_id = "";


        CamData camData = new CamData (
        header,
        imageHeight,
        imageWidth,
        imageEncoding,
        0,
        24 * imageWidth,
        tex.GetRawTextureData()
        );

        ros.Send(frontTopicName, camData);

        Destroy(tex);

    }
    private void PublishBottom()
    {

    }
}