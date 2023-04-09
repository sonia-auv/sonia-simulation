//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.UnityRoboticsDemo
{
    [Serializable]
    public class SetSimulationAUVServiceRequest : Message
    {
        public const string k_RosMessageName = "unity_robotics_demo_msgs/SetSimulationAUVService";
        public override string RosMessageName => k_RosMessageName;

        public string object_name;

        public SetSimulationAUVServiceRequest()
        {
            this.object_name = "";
        }

        public SetSimulationAUVServiceRequest(string object_name)
        {
            this.object_name = object_name;
        }

        public static SetSimulationAUVServiceRequest Deserialize(MessageDeserializer deserializer) => new SetSimulationAUVServiceRequest(deserializer);

        private SetSimulationAUVServiceRequest(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.object_name);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.object_name);
        }

        public override string ToString()
        {
            return "SetSimulationAUVServiceRequest: " +
            "\nobject_name: " + object_name.ToString();
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
