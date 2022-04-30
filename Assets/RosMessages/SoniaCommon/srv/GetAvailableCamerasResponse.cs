//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.SoniaCommon
{
    [Serializable]
    public class GetAvailableCamerasResponse : Message
    {
        public const string k_RosMessageName = "sonia_common/GetAvailableCameras";
        public override string RosMessageName => k_RosMessageName;

        public string[] available_media;

        public GetAvailableCamerasResponse()
        {
            this.available_media = new string[0];
        }

        public GetAvailableCamerasResponse(string[] available_media)
        {
            this.available_media = available_media;
        }

        public static GetAvailableCamerasResponse Deserialize(MessageDeserializer deserializer) => new GetAvailableCamerasResponse(deserializer);

        private GetAvailableCamerasResponse(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.available_media, deserializer.ReadLength());
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.WriteLength(this.available_media);
            serializer.Write(this.available_media);
        }

        public override string ToString()
        {
            return "GetAvailableCamerasResponse: " +
            "\navailable_media: " + System.String.Join(", ", available_media.ToList());
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize, MessageSubtopic.Response);
        }
    }
}