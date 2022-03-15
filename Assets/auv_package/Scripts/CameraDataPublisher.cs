using System.Threading;
using System;
using RosMessageTypes.Sensor;
using RosMessageTypes.Std;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;


public class CameraDataPublisher : MonoBehaviour
{
    ROSConnection ros;
    public UInt32 cameraFrameRate;
    public string rawFrontTopicName;
    public string compressedFrontTopicName;
    public string rawBottomTopicName;
    public string compressedBottomTopicName;
    public UInt32 imageHeight;
    public UInt32 imageWidth;
    public string imageEncoding;

    public string frontStop ;
    public string bottomStop ;


    public GameObject frontCamera;
    public GameObject bottomCamera;
    private GameObject frontRenderer;
    private GameObject bottomRenderer;
    private UInt32 sequence;
    private double ts; // Cmera sample time
    private DateTime dtk = DateTime.Now; // Time when last image is generate
    private DateTime dt = DateTime.Now; // Current time
    void Start()
    {
        sequence = 0;
        // Get render object
        frontRenderer = frontCamera.transform.GetChild(0).gameObject;
        bottomRenderer = bottomCamera.transform.GetChild(0).gameObject;

        // Get camera sample time
        ts = (1000.0/cameraFrameRate);

        // Initialize ros publisher
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<ImageMsg>(rawFrontTopicName);
        ros.RegisterPublisher<CompressedImageMsg>(compressedFrontTopicName);
        ros.RegisterPublisher<ImageMsg>(rawBottomTopicName);   
        ros.RegisterPublisher<CompressedImageMsg>(compressedBottomTopicName);   
    }
    private void Update(){
        
        // update cam toggle in update method for better responsiveness
        if (Input.GetKeyDown(frontStop))
        {
            changeCameraStatus(ref frontCamera);
        }

        if (Input.GetKeyDown(bottomStop))
        {
             changeCameraStatus(ref bottomCamera);
             
        }
    }
    private void LateUpdate() 
    {
        PublishCameraData();
    }
    private void changeCameraStatus(ref GameObject cam){

        if (cam.activeSelf)
        {
            cam.SetActive(false);
        }
        else
        {
            cam.SetActive(true);
        }
    }
    private void PublishCameraData()
    {
        // compute elapsed time since last image generated
        dt = DateTime.Now; 
        TimeSpan elapse = dt - dtk;

        // Check if its time to generate a new image.
        if (elapse.TotalMilliseconds >= ts)
        {
            dtk = dt; // Update new time
            sequence = sequence + 1 ;

            if (frontCamera.activeSelf)
            {
                    Publish(ref frontRenderer,ref rawFrontTopicName,ref compressedFrontTopicName);
            }
            if (bottomCamera.activeSelf)
            { 
                    Publish(ref bottomRenderer,ref rawBottomTopicName,ref compressedBottomTopicName);
            }
        }
    }
    private void Publish(ref GameObject cam, ref string rawTopic, ref string compressedTopic)
    {
        Texture2D tex = new Texture2D((int)imageHeight,(int)imageWidth,TextureFormat.RGB24, false);
        RenderTexture.active = cam.GetComponent<Camera>().targetTexture;
        tex.Reinitialize((int)imageWidth,(int)imageHeight,TextureFormat.RGB24,false);
        tex.Apply();
        tex.ReadPixels(new Rect(0, 0, imageWidth, imageHeight), 0, 0);
        tex.Apply();

        HeaderMsg header = new HeaderMsg();

        byte[] imageBytes = tex.EncodeToJPG();
        var message = new CompressedImageMsg(header, "png", imageBytes);
        ros.Send(compressedTopic, message);

        // Flips the image
        FlipTextureVertically(ref tex);

        ImageMsg cameraData = new ImageMsg (
                                            header,
                                            imageHeight,
                                            imageWidth,
                                            imageEncoding,
                                            0,
                                            1800, // step, had to put in hard as the operation needs a float
                                            tex.GetRawTextureData()
                                            );

        ros.Send(rawTopic, cameraData);
        Destroy(tex);
    }
    
    private void FlipTextureVertically(ref Texture2D original)
    {
        Color[] originalPixels = original.GetPixels();
        Color[] newPixels = new Color[originalPixels.Length];

        int width = original.width;
        int rows = original.height;

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < rows; ++y)
            {
                newPixels[x + y * width] = originalPixels[x + (rows - y -1) * width];
            }
        }
        original.SetPixels(newPixels);
        original.Apply();
    }

  
}
