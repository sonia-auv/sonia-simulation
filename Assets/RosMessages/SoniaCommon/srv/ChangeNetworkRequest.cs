//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.SoniaCommon
{
    [Serializable]
    public class ChangeNetworkRequest : Message
    {
        public const string k_RosMessageName = "sonia_common/ChangeNetwork";
        public override string RosMessageName => k_RosMessageName;

        // request value
        public string network_name;
        public string topic;
        public float threshold;

        public ChangeNetworkRequest()
        {
            this.network_name = "";
            this.topic = "";
            this.threshold = 0.0f;
        }

        public ChangeNetworkRequest(string network_name, string topic, float threshold)
        {
            this.network_name = network_name;
            this.topic = topic;
            this.threshold = threshold;
        }

        public static ChangeNetworkRequest Deserialize(MessageDeserializer deserializer) => new ChangeNetworkRequest(deserializer);

        private ChangeNetworkRequest(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.network_name);
            deserializer.Read(out this.topic);
            deserializer.Read(out this.threshold);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.network_name);
            serializer.Write(this.topic);
            serializer.Write(this.threshold);
        }

        public override string ToString()
        {
            return "ChangeNetworkRequest: " +
            "\nnetwork_name: " + network_name.ToString() +
            "\ntopic: " + topic.ToString() +
            "\nthreshold: " + threshold.ToString();
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
