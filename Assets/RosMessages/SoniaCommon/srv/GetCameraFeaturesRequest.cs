//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.SoniaCommon
{
    [Serializable]
    public class GetCameraFeaturesRequest : Message
    {
        public const string k_RosMessageName = "sonia_common/GetCameraFeatures";
        public override string RosMessageName => k_RosMessageName;

        //  List of the feature types supported by the vision server.
        public const uint FEATURE_BOOL = 0;
        public const uint FEATURE_INT = 1;
        public const uint FEATURE_DOUBLE = 2;
        public string camera_name;
        public string camera_feature;

        public GetCameraFeaturesRequest()
        {
            this.camera_name = "";
            this.camera_feature = "";
        }

        public GetCameraFeaturesRequest(string camera_name, string camera_feature)
        {
            this.camera_name = camera_name;
            this.camera_feature = camera_feature;
        }

        public static GetCameraFeaturesRequest Deserialize(MessageDeserializer deserializer) => new GetCameraFeaturesRequest(deserializer);

        private GetCameraFeaturesRequest(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.camera_name);
            deserializer.Read(out this.camera_feature);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.camera_name);
            serializer.Write(this.camera_feature);
        }

        public override string ToString()
        {
            return "GetCameraFeaturesRequest: " +
            "\ncamera_name: " + camera_name.ToString() +
            "\ncamera_feature: " + camera_feature.ToString();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize);
        }
    }
}
