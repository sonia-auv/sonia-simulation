//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.SoniaCommon
{
    [Serializable]
    public class GetPositionTargetRequest : Message
    {
        public const string k_RosMessageName = "sonia_common/GetPositionTarget";
        public override string RosMessageName => k_RosMessageName;


        public GetPositionTargetRequest()
        {
        }
        public static GetPositionTargetRequest Deserialize(MessageDeserializer deserializer) => new GetPositionTargetRequest(deserializer);

        private GetPositionTargetRequest(MessageDeserializer deserializer)
        {
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
        }

        public override string ToString()
        {
            return "GetPositionTargetRequest: ";
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
