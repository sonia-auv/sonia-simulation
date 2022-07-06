//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.SoniaCommon
{
    [Serializable]
    public class SetSimulationAUVRequest : Message
    {
        public const string k_RosMessageName = "sonia_common/SetSimulationAUV";
        public override string RosMessageName => k_RosMessageName;

        public string object_name;

        public SetSimulationAUVRequest()
        {
            this.object_name = "";
        }

        public SetSimulationAUVRequest(string object_name)
        {
            this.object_name = object_name;
        }

        public static SetSimulationAUVRequest Deserialize(MessageDeserializer deserializer) => new SetSimulationAUVRequest(deserializer);

        private SetSimulationAUVRequest(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.object_name);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.object_name);
        }

        public override string ToString()
        {
            return "SetSimulationAUVRequest: " +
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