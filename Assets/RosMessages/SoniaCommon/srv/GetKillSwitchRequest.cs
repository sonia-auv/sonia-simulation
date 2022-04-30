//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.SoniaCommon
{
    [Serializable]
    public class GetKillSwitchRequest : Message
    {
        public const string k_RosMessageName = "sonia_common/GetKillSwitch";
        public override string RosMessageName => k_RosMessageName;


        public GetKillSwitchRequest()
        {
        }
        public static GetKillSwitchRequest Deserialize(MessageDeserializer deserializer) => new GetKillSwitchRequest(deserializer);

        private GetKillSwitchRequest(MessageDeserializer deserializer)
        {
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
        }

        public override string ToString()
        {
            return "GetKillSwitchRequest: ";
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