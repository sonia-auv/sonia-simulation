using System.Threading;
using System;
using RosMessageTypes.Sensor;
using UnityEngine;


public class CameraDataPublisher : MonoBehaviour
{
    ROSConnection ros;
    public string frontTopicName = "front_simulation";
    public string bottomTopicName = "bottom_simulation";
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
        PublishCameraData();
        System.Threading.Thread.Sleep(100);
    }
    private void PublishCameraData()
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
        Texture2D tex = new Texture2D((int)imageHeight,(int)imageWidth,TextureFormat.RGB24, false);
        RenderTexture.active = frontRenderer.GetComponent<Camera>().targetTexture;
        tex.ReadPixels(new Rect(0, 0, imageWidth, imageHeight), 0, 0);
        tex.Apply();

        RosMessageTypes.Std.Header header = new RosMessageTypes.Std.Header();
        Image cameraData = new Image (
        header,
        imageHeight,
        imageWidth,
        imageEncoding,
        0,
        3 * imageWidth,
        tex.GetRawTextureData()
        );

        ros.Send(frontTopicName, cameraData);

        Destroy(tex);

    }
    private void PublishBottom()
    {
        Texture2D tex = new Texture2D((int)imageHeight,(int)imageWidth,TextureFormat.RGB24, false);
        RenderTexture.active = bottomRenderer.GetComponent<Camera>().targetTexture;
        tex.ReadPixels(new Rect(0, 0, imageWidth, imageHeight), 0, 0);
        tex.Apply();

        RosMessageTypes.Std.Header header = new RosMessageTypes.Std.Header();
        Image cameraData = new Image (
        header,
        imageHeight,
        imageWidth,
        imageEncoding,
        0,
        3 * imageWidth,
        tex.GetRawTextureData()
        );

        ros.Send(bottomTopicName, cameraData);

        Destroy(tex);

    }
}