using System.Threading;
using System;
using RosMessageTypes.Sensor;
using RosMessageTypes.Std;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;


public class CameraDataPublisher : MonoBehaviour
{
    ROSConnection ros;
    public string frontTopicName = "front_simulation";
    public string bottomTopicName = "bottom_simulation";
    public UInt32 imageHeight = 720;
    public UInt32 imageWidth = 720;
    public string imageEncoding = "rgb8";

    public string frontStop = "";
    public string bottomStop = "";


    public GameObject frontRenderer;
    public GameObject bottomRenderer;
    private UInt32 sequence;
    private Boolean publishFront;
    private Boolean publishBottom;

    void Start()
    {
        sequence = 0;
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<ImageMsg>(frontTopicName);
        ros.RegisterPublisher<ImageMsg>(bottomTopicName);
    }

    private void FixedUpdate() 
    {
        PublishCameraData();
        //System.Threading.Thread.Sleep(100);

        if (Input.GetKeyDown(frontStop))
        {
            publishFront = !publishFront;
        }

        if (Input.GetKeyDown(bottomStop))
        {
            publishBottom = !publishBottom;
        }
    }

    private void PublishCameraData()
    {
        sequence = sequence + 1 ;
        if (frontRenderer.activeSelf)
        {
            if (publishFront) 
            {
                PublishFront();
            }
        }
        if (bottomRenderer.activeSelf)
        {
            if (publishBottom)
            {
                PublishBottom();
            }
        }
    }

    private void FlipTextureVertically(Texture2D original)
    {
        var originalPixels = original.GetPixels();

        var newPixels = new Color[originalPixels.Length];

        var width = original.width;
        var rows = original.height;

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < rows; y++)
            {
                newPixels[x + y * width] = originalPixels[x + (rows - y -1) * width];
            }
        }
        original.SetPixels(newPixels);
        original.Apply();
    }

    private void PublishFront()
    {
        Texture2D tex = new Texture2D((int)imageHeight,(int)imageWidth,TextureFormat.RGB24, false);
        RenderTexture.active = frontRenderer.GetComponent<Camera>().targetTexture;
        tex.ReadPixels(new Rect(0, 0, imageWidth, imageHeight), 0, 0);
        
        // Flips the image
        FlipTextureVertically(tex);

        HeaderMsg header = new HeaderMsg();
        ImageMsg cameraData = new ImageMsg (
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
        
        // Flips the image
        FlipTextureVertically(tex);

        HeaderMsg header = new HeaderMsg();
        ImageMsg cameraData = new ImageMsg (
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
